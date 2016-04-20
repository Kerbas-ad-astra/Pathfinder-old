﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using KSP.IO;

/*
Source code copyright 2016, by Michael Billard (Angel-125)
License: CC BY-NC-SA 4.0
License URL: https://creativecommons.org/licenses/by-nc-sa/4.0/
Wild Blue Industries is trademarked by Michael Billard and may be used for non-commercial purposes. All other rights reserved.
Note that Wild Blue Industries is a ficticious entity 
created for entertainment purposes. It is in no way meant to represent a real entity.
Any similarity to a real entity is purely coincidental.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
namespace WildBlueIndustries
{
    public class WBIConestoga : WBIMeshHelper
    {
        [KSPField]
        public string groundOpsObjects;

        [KSPField]
        public string orbitalOpsObjects;

        [KSPField]
        public string orbitalNodesToKeep;

        [KSPField(isPersistant = true)]
        public bool groundOpsMode;

        string[] groundObjectNames;
        string[] orbitalObjectNames;
        List<AttachNode> groundNodes = new List<AttachNode>();

        [KSPEvent(guiActiveEditor = true, guiName = "ToggleConfig")]
        public void ToggleConfig()
        {
            groundOpsMode = !groundOpsMode;

            setupGUI();
            setupModules();
            setVisibleObjects();

            if (HighLogic.LoadedSceneIsEditor && groundOpsMode)
            {
                foreach (AttachNode node in groundNodes)
                {
                    if (this.part.attachNodes.Contains(node) == false)
                        this.part.attachNodes.Add(node);
                }
            }

            setupNodes();
        }

        protected override void getProtoNodeValues(ConfigNode protoNode)
        {
            base.getProtoNodeValues(protoNode);

            if (protoNode.HasValue("groundOpsObjects"))
                groundOpsObjects = protoNode.GetValue("groundOpsObjects");
            if (protoNode.HasValue("orbitalOpsObjects"))
                orbitalOpsObjects = protoNode.GetValue("orbitalOpsObjects");

            if (string.IsNullOrEmpty(groundOpsObjects) == false)
                groundObjectNames = groundOpsObjects.Split(';');
            if (string.IsNullOrEmpty(orbitalOpsObjects) == false)
                orbitalObjectNames = orbitalOpsObjects.Split(';');
        }

        public override void OnStart(StartState state)
        {
            base.OnStart(state);

            if (HighLogic.LoadedSceneIsEditor)
            {
                foreach (AttachNode node in this.part.attachNodes)
                {
                    if (orbitalNodesToKeep.Contains(node.id) == false)
                        groundNodes.Add(node);
                }
            }

            setupGUI();
            setupModules();
            setVisibleObjects();
            setupNodes();

            if (HighLogic.LoadedSceneIsEditor == false)
            {
                this.enabled = false;
                this.isEnabled = false;
            }
        }

        protected void setupNodes()
        {
            if (groundOpsMode)
                return;
            if (string.IsNullOrEmpty(orbitalNodesToKeep))
                return;

            List<AttachNode> doomedNodes = new List<AttachNode>();

            foreach (AttachNode node in this.part.attachNodes)
            {
                if (orbitalNodesToKeep.Contains(node.id) == false)
                    doomedNodes.Add(node);
            }

            foreach (AttachNode doomed in doomedNodes)
                this.part.attachNodes.Remove(doomed);
        }

        protected void setupGUI()
        {
            if (HighLogic.LoadedSceneIsEditor == false)
            {
                Events["ToggleConfig"].active = false;
                return;
            }

            if (groundOpsMode)
                Events["ToggleConfig"].guiName = "Configure for orbit";
            else
                Events["ToggleConfig"].guiName = "Configure for ground";
        }

        protected void setupModules()
        {
            //Command
            ModuleCommand command = this.part.FindModuleImplementing<ModuleCommand>();
            if (command != null)
            {
                command.enabled = groundOpsMode;
                command.isEnabled = groundOpsMode;
            }

            //Solar Panel
            ModuleDeployableSolarPanel solarPanel = this.part.FindModuleImplementing<ModuleDeployableSolarPanel>();
            if (solarPanel != null)
            {
                solarPanel.enabled = groundOpsMode;
                solarPanel.isEnabled = groundOpsMode;
            }

            //Reaction Wheel
            ModuleReactionWheel reactionWheel = this.part.FindModuleImplementing<ModuleReactionWheel>();
            if (reactionWheel != null)
            {
                reactionWheel.enabled = groundOpsMode;
                reactionWheel.isEnabled = groundOpsMode;
            }

            //SAS
            ModuleSAS sas = this.part.FindModuleImplementing<ModuleSAS>();
            if (sas != null)
            {
                sas.enabled = groundOpsMode;
                sas.isEnabled = groundOpsMode;
            }
        }

        protected void setVisibleObjects()
        {
            List<int> visibleObjects = new List<int>();

            if (groundOpsMode)
            {
                foreach (string obj in groundObjectNames)
                    visibleObjects.Add(meshIndexes[obj]);
            }

            else
            {
                foreach (string obj in orbitalObjectNames)
                    visibleObjects.Add(meshIndexes[obj]);
            }

            setObjects(visibleObjects);
        }
    }
}
