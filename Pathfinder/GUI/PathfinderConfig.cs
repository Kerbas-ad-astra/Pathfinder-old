using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using KSP.IO;
using KSP.UI.Screens;

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
    [KSPAddon(KSPAddon.Startup.SpaceCentre, false)]
    class PathfinderConfigMenu : MonoBehaviour
    {
        static protected ApplicationLauncherButton appLauncherButton = null;
        static protected bool buttonAdded;
        static protected Texture2D appIcon = null;
        protected PathfinderSettings pathfinderSettings = new PathfinderSettings();

        public void OnGUI()
        {
            if (pathfinderSettings.IsVisible())
                pathfinderSettings.DrawWindow();
        }

        public void Update()
        {
            addButton();
        }

        protected virtual void addButton()
        {
            if (ApplicationLauncher.Ready && !buttonAdded)
            {
                appLauncherButton = InitializeApplicationButton();

                if (appLauncherButton != null)
                    appLauncherButton.VisibleInScenes = ApplicationLauncher.AppScenes.SPACECENTER;

                buttonAdded = true;
            }
        }

        public void OnDestroy()
        {
            if (appLauncherButton != null)
            {
                ApplicationLauncher.Instance.RemoveModApplication(appLauncherButton);
                appLauncherButton = null;
                buttonAdded = false;
            }
        }

        protected ApplicationLauncherButton InitializeApplicationButton()
        {
            ApplicationLauncherButton appButton = null;
            appIcon = GameDatabase.Instance.GetTexture("WildBlueIndustries/Pathfinder/Icons/PathfinderApp", false);

            if (appIcon != null)
            {
                appButton = ApplicationLauncher.Instance.AddModApplication(
                    OnAppLauncherTrue,
                    OnAppLauncherFalse,
                    null,
                    null,
                    null,
                    null,
                    ApplicationLauncher.AppScenes.SPACECENTER,
                    appIcon);

                buttonAdded = true;
            }

            return appButton;
        }

        void OnAppLauncherTrue()
        {
            pathfinderSettings.SetVisible(true);
        }

        void OnAppLauncherFalse()
        {
            pathfinderSettings.SetVisible(false);
        }
    }

    [KSPAddon(KSPAddon.Startup.Flight, false)]
    class PathfinderConfigMenuFlight : PathfinderConfigMenu
    {
        protected override void addButton()
        {
            base.addButton();
            if (buttonAdded)
                appLauncherButton.VisibleInScenes = ApplicationLauncher.AppScenes.FLIGHT;
        }
    }
}
