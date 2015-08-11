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
    public class WBIUpgradableLab : WBIBasicScienceLab
    {
        protected float originalMinimumSuccess;
        protected float originalCriticalSuccess;
        protected float originalCriticalFail;
        protected float originalScientistBonus;
        protected double originalResearchTime;
        protected float currencyPerCycle;
        protected float originalSciencePerCycle;

        public override void OnLoad(ConfigNode node)
        {
            base.OnLoad(node);
            string value;

            //Copy the original values
            value = node.GetValue("minimumSuccess");
            if (string.IsNullOrEmpty(value) == false)
                originalMinimumSuccess = float.Parse(value);

            value = node.GetValue("criticalSuccess");
            if (string.IsNullOrEmpty(value) == false)
                originalCriticalSuccess = float.Parse(value);

            value = node.GetValue("criticalFail");
            if (string.IsNullOrEmpty(value) == false)
                originalCriticalFail = float.Parse(value);

            value = node.GetValue("scientistBonus");
            if (string.IsNullOrEmpty(value) == false)
                originalScientistBonus = float.Parse(value);

            value = node.GetValue("researchTime");
            if (string.IsNullOrEmpty(value) == false)
                originalResearchTime = float.Parse(value);

            value = node.GetValue("sciencePerCycle");
            if (string.IsNullOrEmpty(value) == false)
                sciencePerCycle = float.Parse(value);

            //Now apply the modifiers
            ApplyModifiers();
        }

        public void ResetParameters()
        {
            minimumSuccess = originalMinimumSuccess;
            criticalSuccess = originalCriticalSuccess;
            criticalFail = originalCriticalFail;
            scientistBonus = originalScientistBonus;
            researchTime = originalResearchTime;
            sciencePerCycle = originalSciencePerCycle;
            //TODO: fundsPerCycle, reputationPerCycle
        }

        public void ApplyModifiers()
        {
            List<WBILabUpgrade> upgrades = this.part.vessel.FindPartModulesImplementing<WBILabUpgrade>();

            //Reset parameters to original values
            ResetParameters();

            //Effects are cumulative for the active upgrades.
            foreach (WBILabUpgrade upgrade in upgrades)
            {
                if (upgrade.isActive)
                {
                    minimumSuccess += upgrade.minimumSuccessMod;

                    criticalSuccess += upgrade.criticalSuccessMod;

                    criticalFail += upgrade.criticalFailMod;

                    scientistBonus += upgrade.scientistBonusMod;

                    researchTime += upgrade.researchTimeMod;

                    //sciencePerCycle += upgrade.sciencePerCycleMod;
                }
            }
        }

    }
}
