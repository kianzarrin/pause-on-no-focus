using ICities;
using UnityEngine;
using KianCommons;
using KianCommons.IImplict;

namespace PauseOnNoFocus
{
    public class Mod : IUserMod
    {

        public string Description
        {
            get { return "Automatically pauses the game when it is not focused"; }
        }

        public string Name
        {
            get { return "Pause on no focus"; }
        }
    }

    public class PauseOnUnfocus : LoadingExtensionBase
    {
        public override void OnLevelLoaded(LoadMode mode)
        {
            Application.runInBackground = false;
        }

        public override void OnLevelUnloading()
        {
            Application.runInBackground = true;
        }
    }
}
