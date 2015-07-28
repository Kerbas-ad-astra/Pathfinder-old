﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using KSP.IO;

/*
Source code copyrighgt 2015, by Michael Billard (Angel-125)
License: CC BY-NC-SA 4.0
License URL: https://creativecommons.org/licenses/by-nc-sa/4.0/
If you want to use this code, give me a shout on the KSP forums! :)
Wild Blue Industries is trademarked by Michael Billard and may be used for non-commercial purposes. All other rights reserved.
Note that Wild Blue Industries is a ficticious entity 
created for entertainment purposes. It is in no way meant to represent a real entity.
Any similarity to a real entity is purely coincidental.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
namespace WildBlueIndustries
{
    public struct ResearchResource
    {
        public string name;
        public float amount;
        public ResourceFlowMode flowMode;
    }

    public class WBIBasicScienceLab : ExtendedPartModule
    {
        private const float kminimumSuccess = 80f;
        private const float kCriticalSuccess = 95f;
        private const float kCriticalFailure = 33f;
        private const float kCriticalFailPenalty = 15f;
        private const float kBaseResearchDivisor = 1.75f;
        private const float kDefaultResearchTime = 7f;
        private const float kCriticalSuccessBonus = 1.5f;

        public static bool showResults = true;

        protected float kMessageDuration = 6.5f;
        protected string kBotchedResults = "Botched results! This may have consequences in the future...";
        protected string kGreatResults = "Analysis better than expected!";
        protected string kGoodResults = "Good results!";
        protected string kNoSuccess = "Analysis inconclusive";
        protected string kInsufficientResources = "Research halted. Not enough resources to perform the analysis.";
        protected string kScienceAdded = "Science added: {0:f2}";
        protected string kReputationAdded = "Reputation added: {0:f2}";
        protected string kFundsAdded = "Funds added: {0:f2}";

        [KSPField]
        public string startResearchGUIName;

        [KSPField]
        public string stopResearchGUIName;

        [KSPField]
        public float minimumSuccess;

        [KSPField]
        public float criticalSuccess;

        [KSPField]
        public float criticalFail;

        [KSPField]
        public float scientistBonus;

        [KSPField]
        public double researchTime;

        [KSPField]
        public float sciencePerCycle;

        [KSPField]
        public float reputationPerCycle;

        [KSPField]
        public float fundsPerCycle;

        [KSPField(isPersistant = true)]
        public bool isResearching;

        [KSPField(isPersistant = true)]
        public double lastUpdated;

        [KSPField(isPersistant = true)]
        public double researchStartTime;

        [KSPField(guiActive = true, guiName = "Progress")]
        public string researchProgress;

        public double elapsedTime;
        public List<ResearchResource> inputResources = new List<ResearchResource>();

        protected float averageCrewSkill = -1.0f;
        protected double secondsPerCycle = 0f;
        protected bool failedLastAttempt;
        protected float successBonus;
        protected float scienceAdded;
        protected float reputationAdded;
        protected float fundsAdded;

        #region Actions And Events
        [KSPAction("Toggle Research")]
        public virtual void ToggleResearchAction(KSPActionParam param)
        {
            ToggleResearch();
        }

        [KSPEvent(guiActive = true)]
        public virtual void ToggleResearch()
        {
            isResearching = !isResearching;

            if (isResearching)
            {
                Events["ToggleResearch"].guiName = stopResearchGUIName;
                researchStartTime = Planetarium.GetUniversalTime();
                lastUpdated = researchStartTime;
                elapsedTime = 0.0f;
            }

            else
            {
                Events["ToggleResearch"].guiName = startResearchGUIName;
                researchProgress = "None";
            }
        }

        #endregion

        #region Overrides

        public override void OnLoad(ConfigNode node)
        {
            base.OnLoad(node);

            if (HighLogic.LoadedSceneIsFlight == false)
                return;
        }

        public override void OnSave(ConfigNode node)
        {
            base.OnSave(node);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (FlightGlobals.ready == false)
                return;
            if (HighLogic.LoadedSceneIsFlight == false)
                return;
            if (isResearching == false)
                return;

            //Calculate the average crew skill and seconds of research per cycle.
            //Thes values can change if the player swaps out crew.
            averageCrewSkill = GetAverageSkill();
            secondsPerCycle = GetSecondsPerCycle();

            //If research is in progress and it's been awhile since the last update
            //then we need to catch up on research.
            if (lastUpdated == 0.0f)
            {
                lastUpdated = Planetarium.GetUniversalTime();
                return;
            }

            else if (Planetarium.GetUniversalTime() - lastUpdated > 1.0f)
            {
                CatchUpOnResearch();
                return;
            }

            //Calculate elapsed time
            elapsedTime = Planetarium.GetUniversalTime() - researchStartTime;

            //Consume input resources
            ConsumeResources();

            //Calculate progress
            CalculateProgress();

            //If we've completed our research cycle then perform the analyis.
            if (elapsedTime >= secondsPerCycle)
            {
                PerformAnalysis();
                
                //Reset elapsed time.
                researchStartTime = Planetarium.GetUniversalTime();
            }

            //Keep track of the last time we updated.
            lastUpdated = Planetarium.GetUniversalTime();
        }

        public override void OnStart(StartState state)
        {
            UnityEngine.Random.seed = (int)System.DateTime.Now.Ticks;
            base.OnStart(state);
            if (HighLogic.LoadedSceneIsFlight == false)
                return;

            //Setup
            researchProgress = "None";
            if (researchTime == 0f)
                researchTime = kDefaultResearchTime;
            if (isResearching)
                Events["ToggleResearch"].guiName = stopResearchGUIName;
            else
                Events["ToggleResearch"].guiName = startResearchGUIName;

            if (minimumSuccess == 0)
                minimumSuccess = kminimumSuccess;
            if (criticalSuccess == 0)
                criticalSuccess = kCriticalSuccess;
            if (criticalFail == 0)
                criticalFail = kCriticalFailure;
        }

        #endregion

        #region Helpers
        public virtual void CatchUpOnResearch()
        {
            double secondsSinceLastUpdate = Planetarium.GetUniversalTime() - lastUpdated;
            int cyclesSinceLastUpdate = Mathf.RoundToInt((float)(secondsSinceLastUpdate / secondsPerCycle));
            int currentCycle;
            float totalScienceAdded = 0.0f;
            float totalReputationAdded = 0.0f;
            float totalFundsAdded = 0.0f;
            bool currentShowResults = showResults;

            showResults = false;
            for (currentCycle = 0; currentCycle < cyclesSinceLastUpdate; currentCycle++)
            {
                ConsumeResources((float)secondsPerCycle);
                if (isResearching == false)
                {
                    lastUpdated = Planetarium.GetUniversalTime();
                    researchStartTime = Planetarium.GetUniversalTime();
                    showResults = currentShowResults;
                    return;
                }

                PerformAnalysis();
                totalScienceAdded += scienceAdded;
                totalReputationAdded += reputationAdded;
                totalFundsAdded += fundsAdded;
                scienceAdded = 0.0f;
                reputationAdded = 0.0f;
                fundsAdded = 0.0f;
                if (isResearching == false)
                {
                    lastUpdated = Planetarium.GetUniversalTime();
                    researchStartTime = Planetarium.GetUniversalTime();
                    showResults = currentShowResults;
                    if (totalScienceAdded > 0.0f && showResults)
                        ScreenMessages.PostScreenMessage(string.Format(kScienceAdded, totalScienceAdded), kMessageDuration, ScreenMessageStyle.UPPER_CENTER);
                    if (totalReputationAdded > 0.0f && showResults)
                        ScreenMessages.PostScreenMessage(string.Format(kReputationAdded, totalReputationAdded), kMessageDuration, ScreenMessageStyle.UPPER_CENTER);
                    if (totalFundsAdded > 0.0f && showResults)
                        ScreenMessages.PostScreenMessage(string.Format(kFundsAdded, totalFundsAdded), kMessageDuration, ScreenMessageStyle.UPPER_CENTER);
                    return;
                }
            }

            //Cleanup
            lastUpdated = Planetarium.GetUniversalTime();
            researchStartTime = Planetarium.GetUniversalTime();
            showResults = currentShowResults;

            //If we generated science, then report the results
            if (totalScienceAdded > 0.0f && showResults)
                ScreenMessages.PostScreenMessage(string.Format(kScienceAdded, totalScienceAdded), kMessageDuration, ScreenMessageStyle.UPPER_CENTER);
            if (totalReputationAdded > 0.0f && showResults)
                ScreenMessages.PostScreenMessage(string.Format(kReputationAdded, totalReputationAdded), kMessageDuration, ScreenMessageStyle.UPPER_CENTER);
            if (totalFundsAdded > 0.0f && showResults)
                ScreenMessages.PostScreenMessage(string.Format(kFundsAdded, totalFundsAdded), kMessageDuration, ScreenMessageStyle.UPPER_CENTER);
        }

        public virtual void SetGuiVisible(bool isVisible)
        {
            Fields["researchProgress"].guiActive = isVisible;
            Fields["researchProgress"].guiActiveEditor = isVisible;
            Events["ToggleResearch"].guiActive = isVisible;
            Events["ToggleResearch"].guiActiveUnfocused = isVisible;
            Events["ToggleResearch"].guiActiveEditor = isVisible;
        }

        public virtual void ConsumeResources(float timeElapsed = 0.0f)
        {
            double resourcePerTimeTick;
            double amountObtained;
            PartResourceDefinition definition;

            foreach (ResearchResource input in inputResources)
            {
                //Get the resource definition
                definition = ResourceHelper.DefinitionForResource(input.name);
                if (definition == null)
                {
                    Log("No definition for " + input.name);
                    return;
                }

                //Calculate the amount of resource to consume.
                if (timeElapsed == 0.0f)
                    resourcePerTimeTick = input.amount * TimeWarp.fixedDeltaTime;
                else
                    resourcePerTimeTick = input.amount * timeElapsed;

                //Special case: ElectricCharge
                //We can't tell how much EC gets generated over time.
                if (input.name == "ElectricCharge" && timeElapsed > 0.0f)
                    continue;

                //Try to get the resource
                amountObtained = this.part.RequestResource(definition.id, resourcePerTimeTick, input.flowMode);

                //If we don't have enough resources then we're done
                if ((amountObtained / resourcePerTimeTick) < 0.999f)
                {
                    isResearching = false;
                    Events["ToggleResearch"].guiName = startResearchGUIName;
                    if (showResults)
                        ScreenMessages.PostScreenMessage(kInsufficientResources, kMessageDuration, ScreenMessageStyle.UPPER_CENTER);

                    this.part.RequestResource(definition.id, -amountObtained, input.flowMode);

                    return;
                }
            }
        }

        public virtual void CalculateProgress()
        {
            //Get elapsed time (seconds)
            researchProgress = string.Format("{0:f1}%", ((elapsedTime / secondsPerCycle) * 100));
        }

        public virtual void PerformAnalysis()
        {
            float analysisRoll = performAnalysisRoll();

            if (analysisRoll <= criticalFail)
                onCriticalFailure();

            else if (analysisRoll >= criticalSuccess)
                onCriticalSuccess();

            else if (analysisRoll >= minimumSuccess)
                onSuccess();

            else
                onFailure();

        }

        public double GetSecondsPerCycle()
        {
            if (averageCrewSkill == 0)
                averageCrewSkill = GetAverageSkill();
            double hoursPerCycle = Math.Pow((researchTime / (kBaseResearchDivisor + (averageCrewSkill / 10.0f))), 3.0f);

            return hoursPerCycle * 3600;
        }

        protected virtual float performAnalysisRoll()
        {
            float roll = 0.0f;

            //Roll 3d6 to approximate a bell curve, then convert it to a value between 1 and 100.
            roll = UnityEngine.Random.Range(1, 6);
            roll += UnityEngine.Random.Range(1, 6);
            roll += UnityEngine.Random.Range(1, 6);
            roll *= 5.5556f;

            //Factor in crew
            roll += (averageCrewSkill * 10);

            //Done
            return roll;
        }

        protected virtual void onCriticalFailure()
        {
            if (showResults)
                ScreenMessages.PostScreenMessage(kBotchedResults, kMessageDuration, ScreenMessageStyle.UPPER_CENTER);
        }

        protected virtual void onCriticalSuccess()
        {
            successBonus = kCriticalSuccessBonus;
            addCurrency();

            if (showResults)
                ScreenMessages.PostScreenMessage(kGreatResults, kMessageDuration, ScreenMessageStyle.UPPER_CENTER);
        }

        protected virtual void onFailure()
        {
            if (showResults)
                ScreenMessages.PostScreenMessage(kNoSuccess, kMessageDuration, ScreenMessageStyle.UPPER_CENTER);
        }

        protected virtual void onSuccess()
        {
            successBonus = 1.0f;
            addCurrency();

            if (showResults)
                ScreenMessages.PostScreenMessage(kGoodResults, kMessageDuration, ScreenMessageStyle.UPPER_CENTER);
        }

        protected virtual void addCurrency()
        {
            float successFactor = successBonus * (1.0f + (averageCrewSkill / 10.0f));

            //Add science to the resource pool
            if (sciencePerCycle > 0.0f)
            {
                scienceAdded = sciencePerCycle * successFactor;
                ResearchAndDevelopment.Instance.AddScience(scienceAdded, TransactionReasons.Any);
            }

            //Reputation
            if (reputationPerCycle > 0.0f)
            {
                reputationAdded = reputationPerCycle * successFactor;
                Reputation.Instance.AddReputation(reputationAdded, TransactionReasons.Any);
            }

            //Funds
            if (fundsPerCycle > 0.0f)
            {
                fundsAdded = fundsPerCycle * successFactor;
                Funding.Instance.AddFunds(fundsAdded, TransactionReasons.Any);
            }
        }

        public virtual float GetAverageSkill()
        {
            float totalSkillPoints = 0f;
            int totalScientists = 0;

            if (this.part.CrewCapacity == 0)
                return 0f;

            foreach (ProtoCrewMember crewMember in this.part.protoModuleCrew)
            {
                if (crewMember.experienceTrait.TypeName == "Scientist")
                {
                    totalSkillPoints += crewMember.experienceTrait.CrewMemberExperienceLevel();
                    totalScientists += 1;
                }
            }

            return totalSkillPoints / totalScientists;
        }

        public virtual void LoadValuesFromNode(ConfigNode node)
        {
            string flowMode;
            inputResources.Clear();
            ResearchResource inputResource;
            ConfigNode[] nodeInputs = node.GetNodes("INPUT_RESOURCE");

            foreach (ConfigNode nodeInput in nodeInputs)
            {
                inputResource = new ResearchResource();
                inputResource.name = nodeInput.GetValue("name");
                inputResource.amount = float.Parse(nodeInput.GetValue("amount"));

                flowMode = nodeInput.GetValue("flowMode");
                if (string.IsNullOrEmpty(flowMode) == false)
                    inputResource.flowMode = (ResourceFlowMode)Enum.Parse(typeof(ResourceFlowMode), flowMode);
                else
                    inputResource.flowMode = ResourceFlowMode.ALL_VESSEL;

                inputResources.Add(inputResource);
            }
        }

        protected override void getProtoNodeValues(ConfigNode protoNode)
        {
            base.getProtoNodeValues(protoNode);

            LoadValuesFromNode(protoNode);
        }
        #endregion
    }
}
