using ICities;
using UnityEngine;
using System.Collections;
using ColossalFramework.UI;
using System;

namespace PauseOnNoFocus
{
    public static class Extensions {
        public static UICheckBox AddUpdatingCheckbox(
        this UIHelperBase helper, string label, Action<bool> SetValue, Func<bool> GetValue)
        {
            Debug.Log($"option '{label}' is " + GetValue());
            var cb = helper.AddCheckbox(label, GetValue(), delegate (bool value) {
                try
                {
                    SetValue(value);
                    Debug.Log($"option '{label}' is set to " + value);
                }
                catch (Exception ex) { Debug.LogException(ex); }
            }) as UICheckBox;
            cb.eventVisibilityChanged += (c, val) => (c as UICheckBox).isChecked = GetValue();
            return cb;

        }
    }

    public class Mod : IUserMod
    {
        public string Description => "Automatically pauses the game when it is not focused";
        public string Name => "Pause on no focus";

        public void OnSettingsUI(UIHelper helper)
        {
            helper.AddUpdatingCheckbox(
                "Run in background",
                val => Application.runInBackground = val,
                () => Application.runInBackground);
        }


    }

    public class PauseOnUnfocus : LoadingExtensionBase
    {
        public override void OnLevelLoaded(LoadMode mode)
        {
            LoadingManager.instance.QueueLoadingAction(LoadAction());
        }

        public static IEnumerator LoadAction()
        {
            yield return 0;
            Application.runInBackground = false;
            yield break;
        }

        public override void OnLevelUnloading()
        {
            Application.runInBackground = true;
        }
    }
}
