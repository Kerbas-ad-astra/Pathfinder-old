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
    public enum ScienceLabModes
    {
        researchMode,
        resourceMode,
        productionMode
    }

    public class WBIConvertibleSciLab : ExtendedPartModule //WBIInflatablePartModule
    {
        WBIScienceConverter scienceConverter;
        FakeExperimentResults fakeExperiment;
        ModuleScienceContainer container;
        WBIResultsDialogSwizzler swizzler;
        TransmitHelper transmitHelper;

        [KSPEvent(guiActive = true, guiName = "Add Data")]
        public void ToggleLabMode()
        {
            /*
            ScienceExperiment experiment = ResearchAndDevelopment.GetExperiment("WBIBiomeAnalysis");
            ScienceSubject subject = ResearchAndDevelopment.GetExperimentSubject(experiment, ScienceUtil.GetExperimentSituation(this.part.vessel),
                this.part.vessel.mainBody, Utils.GetCurrentBiome(this.part.vessel).name);
            ScienceSubject subjectLEO = ResearchAndDevelopment.GetExperimentSubject(experiment, ExperimentSituations.InSpaceLow,
                FlightGlobals.GetHomeBody(), "");

            //This ensures you can re-run the experiment.
            subjectLEO.science = 0f;
            subjectLEO.scientificValue = 1f;

            ScienceData data = new ScienceData(20, 1f, 0f, subjectLEO.id, subject.title);
            container.AddData(data);
             */

            ScienceData data = WBIBiomeAnalysis.CreateData(this.part, 100f);
            container.AddData(data);
        }

        [KSPEvent(guiActive = true, guiName = "Me Review Data")]
        public void MeReviewData()
        {
            container.ReviewData();

            //Swizzle the callbacks
            swizzler.SwizzleResultsDialog();
        }

        public bool MeDiscard(ScienceData data)
        {
            ScreenMessages.PostScreenMessage("Me Discard " + data.subjectID, 3.0f, ScreenMessageStyle.UPPER_CENTER);
            return true;
        }

        public bool MeTransmit(ScienceData data)
        {
            ScreenMessages.PostScreenMessage("Me Transmit " + data.subjectID, 3.0f, ScreenMessageStyle.UPPER_CENTER);
            if (data.subjectID.Contains("WBIBiomeAnalysis"))
            {
                swizzler.Discard(data);
                return false;
            }

            return true;
        }

        public void Transmit(ScienceData data)
        {
            if (transmitHelper.TransmitToKSC(data))
                ScreenMessages.PostScreenMessage("Transmit transmitted data for " + data.subjectID, 5.0f, ScreenMessageStyle.UPPER_CENTER);
        }

        public override void OnStart(StartState state)
        {
            base.OnStart(state);
            fakeExperiment = new FakeExperimentResults();
            fakeExperiment.part = this.part;
            fakeExperiment.transmitDelegate = Transmit;

            transmitHelper = new TransmitHelper();
            transmitHelper.part = this.part;

            container = this.part.FindModuleImplementing<ModuleScienceContainer>();
            scienceConverter = this.part.FindModuleImplementing<WBIScienceConverter>();

            swizzler = new WBIResultsDialogSwizzler();
            swizzler.onDiscard = MeDiscard;
            swizzler.onTransmit = MeTransmit;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            scienceConverter = this.part.FindModuleImplementing<WBIScienceConverter>();
            
        }
    }
}
