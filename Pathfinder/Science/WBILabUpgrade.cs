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
    public class WBILabUpgrade : PartModule
    {
        [KSPField]
        public string activeGUIName;

        [KSPField]
        public string inactiveGUIName;

        [KSPField]
        public float minimumSuccessMod;

        [KSPField]
        public float criticalSuccessMod;

        [KSPField]
        public float criticalFailMod;

        [KSPField]
        public float scientistBonusMod;

        [KSPField]
        public double researchTimeMod;

        [KSPField]
        public float currencyPerCycleMod;

        [KSPField]
        public int currencyGenerated;

        [KSPField(isPersistant = true)]
        public bool isActive;

        [KSPField(isPersistant = true)]
        public float lifeTime;

        [KSPEvent(guiActive = true, guiActiveUnfocused = true, unfocusedRange = 5f)]
        public void ToggleActive()
        {
            isActive = !isActive;

            if (isActive)
                Events["ToggleActive"].guiName = inactiveGUIName;
            else
                Events["ToggleActive"].guiName = activeGUIName;

            //Inform the upgradable lab
            List<WBIUpgradableLab> labs = this.part.vessel.FindPartModulesImplementing<WBIUpgradableLab>();
            foreach (WBIUpgradableLab lab in labs)
                lab.ApplyModifiers();
        }

        [KSPAction("Toggle Active")]
        public void ToggleActiveAction(KSPActionParam param)
        {
            ToggleActive();
        }

        public override void OnStart(StartState state)
        {
            base.OnStart(state);

            if (isActive)
                Events["ToggleActive"].guiName = inactiveGUIName;
            else
                Events["ToggleActive"].guiName = activeGUIName;
        }
    }
}
