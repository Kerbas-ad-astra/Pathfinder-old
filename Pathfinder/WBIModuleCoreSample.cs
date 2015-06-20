using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using UnityEngine;
using KSP.IO;

/*
Source code copyright 2015, by Michael Billard (Angel-125)
License: CC BY-NC-SA 4.0
License URL: https://creativecommons.org/licenses/by-nc-sa/4.0/
Wild Blue Industries is trademarked by Michael Billard and may be used for non-commercial purposes. All other rights reserved.
Note that Wild Blue Industries is a ficticious entity 
created for entertainment purposes. It is in no way meant to represent a real entity.
Any similarity to a real entity is purely coincidental.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
namespace WildBlueIndustries
{
    public enum CoreSampleStates
    {
        Ready,
        Deploying,
        TakingSample,
        Retracting
    }

    public class WBIModuleCoreSample : ModuleScienceExperiment
    {
        private const float kBaseEfficiencyModifier = 0.1f;
        private const float maxWorsenRoll = 40f;
        private const float minImprovementRoll = 60f;
        private const float kExperiencePercentModifier = 3.0f;
        private const float kMessageDuration = 5.0f;
        private const float kWarningMsgDuration = 10.0f;
        private const string kNeedVesselLanded = "Vessel needs to be landed in order to peform analysis.";
        private const string kNeedVesselSplashed = "Vessel needs to be splashed in order to peform analysis.";
        private const string kUnlockBiome = "Perform a surface scan before performing the analysis.";
        private const string kResourceExtractionImproved = "Extraction rates for all drills increased by {0:#.##}% for ";
        private const string kResourceExtractionWorsened = "Extraction rates for all drills reduced by {0:#.##}% for ";
        private const string kResourceExtractionUnchanged = "Extraction rates for all drills unchanged for ";
        private const string kAnalysisStatus = "Analyzing results, please wait...";
        private const string kNoMoreAttempts = "Analysis cycle completed. Either move to a new biome or declare your results invalid.";
        private const string kUnknown = "Unknown";
        private const string kConfirmInvalidate = "Declaring core sample results invalid will adversely affect your reputation. Click again to confirm.";
        private const string kFirstCoreSampleTitle = "Your First Core Sample!";
        private const string kFirstCoreSampleMsg = "Congratulations, you've taken your very first core sample! Core samples provide a detailed look at the biome's resources and their results will affect the efficiency of your drills. A good core sample result will improve the extraction efficiency of your drill; the converse is also true. You'll only get a few attempts to run the core samples...watch your drill extraction rates and should you decide to invalidate your test results in order to gain more core sample attempts, choose wisely...";

        [KSPField]
        public int resourceType;

        [KSPField]
        public string analysisGUIName;

        [KSPField]
        public string analysisActionName;

        [KSPField]
        public string drillTechNode;

        [KSPField]
        public string analysisSkill;

        [KSPField]
        public float analysisTime;

        [KSPField]
        public string attemptStatus;

        [KSPField(guiActive = true, guiName = "Core Samples Left")]
        public string coreSampleStatus;

        public CoreSampleStates coreSampleState;
        private ModuleAnimationGroup drillAnimation;
        private Dictionary<string, ResourceData> resourceDataMap = new Dictionary<string, ResourceData>();
        private ModuleResourceHarvester harvester;
        private float analysisTimeRemaining;
        private ScreenMessage analysisStatusMsg;
        private bool invalidateConfirm;
        int coreSamplesRemaining = 8;

        public override void OnStart(StartState state)
        {
            base.OnStart(state);

            if (HighLogic.LoadedSceneIsFlight == false)
            {
                setupExperimentGUI();
                return;
            }

            //Get drill animation
            drillAnimation = this.part.FindModuleImplementing<ModuleAnimationGroup>();

            //Harvester
            harvester = this.part.FindModuleImplementing<ModuleResourceHarvester>();

            //Core sample state
            coreSampleState = CoreSampleStates.Ready;

            //If the biome has been unlocked yet then get the samples left
            if (situationIsValid() && Utils.IsBiomeUnlocked(this.part.vessel))
                coreSampleStatus = getSamplesLeft().ToString();
            else
                coreSampleStatus = kUnknown;

            //Setup the gui
            setupGUI();
        }

        [KSPAction("Take Core Sample")]
        public void TakeSampleAction(KSPActionParam param)
        {
            TakeSample();
        }

        [KSPEvent(guiActive = true, guiActiveEditor = false, guiName = "Run Analysis", active = true, externalToEVAOnly = false, unfocusedRange = 3.0f, guiActiveUnfocused = true)]
        public void TakeSample()
        {
            //If we aren't in the right situation for the resource type then we're done.
            if (situationIsValid() == false)
                return;

            //If the biome hasn't been unlocked yet then we're done.
            if (Utils.IsBiomeUnlocked(this.part.vessel) == false)
            {
                ScreenMessages.PostScreenMessage(kUnlockBiome, kMessageDuration, ScreenMessageStyle.UPPER_CENTER);
                return;
            }

            //If we're out of attempts then we're done.
            if (getSamplesLeft() == 0)
            {
                ScreenMessages.PostScreenMessage(kNoMoreAttempts, kWarningMsgDuration, ScreenMessageStyle.UPPER_CENTER);
                Events["InvalidateResults"].guiActive = true;
                Events["InvalidateResults"].guiActiveUnfocused = true;
                return;
            }

            //If the drill isn't deployed then deploy it before performing the analysis.
            if (drillAnimation != null)
            {
                if (drillAnimation.isDeployed == false)
                {
                    drillAnimation.DeployModule();
                    coreSampleState = CoreSampleStates.Deploying;
                    Events["TakeSample"].guiActive = false;
                    return;
                }
            }

            //If we're aren't drilling then take a sample
            if (harvester != null)
            {
                if (harvester.isActiveAndEnabled)
                {
                    performAnalysis();
                }
                else
                {
                    Events["TakeSample"].guiActive = false;
                    coreSampleState = CoreSampleStates.TakingSample;
                    harvester.StartResourceConverter();
                    analysisTimeRemaining = analysisTime;
                }
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (HighLogic.LoadedSceneIsFlight == false)
                return;

            //If the biome has been unlocked yet then get the samples left
            if (situationIsValid() && Utils.IsBiomeUnlocked(this.part.vessel))
                coreSampleStatus = getSamplesLeft().ToString();
            else
                coreSampleStatus = kUnknown;

            //Update the core sample state
            updateCoreSampleState();
        }

        #region Helpers
        protected int getSamplesLeft()
        {
             return coreSamplesRemaining;
        }

        protected void updateCoreSampleState()
        {
            string message;
            if (HighLogic.LoadedSceneIsFlight == false)
                return;

            switch (coreSampleState)
            {
                //If we're done deploying then start taking a core sample.
                case CoreSampleStates.Deploying:
                    if (drillAnimation != null)
                    {
                        if (drillAnimation.DeployAnimation.isPlaying == false)
                        {
                            //Get a small sample
                            if (harvester != null)
                                harvester.StartResourceConverter();

                            //Set analysis time
                            analysisTimeRemaining = analysisTime;

                            //Update state
                            coreSampleState = CoreSampleStates.TakingSample;
                        }
                    }
                    break;

                case CoreSampleStates.TakingSample:

                    //Use regular time to prevent timewarp cheating
                    analysisTimeRemaining -= Time.fixedDeltaTime;

                    if (analysisTimeRemaining <= 0.001f)
                    {
                        //Reset state
                        coreSampleState = CoreSampleStates.Retracting;
                        analysisStatusMsg = null;

                        //Stop harvesting
                        if (harvester != null)
                            harvester.StopResourceConverter();

                        //Retract harvester
                        drillAnimation.RetractModule();

                        //Perform analysis
                        performAnalysis();
                    }

                    else
                    {
                        message = kAnalysisStatus;

                        if (analysisStatusMsg == null)
                            analysisStatusMsg = ScreenMessages.PostScreenMessage(message, analysisTime, ScreenMessageStyle.UPPER_RIGHT);
                    }
                    break;

                case CoreSampleStates.Retracting:
                    if (drillAnimation != null)
                    {
                        if (drillAnimation.Fields["animationStatus"].guiActive == false)
                        {
                            //Reset GUI
                            coreSampleState = CoreSampleStates.Ready;
                            Events["TakeSample"].guiActive = true;
                        }
                    }
                    break;
            }
        }

        protected virtual void performAnalysis()
        {
            CBAttributeMapSO.MapAttribute biome = Utils.GetCurrentBiome(this.part.vessel);
            float experienceLevel = 0f;
            float analysisRoll = 0f;
            string analysisResultMessage;
            float efficiencyModifier = 0f;

            //Decrement the attempts remaining count
            coreSamplesRemaining = coreSamplesRemaining - 1;
            if (coreSamplesRemaining <= 0)
                coreSamplesRemaining = 0;
            coreSampleStatus = coreSamplesRemaining.ToString();

            UIPartActionWindow tweakableUI = Utils.FindActionWindow(this.part);
            if (tweakableUI != null)
                tweakableUI.displayDirty = true;

            //If an experienced scientist is taking the core sample, then the scientist's experience will
            //affect the analysis.
            if (FlightGlobals.ActiveVessel.isEVA)
            {
                Vessel vessel = FlightGlobals.ActiveVessel;
                Experience.ExperienceTrait experience = vessel.GetVesselCrew()[0].experienceTrait;

                if (experience.TypeName == analysisSkill)
                    experienceLevel = experience.CrewMemberExperienceLevel();
            }

            //Seed the random number generator
            UnityEngine.Random.seed = System.Environment.TickCount;

            //Roll 3d6 to approximate a bell curve, then convert it to a value between 1 and 100.
            analysisRoll = UnityEngine.Random.Range(1, 6);
            analysisRoll += UnityEngine.Random.Range(1, 6);
            analysisRoll += UnityEngine.Random.Range(1, 6);
            analysisRoll *= 5.5556f;

            //Now add the experience modifier
            analysisRoll += experienceLevel * kExperiencePercentModifier;

            //Since we're using a bell curve, anything below maxWorsenRoll worsens the biome's extraction rates.
            //Anything above minImprovementRoll improves the biome's extraction rates.
            //A skilled scientist can affect the modifier by as much as 5%.
            if (analysisRoll <= maxWorsenRoll)
            {
                //Calculate the modifier
                efficiencyModifier = -kBaseEfficiencyModifier * (1.0f - (experienceLevel / 100f));

                //Format the result message
                analysisResultMessage = string.Format(kResourceExtractionWorsened, Math.Abs((efficiencyModifier * 100.0f))) + biome.name;
            }

            //Good result!
            else if (analysisRoll >= minImprovementRoll)
            {
                //Calculate the modifier
                efficiencyModifier = kBaseEfficiencyModifier * (1.0f + (experienceLevel / 100f));

                //Format the result message
                analysisResultMessage = string.Format(kResourceExtractionImproved, Math.Abs((efficiencyModifier * 100.0f))) + biome.name;
            }

            else
            {
                analysisResultMessage = kResourceExtractionUnchanged + biome.name;
            }

            //Modify harvester
            harvester.Efficiency *= efficiencyModifier;
            WBIExtractionMonitor monitor = this.part.FindModuleImplementing<WBIExtractionMonitor>();
            monitor.efficiencyModifier = efficiencyModifier;

            //Inform the player of the result.
            ScreenMessages.PostScreenMessage(analysisResultMessage, 5.0f, ScreenMessageStyle.UPPER_CENTER);
            DeployExperiment();
        }

        protected virtual void setupGUI()
        {
            //If we've made all our attempts to modify the biome resources then hide the event.
            //Otherwise, set the GUI name.
            if (getSamplesLeft() > 0)
            {
                Events["InvalidateResults"].guiActive = false;
                Events["InvalidateResults"].guiActiveUnfocused = false;
            }
            else
            {
                Events["InvalidateResults"].guiActive = true;
                Events["InvalidateResults"].guiActiveUnfocused = true;
            }

            //Setup the gui names for taking samples.
            if (string.IsNullOrEmpty(analysisGUIName) == false)
                Events["TakeSample"].guiName = analysisGUIName;

            if (string.IsNullOrEmpty(analysisActionName) == false)
                Actions["TakeSampleAction"].guiName = analysisActionName;

            //The tech node for using the drills might not be unlocked yet.
            //Setup the drill GUI accordingly.
            setupDrillGUI();

            setupExperimentGUI();
        }

        protected void setupExperimentGUI()
        {
            Events["DeployExperiment"].guiActive = false;
            Events["DeployExperiment"].guiActiveUnfocused = false;
            Events["DeployExperimentExternal"].guiActive = false;
            Events["DeployExperimentExternal"].guiActiveUnfocused = false;

            foreach (BaseAction action in this.Actions)
            {
                if (action.name == "TakeSampleAction")
                    continue;

                action.actionGroup = KSPActionGroup.None;
                action.defaultActionGroup = KSPActionGroup.None;
                action.active = false;
            }
        }

        protected void setupDrillGUI()
        {
            if (Utils.HasResearchedNode(drillTechNode) == false)
            {
                ModuleResourceHarvester harvester = this.part.FindModuleImplementing<ModuleResourceHarvester>();
                foreach (BaseEvent baseEvent in harvester.Events)
                {
                    baseEvent.guiActive = false;
                    baseEvent.active = false;
                }
                foreach (BaseField field in harvester.Fields)
                    field.guiActive = false;
                foreach (BaseAction action in harvester.Actions)
                {
                    action.actionGroup = KSPActionGroup.None;
                    action.defaultActionGroup = KSPActionGroup.None;
                }

                ModuleAsteroidDrill astroDrill = this.part.FindModuleImplementing<ModuleAsteroidDrill>();
                foreach (BaseEvent baseEvent in astroDrill.Events)
                {
                    baseEvent.guiActive = false;
                    baseEvent.active = false;
                }
                foreach (BaseField field in astroDrill.Fields)
                    field.guiActive = false;
                foreach (BaseAction action in astroDrill.Actions)
                {
                    action.actionGroup = KSPActionGroup.None;
                    action.defaultActionGroup = KSPActionGroup.None;
                }
            }
        }

        protected bool situationIsValid()
        {
            HarvestTypes harvestType = (HarvestTypes)resourceType;
            bool isValid = true;
            string message = null;

            if (HighLogic.LoadedSceneIsFlight == false)
                return true;

            switch (this.part.vessel.situation)
            {
                case Vessel.Situations.LANDED:
                case Vessel.Situations.PRELAUNCH:
                    if (harvestType != HarvestTypes.Planetary && harvestType != HarvestTypes.Atmospheric)
                    {
                        isValid = false;
                        message = kNeedVesselLanded;
                    }
                    break;

                case Vessel.Situations.SPLASHED:
                    if (harvestType != HarvestTypes.Oceanic)
                    {
                        isValid = false;
                        message = kNeedVesselSplashed;
                    }
                    break;
            }

            //Inform the player if needed
            if (message != null)
                ScreenMessages.PostScreenMessage(message, kMessageDuration, ScreenMessageStyle.UPPER_CENTER);

            return isValid;
        }
        #endregion
    }
}
