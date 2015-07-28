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
    public class WBIOSEWorkshop : ExtendedPartModule, ITemplateOps
    {
        PartModule oseWorkshop;
        PartModule oseRecycler;

        public override void OnStart(StartState state)
        {
            base.OnStart(state);

            //Get the workshop and recycler
            foreach (PartModule mod in this.part.Modules)
            {
                if (mod.moduleName == "OseModuleWorkshop")
                    oseWorkshop = mod;
                else if (mod.moduleName == "OseModuleRecycler")
                    oseRecycler = mod;
            }

            //Now, hide the workshop and recycler GUI.
            oseWorkshop.Fields["Status"].guiActive = false;
            oseWorkshop.Fields["Status"].guiActiveEditor = false;
            oseWorkshop.Events["ContextMenuOnOpenWorkbench"].guiActive = false;
            oseWorkshop.Events["ContextMenuOnOpenWorkbench"].guiActiveEditor = false;
            oseWorkshop.Events["ContextMenuOnOpenWorkbench"].guiActiveUnfocused = false;

            oseRecycler.Fields["Status"].guiActive = false;
            oseRecycler.Fields["Status"].guiActiveEditor = false;
            oseRecycler.Events["ContextMenuOnOpenWorkbench"].guiActive = false;
            oseRecycler.Events["ContextMenuOnOpenWorkbench"].guiActiveEditor = false;
            oseRecycler.Events["ContextMenuOnOpenWorkbench"].guiActiveUnfocused = false;
        }

        public void DrawOpsWindow()
        {
            string workshopStatus = (string)Utils.GetField("Status", oseWorkshop);
            string recyclerStatus = (string)Utils.GetField("Status", oseRecycler);

            GUILayout.BeginVertical();
            GUILayout.Label("<b>Workshop Status:</b> " + workshopStatus);
            GUILayout.Label("<b>Recycler Status:</b> " + recyclerStatus);

            if (GUILayout.Button("Open Workshop"))
                oseWorkshop.Events["ContextMenuOnOpenWorkbench"].Invoke();

            if (GUILayout.Button("Open Recycler"))
                oseRecycler.Events["ContextMenuOnOpenWorkbench"].Invoke();

            GUILayout.EndVertical();
        }
    }
}
