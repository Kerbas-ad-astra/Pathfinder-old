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
    public class WBIGeologyLab : PartModule
    {
        private const string kGeoLabToolTip = "You might want to staff your Geology Labs with scientists. Just be sure you choose the right scientists for the job...";
        private const string kToolTipTitle = "Your First Pathfinder Lab!";
        Animation anim;
        WBIMultiConverter multiConverter;

        public override void OnStart(StartState state)
        {
            base.OnStart(state);

            if (HighLogic.LoadedSceneIsFlight == false)
                return;

            if (WBIPathfinderScenario.Instance.geoLabToolTipShown)
                return;

            multiConverter = this.part.FindModuleImplementing<WBIMultiConverter>();
            if (multiConverter == null)
                return;
            if (string.IsNullOrEmpty(multiConverter.animationName))
                return;

            anim = this.part.FindModelAnimators(multiConverter.animationName)[0];
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (anim != null && multiConverter != null)
            {
                if (anim.isPlaying == false && multiConverter.isDeployed)
                {
                    WBIPathfinderScenario.Instance.geoLabToolTipShown = true;
                    WBIToolTipWindow toolTipWindow = new WBIToolTipWindow(kToolTipTitle, kGeoLabToolTip);
                    toolTipWindow.SetVisible(true);
                    anim = null;
                    multiConverter = null;
                }
            }
        }
    }
}
