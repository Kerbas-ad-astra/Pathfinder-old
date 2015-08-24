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
    public class WBIGeoThermalPlant : WBIBreakableResourceConverter
    {
        [KSPField]
        public string planetEfficiencies;// = "0,0;1,1;2,0.5;3,0.3;4,0.45;5,.75;6,1;7,1;8,0;9,1;10,.7;11,.4;12,.1;13,.1;14,.25;15,.1;16,.1";

        Dictionary<int, float> efficiencyModifiers = new Dictionary<int, float>();

        public override void OnStart(StartState state)
        {
            base.OnStart(state);

            loadValuesFromConfig();

            //Parse planet efficiencies
            string[] efficiencies = planetEfficiencies.Split(new char[] { ';' });
            string[] efficiencyID;

            foreach (string efficiency in efficiencies)
            {
                efficiencyID = efficiency.Split(new char[] { ',' });

                efficiencyModifiers.Add(int.Parse(efficiencyID[0]), float.Parse(efficiencyID[1]));
            }

            //Now set the efficiency based upon planet.
            int planetID = this.part.vessel.mainBody.flightGlobalsIndex;
            if (efficiencyModifiers.ContainsKey(planetID))
            {
                this.Efficiency = efficiencyModifiers[planetID];
            }

            efficiencyString = "Geothermal Efficiency";
        }

        protected void loadValuesFromConfig()
        {
            Debug.Log("FRED loadValuesFromConfig called.");
            string value;
            WBIModuleSwitcher switcher = this.part.FindModuleImplementing<WBIModuleSwitcher>();

            ConfigNode[] moduleNodes = switcher.CurrentTemplate.GetNodes("MODULE");
            foreach (ConfigNode moduleNode in moduleNodes)
            {
                Debug.Log("FRED moduleNode" + moduleNode);
                value = moduleNode.GetValue("name");
                if (value.Contains(this.ClassName))
                {
                    if (moduleNode.HasValue("planetEfficiencies"))
                    {
                        planetEfficiencies = moduleNode.GetValue("planetEfficiencies");
                        return;
                    }
                }
            }
        }

    }
}
