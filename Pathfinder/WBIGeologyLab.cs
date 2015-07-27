using System;
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
    public class WBIGeologyLab : WBIBasicScienceLab, ITemplateOps
    {
        const float kSoilAnalysisFactor = 0.75f;
        const string kSoilScienceAdded = "Analysis has generated {0:f2} Science!";
        const string kNoScientistsOrTerrain = "Unable to perform the analysis, there are no scientists staffing the lab, and no T.E.R.R.A.I.N. satellites in orbit.";
        const string kAnalysisUplink = "Soil analysis uplinked to KSC via T.E.R.R.A.I.N. and analyzed at KSC.";
        const string kNoCrew = "At least one cremember must staff the lab in order to perform the analysis.";
        const string kNoScientists = "At least one Scientist must staff the lab in order to perform the analysis.";

        ModuleBiomeScanner biomeScanner;
        ModuleGPS gps;
        PartModule impactSeismometer;
        private Vector2 scrollPosResources;
        List<PResource.Resource> resourceList;

        #region Overrides
        public override void OnStart(StartState state)
        {
            base.OnStart(state);
            SetGuiVisible(false);

            resourceList = ResourceMap.Instance.GetResourceItemList(HarvestTypes.Planetary, this.part.vessel.mainBody);

            gps = this.part.FindModuleImplementing<ModuleGPS>();
            biomeScanner = this.part.FindModuleImplementing<ModuleBiomeScanner>();
            hideStockGUI();

            foreach (PartModule mod in this.part.Modules)
                if (mod.moduleName == "Seismometer")
                {
                    impactSeismometer = mod;
                    break;
                }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (impactSeismometer != null)
            {
                BaseEvent reviewEvent = impactSeismometer.Events["reviewEvent"];
                if (reviewEvent != null)
                {
                    reviewEvent.guiActive = false;
                    reviewEvent.guiActiveUnfocused = false;
                }
            }

        }
        #endregion

        #region Helpers
        protected void performSoilAnalysis()
        {
            //We need at least one crewmember in the lab.
            if (this.part.protoModuleCrew.Count == 0)
            {
                ScreenMessages.PostScreenMessage(kNoCrew, kMessageDuration, ScreenMessageStyle.UPPER_CENTER);
                return;
            }

            //We need at least one scientist in the lab, or one TERRAIN satellite in orbit.
            float scienceBonus = getSoilAnalysisBonus();

            //We can run the analysis, add the science bonus
            if (scienceBonus > 0.0f)
            {
                ResearchAndDevelopment.Instance.AddScience(scienceBonus, TransactionReasons.Any);
                ScreenMessages.PostScreenMessage(string.Format(kSoilScienceAdded, scienceBonus), kMessageDuration, ScreenMessageStyle.UPPER_CENTER);
            }

            //Ok, do we at least have a TERRAIN satellite in orbit?
            else if (planetHasTerrainSat())
            {
                ScreenMessages.PostScreenMessage(kAnalysisUplink, kMessageDuration * 1.5f, ScreenMessageStyle.UPPER_CENTER);
            }

            else
            {
                ScreenMessages.PostScreenMessage(kNoScientistsOrTerrain, kMessageDuration * 1.5f, ScreenMessageStyle.UPPER_CENTER);
                return;
            }

            //Run the analysis
            biomeScanner.RunAnalysis();
        }

        protected bool planetHasTerrainSat()
        {
            foreach (Vessel vessel in FlightGlobals.Vessels)
            {
                if (vessel == this.part.vessel)
                    continue;
                if (vessel.mainBody != this.part.vessel.mainBody)
                    continue;
                if (vessel.situation != Vessel.Situations.ORBITING)
                    continue;

                ProtoVessel protoVessel = vessel.protoVessel;
                foreach (ProtoPartSnapshot partSnapshot in protoVessel.protoPartSnapshots)
                {
                    foreach (ProtoPartModuleSnapshot moduleSnapshot in partSnapshot.modules)
                    {
                        if (moduleSnapshot.moduleName == "GeoSurveyCamera")
                            return true;
                    }
                }
            }

            return false;
        }

        protected float getSoilAnalysisBonus()
        {
            float bonus = 0f;

            foreach (ProtoCrewMember crewMember in this.part.protoModuleCrew)
                if (crewMember.experienceTrait.TypeName == "Scientist")
                {
                    //One point for being a scientist.
                    bonus += 1.0f;

                    //One point for each experience level.
                    bonus += crewMember.experienceLevel;
                }

            return bonus * kSoilAnalysisFactor;
        }

        protected void hideStockGUI()
        {
            //GPS
            gps.Fields["bioName"].guiActive = false;
            gps.Fields["body"].guiActive = false;
            gps.Fields["lat"].guiActive = false;
            gps.Fields["lon"].guiActive = false;

            //Biome Scanner
            biomeScanner.Events["RunAnalysis"].guiActive = false;
            biomeScanner.Events["RunAnalysis"].guiActiveUnfocused = false;
        }
        #endregion

        #region ITemplateOps

        public void DrawOpsWindow()
        {
            GUILayout.BeginVertical();
            GUILayout.Label("<b>Body:</b> " + gps.body + " <b>Biome:</b> " + gps.bioName);
            GUILayout.Label("<b>Lon:</b> " + gps.lon + " <b>Lat:</b> " + gps.lat);

            GUILayout.Label("Local Abundance");
            scrollPosResources = GUILayout.BeginScrollView(scrollPosResources, new GUIStyle(GUI.skin.textArea));
            foreach (PResource.Resource resource in resourceList)
                GUILayout.Label(resource.resourceName + " abundance: " + getAbundance(resource.resourceName));
            GUILayout.EndScrollView();

            if (Utils.IsBiomeUnlocked(this.part.vessel) == false)
                if (GUILayout.Button("Perform soil analysis"))
                    performSoilAnalysis();

            if (impactSeismometer != null)
            {
                IScienceDataContainer impactSensor = (IScienceDataContainer)impactSeismometer;

                ScienceData[] sciData = impactSensor.GetData();
                if (sciData != null)
                    if (sciData.Length > 0)
                        if (GUILayout.Button("Review Impact Data"))
                        {
                            if (scientistIsAboard())
                                impactSensor.ReviewData();
                            else
                                ScreenMessages.PostScreenMessage(kNoScientists, kMessageDuration, ScreenMessageStyle.UPPER_CENTER);
                        }
            }
            GUILayout.EndVertical();
        }

        protected bool scientistIsAboard()
        {
            foreach (ProtoCrewMember crewMember in this.part.protoModuleCrew)
                if (crewMember.experienceTrait.TypeName == "Scientist")
                    return true;

            return false;
        }

        protected string getAbundance(string resourceName)
        {
            AbundanceRequest request = new AbundanceRequest();
            double lattitude = ResourceUtilities.Deg2Rad(this.part.vessel.latitude);
            double longitude = ResourceUtilities.Deg2Rad(this.part.vessel.longitude);

            request.BiomeName = Utils.GetCurrentBiome(this.part.vessel).name;
            request.BodyId = this.part.vessel.mainBody.flightGlobalsIndex;
            request.Longitude = longitude;
            request.Latitude = lattitude;
            request.CheckForLock = true;
            request.ResourceName = resourceName;

            float abundance = ResourceMap.Instance.GetAbundance(request) * 100.0f;

            if (abundance > 0.001)
                return string.Format("{0:f2}%", abundance);
            else
                return "Unknown";
        }
        #endregion

    }
}
