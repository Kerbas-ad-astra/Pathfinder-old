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
    public class DrillSwitchWindow : Window<DrillSwitchWindow>
    {
        private const string kInsufficientResources = "Unable to reconfigure the drill. You need {0:f2} {1:s} to reconfigure it.";
        private const string kInsufficientSkill = "Unable to reconfigure the drill. You need a skilled {0:s}. to reconfigure it.";
        private const string kDrillReconfigured = "Drill reconfigured";
        private const int kWindowWidth = 300;
        private const int kWindowHeight = 310;
        private const int kDrillLabelWidth = 220;

        public List<ModuleResourceHarvester> groundDrills;
        public List<PResource.Resource> resourceList;
        public Part part;

        public string requiredResource;
        public float reconfigureCost;
        public string requiredSkill;

        private Vector2 scrollPos;
        private List<int> groundDrillResourceIndexes = new List<int>();

        public DrillSwitchWindow(string title = "Drill Modifications") :
            base(title, kWindowWidth, kWindowHeight)
        {
            WindowTitle = title;
            Resizable = false;
        }

        public override void SetVisible(bool newValue)
        {
            PResource.Resource res;
            int index;
            base.SetVisible(newValue);

            if (newValue)
            {
                //Get the list of resources for the biome
                resourceList = ResourceMap.Instance.GetResourceItemList(HarvestTypes.Planetary, this.part.vessel.mainBody);

                //For each drill, find the index of the resource that it drills for.
                foreach (ModuleResourceHarvester drill in groundDrills)
                {
                    for (index = 0; index < resourceList.Count; index++)
                    {
                        res = resourceList[index];
                        if (drill.ResourceName == res.resourceName)
                        {
                            groundDrillResourceIndexes.Add(index);
                            break;
                        }
                    }
                }
            }
        }

        protected override void DrawWindowContents(int windowId)
        {

            GUILayout.BeginVertical();
            scrollPos = GUILayout.BeginScrollView(scrollPos, new GUIStyle(GUI.skin.textArea));

            drawGroundDrills();

            if (GUILayout.Button("Reconfigure Drill"))
                reconfigureDrill();

            GUILayout.EndScrollView();
            GUILayout.EndVertical();
        }

        protected void reconfigureDrill()
        {
            //If required, make sure we have the proper skill
            if (PathfinderSettings.requireSkillCheck)
            {
                if (FlightGlobals.ActiveVessel.isEVA)
                {
                    Vessel vessel = FlightGlobals.ActiveVessel;
                    Experience.ExperienceTrait experience = vessel.GetVesselCrew()[0].experienceTrait;

                    if (experience.TypeName != requiredSkill)
                    {
                        ScreenMessages.PostScreenMessage(string.Format(kInsufficientSkill, requiredSkill), 5.0f, ScreenMessageStyle.UPPER_CENTER);
                        return;
                    }
                }
            }

            //If needed, pay the cost to reconfigure
            if (PathfinderSettings.payToRemodel)
            {
                //Make sure we can afford it
                PartResourceDefinition definition = ResourceHelper.DefinitionForResource(requiredResource);

                //Pay for the reconfiguration cost.
                double partsPaid = this.part.RequestResource(definition.id, reconfigureCost, ResourceFlowMode.ALL_VESSEL);

                //Could we afford it?
                if (Math.Abs(partsPaid) / Math.Abs(reconfigureCost) < 0.999f)
                {
                    ScreenMessages.PostScreenMessage(string.Format(kInsufficientResources, reconfigureCost, requiredResource), 6.0f, ScreenMessageStyle.UPPER_CENTER);

                    //Put back what we took
                    this.part.RequestResource(definition.id, -partsPaid, ResourceFlowMode.ALL_VESSEL);
                    return;
                }
            }

            //Now reconfigure the drill.
            ModuleResourceHarvester drill;
            PResource.Resource res;
            for (int drillIndex = 0; drillIndex < groundDrills.Count; drillIndex++)
            {
                drill = groundDrills[drillIndex];
                res = resourceList[drillIndex];
                setupDrillGUI(drill, res);
            }
            ScreenMessages.PostScreenMessage(kDrillReconfigured, 5.0f, ScreenMessageStyle.UPPER_CENTER);
        }

        protected void drawGroundDrills()
        {
            ModuleResourceHarvester drill;
            PResource.Resource res;
            int index;

            for (index = 0; index < groundDrills.Count; index++)
            {
                drill = groundDrills[index];

                //Previous resource
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("<", GUILayout.Width(20)))
                {
                    groundDrillResourceIndexes[index] -= 1;

                    if (groundDrillResourceIndexes[index] < 0)
                        groundDrillResourceIndexes[index] = resourceList.Count - 1;

//                    res = resourceList[groundDrillResourceIndexes[index]];
//                    setupDrillGUI(drill, res);
                }

                //Drill labeling
                res = resourceList[groundDrillResourceIndexes[index]];
                GUILayout.Label("Resource: " + res.resourceName, GUILayout.Width(kDrillLabelWidth));

                //Next resource
                if (GUILayout.Button(">", GUILayout.Width(20)))
                {
                    groundDrillResourceIndexes[index] += 1;

                    if (groundDrillResourceIndexes[index] >= resourceList.Count)
                        groundDrillResourceIndexes[index] = 0;
                }
                GUILayout.EndHorizontal();
            }
        }

        protected void setupDrillGUI(ModuleResourceHarvester drill, PResource.Resource res)
        {
            drill.ResourceName = res.resourceName;
            drill.StartActionName = "Start " + res.resourceName + " drill";
            drill.StopActionName = "Stop " + res.resourceName + " drill";
        }
    }
}
