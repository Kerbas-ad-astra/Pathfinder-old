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
    public class CommercialResultsDialog : Window<CommercialResultsDialog>
    {
        const int kWindowWidth = 420;
        const int kWindowHeight = 320;

        public float transmitBonus = 1f;
        public float publishBonus = 1f;
        public float sellBonus = 1f;
        public TransmitHelper transmitHelper;
        public ModuleScienceContainer scienceContainer;
        public Part part;
        public List<string> fakedExperiments = new List<string>();

        Texture keepIcon;
        Texture publishIcon;
        Texture sellIcon;
        Texture keepIconWhite;
        Texture publishIconWhite;
        Texture sellIconWhite;
        int currentIndex;
        ScienceData[] dataQueue;
        private Vector2 _scrollPos;

        public CommercialResultsDialog(Part host, string title = "Commercial Science Review") :
            base(title, kWindowWidth, kWindowHeight)
        {
            WindowTitle = title;
            Resizable = false;
            HideCloseButton = true;

            keepIcon = GameDatabase.Instance.GetTexture("WildBlueIndustries/Pathfinder/Icons/WBIKeep", false);
            publishIcon = GameDatabase.Instance.GetTexture("WildBlueIndustries/Pathfinder/Icons/WBIPublish", false);
            sellIcon = GameDatabase.Instance.GetTexture("WildBlueIndustries/Pathfinder/Icons/WBISell", false);
            keepIconWhite = GameDatabase.Instance.GetTexture("WildBlueIndustries/Pathfinder/Icons/WBIKeepWhite", false);
            publishIconWhite = GameDatabase.Instance.GetTexture("WildBlueIndustries/Pathfinder/Icons/WBIPublishWhite", false);
            sellIconWhite = GameDatabase.Instance.GetTexture("WildBlueIndustries/Pathfinder/Icons/WBISellWhite", false);

            part = host;
            scienceContainer = this.part.FindModuleImplementing<ModuleScienceContainer>();
        }

        public override void SetVisible(bool newValue)
        {
            base.SetVisible(newValue);

            currentIndex = 0;
            scienceContainer = this.part.FindModuleImplementing<ModuleScienceContainer>();
            dataQueue = scienceContainer.GetData();
        }

        protected override void  DrawWindowContents(int windowId)
        {
            if (currentIndex >= dataQueue.Length)
            {
                SetVisible(false);
                return;
            }

            ScienceData data = dataQueue[currentIndex];
            string results = ResearchAndDevelopment.GetResults(data.subjectID);
            int experimentIndex = currentIndex + 1;

            GUILayout.BeginVertical();

            //Title
            GUILayout.BeginScrollView(new Vector2(0, 0));
            GUILayout.Label("<color=white>[" + experimentIndex.ToString() + "/" + dataQueue.Length.ToString() + "] " + data.title + "</color>");
            GUILayout.EndScrollView();

            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();

            //Results
            _scrollPos = GUILayout.BeginScrollView(_scrollPos);
            GUILayout.Label(results, GUILayout.Height(140));
            GUILayout.EndScrollView();

            //Data Size
            GUILayout.BeginScrollView(new Vector2(0, 0));
            GUILayout.Label("<color=white>Data Size: " + data.dataAmount + " Mits</color>");
            GUILayout.EndScrollView();

            //Publish amount
            GUILayout.BeginScrollView(new Vector2(0, 0));
            GUILayout.Label(new GUIContent("<color=yellow>  Publish: +" + data.dataAmount + " Reputation</color>", publishIconWhite), new GUILayoutOption[] { GUILayout.Height(24) });
            GUILayout.EndScrollView();

            //Sell amount
            GUILayout.BeginScrollView(new Vector2(0, 0));
            GUILayout.Label(new GUIContent("  Sell: +" + data.dataAmount + " Funds", sellIconWhite), new GUILayoutOption[] { GUILayout.Height(24) });
            GUILayout.EndScrollView();

            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            //Keep
            if (GUILayout.Button(keepIcon, new GUILayoutOption[] { GUILayout.Width(64), GUILayout.Height(64)}))
            {
            }
            GUILayout.FlexibleSpace();

            //Publish
            if (GUILayout.Button(publishIcon, new GUILayoutOption[] { GUILayout.Width(64), GUILayout.Height(64) }))
            {
            }
            GUILayout.FlexibleSpace();

            //Sell
            if (GUILayout.Button(sellIcon, new GUILayoutOption[] { GUILayout.Width(64), GUILayout.Height(64) }))
            {
            }
            GUILayout.FlexibleSpace();

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }
    }
}
