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
    public class PathfinderSettings : Window<PathfinderSettings>
    {
        string settingsPath;
        public bool payToRemodel;
        public bool requireSkillCheck;

        public PathfinderSettings() :
        base("Pathfinder Settings", 320, 100)
        {
            Resizable = false;
            settingsPath = AssemblyLoader.loadedAssemblies.GetPathByType(typeof(PathfinderSettings)) + "/Settings.cfg";
            loadSettings();
        }

        protected override void DrawWindowContents(int windowId)
        {
            GUILayout.BeginVertical();
            payToRemodel = GUILayout.Toggle(payToRemodel, "Require resources to reconfigure modules.");
            requireSkillCheck = GUILayout.Toggle(requireSkillCheck, "Require skill check to reconfigure modules.");
            GUILayout.EndVertical();

            WBIMultiConverter.payForReconfigure = payToRemodel;
            WBIMultiConverter.checkForSkill = requireSkillCheck;
        }

        public override void SetVisible(bool newValue)
        {
            base.SetVisible(newValue);
            ConfigNode nodeSettings = new ConfigNode();

            if (newValue)
            {
                loadSettings();
            }

            else
            {
                nodeSettings.name = "SETTINGS";
                nodeSettings.AddValue("payToRemodel", payToRemodel.ToString());
                nodeSettings.AddValue("requireSkillCheck", requireSkillCheck.ToString());
                nodeSettings.Save(settingsPath);
            }
        }

        protected void loadSettings()
        {
            ConfigNode nodeSettings = new ConfigNode();
            string value;

            //Now load settings
            nodeSettings = ConfigNode.Load(settingsPath);
            if (nodeSettings != null)
            {
                value = nodeSettings.GetValue("payToRemodel");
                if (string.IsNullOrEmpty(value) == false)
                    payToRemodel = bool.Parse(value);
                else
                    payToRemodel = WBIMultiConverter.payForReconfigure;

                value = nodeSettings.GetValue("requireSkillCheck");
                if (string.IsNullOrEmpty(value) == false)
                    requireSkillCheck = bool.Parse(value);
                else
                    requireSkillCheck = WBIMultiConverter.checkForSkill;
            }
            else
            {
                payToRemodel = WBIMultiConverter.payForReconfigure;
                requireSkillCheck = WBIMultiConverter.checkForSkill;
            }

            WBIMultiConverter.payForReconfigure = payToRemodel;
            WBIMultiConverter.checkForSkill = requireSkillCheck;
        }

    }
}
