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
    [KSPScenario(ScenarioCreationOptions.AddToAllGames, GameScenes.SPACECENTER, GameScenes.EDITOR, GameScenes.FLIGHT, GameScenes.TRACKSTATION)]
    public class WBIPathfinderScenario : ScenarioModule
    {
        private const string kToolTip = "ToolTip";

        public static WBIPathfinderScenario Instance;

        private List<ConfigNode> toolTipsList = new List<ConfigNode>();

        public override void OnAwake()
        {
            base.OnAwake();
            Instance = this;
        }

        public override void OnLoad(ConfigNode node)
        {
            base.OnLoad(node);
            ConfigNode[] toolTipsShown = node.GetNodes(kToolTip);

            foreach (ConfigNode toolTipNode in toolTipsShown)
                toolTipsList.Add(toolTipNode);
        }

        public override void OnSave(ConfigNode node)
        {
            base.OnSave(node);

            node.RemoveNodes(kToolTip);
            foreach (ConfigNode toolTipNode in toolTipsList)
                node.AddNode(toolTipNode);
        }

        public void SetToolTipShown(string toolTipName)
        {
            //If we've already set the tool tip then we're done.
            foreach (ConfigNode node in toolTipsList)
            {
                if (node.GetValue("name") == toolTipName)
                    return;
            }

            //Node does not exist, then add it.
            ConfigNode nodeTip = new ConfigNode(kToolTip);
            nodeTip.AddValue("name", toolTipName);
            toolTipsList.Add(nodeTip);
        }

        public bool HasShownToolTip(string toolTipName)
        {
            foreach (ConfigNode node in toolTipsList)
            {
                if (node.GetValue("name") == toolTipName)
                    return true;
            }

            return false;
        }

    }
}
