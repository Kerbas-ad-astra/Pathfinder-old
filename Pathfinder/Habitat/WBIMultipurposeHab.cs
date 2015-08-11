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
    public class WBIMultipurposeHab : WBIMultiConverter
    {
        private const string kPonderosaModule = "PonderosaModule";
        private const string kToolTip = "Want to use the Ponderosa for more than one purpose? With a feat of engineering, you can change it in the field. For a price...\r\n\r\n";
        private const string kSettingsWindow = "Settings Window";
        private const string kPartsTip = "Don't want to pay to redecorate? No problem. Just press Mod P (the modifier key defaults to the Alt key on Windows) to open the Settings window and uncheck the option.\r\n\r\n";
        private const string kPonderosaOpsView = "Ponderosa Operations";

        Animation anim;
        PartModule impactSeismometer;

        public override void OnStart(StartState state)
        {
            base.OnStart(state);

            if (string.IsNullOrEmpty(animationName))
                return;
            anim = this.part.FindModelAnimators(animationName)[0];

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

            if (anim == null)
                return;

            //We're only interested in the act of inflating the module.
            if (isDeployed == false)
            {
                animationStarted = false;
                return;
            }

            //If we've completed the animation then we are done.
            if (animationStarted == false)
                return;

            //Animation may not be done yet.
            if (anim.isPlaying)
                return;

            //At this point we know that the animation was playing but has now stopped.
            //We also know that the animation was started. Now reset the flag.
            animationStarted = false;

            //Hide seismometer
            if (impactSeismometer != null)
            {
                IScienceDataContainer container = (IScienceDataContainer)impactSeismometer;

                BaseEvent reviewEvent = impactSeismometer.Events["reviewEvent"];
                if (reviewEvent != null)
                {
                    reviewEvent.guiActive = false;
                    reviewEvent.guiActiveUnfocused = false;
                }

                //clear out the impact data if the current template isn't a geology lab
                if (CurrentTemplateName != "Geology Lab")
                {
                    ScienceData[] dataItems = container.GetData();
                    if (dataItems != null)
                        foreach (ScienceData doomed in dataItems)
                            container.DumpData(doomed);
                }
            }

            checkAndShowToolTip();
        }

        public override void UpdateContentsAndGui(string templateName)
        {
            ModuleScienceContainer scienceContainer = this.part.FindModuleImplementing<ModuleScienceContainer>();
            ScienceData[] dataQueue = scienceContainer.GetData();
            if (dataQueue != null)
            {
                if (dataQueue.Length > 0)
                {
                    scienceContainer.Events["ReviewDataEvent"].guiActiveUnfocused = true;
                    scienceContainer.Events["ReviewDataEvent"].guiActive = true;
                }
            }

            base.UpdateContentsAndGui(templateName);

            //Check to see if we've displayed the tooltip for the template.

            //First, we're only interested in deployed modules.
            if (isDeployed == false)
                return;

            //Now check
            checkAndShowToolTip();
        }

        protected override void notEnoughParts()
        {
            base.notEnoughParts();

            WBIPathfinderScenario scenario = WBIPathfinderScenario.Instance;

            //Add first time for redecoration
            if (scenario.HasShownToolTip(kSettingsWindow) == false)
            {
                scenario.SetToolTipShown(kSettingsWindow);

                WBIToolTipWindow toolTipWindow = new WBIToolTipWindow(kSettingsWindow, kPartsTip);
                toolTipWindow.SetVisible(true);
            }
        }

        protected override bool canAffordReconfigure(string templateName)
        {
            WBIPathfinderScenario scenario = WBIPathfinderScenario.Instance;
            bool canAfford = base.canAffordReconfigure(templateName);

            //Add first time for redecoration
            if (!canAfford && scenario.HasShownToolTip(kSettingsWindow) == false)
            {
                scenario.SetToolTipShown(kSettingsWindow);

                WBIToolTipWindow toolTipWindow = new WBIToolTipWindow(kSettingsWindow, kPartsTip);
                toolTipWindow.SetVisible(true);
            }

            return canAfford;
        }

        protected void checkAndShowToolTip()
        {
            //Now we can check to see if the tooltip for the current template has been shown.
            WBIPathfinderScenario scenario = WBIPathfinderScenario.Instance;
            if (scenario.HasShownToolTip(CurrentTemplateName))
                return;

            //Tooltip for the current template has never been shown. Show it now.
            string toolTipTitle = CurrentTemplate.GetValue("toolTipTitle");
            string toolTip = CurrentTemplate.GetValue("toolTip");

            //Add the very first ponderosa module tool tip.
            if (scenario.HasShownToolTip(kPonderosaModule) == false)
            {
                toolTip = kToolTip + toolTip;

                scenario.SetToolTipShown(kPonderosaModule);
            }

            WBIToolTipWindow toolTipWindow = new WBIToolTipWindow(toolTipTitle, toolTip);
            toolTipWindow.SetVisible(true);

            //Cleanup
            scenario.SetToolTipShown(CurrentTemplateName);
        }

        protected override void createModuleOpsView()
        {
            base.createModuleOpsView();
            moduleOpsView.WindowTitle = kPonderosaOpsView;
        }
    }
}
