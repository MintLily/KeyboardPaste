using MelonLoader;
using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using System.Windows.Forms;

namespace KeyboardPaste
{
    public static class BuildInfo
    {
        public const string Name = "KeyboardPaste"; // Name of the Mod.  (MUST BE SET)
        public const string Author = "Korty (Lily)"; // Author of the Mod.  (Set as null if none)
        public const string Company = null; // Company that made the Mod.  (Set as null if none)
        public const string Version = "1.0.0"; // Version of the Mod.  (MUST BE SET)
        public const string DownloadLink = "https://github.com/KortyBoi/KeyboardPaste"; // Download Link for the Mod.  (Set as null if none)
        public const string Description = "Simple utility that adds a paste button on VRChat's in-game keyboard.";
    }

    public class Main : MelonMod
    {
        public bool isDebug;
        public MelonPreferences_Category melon;
        public MelonPreferences_Entry<bool> visible;
        private GameObject keybardPasteButton;

        public override void OnApplicationStart() // Runs after Game Initialization.
        {
            if (MelonDebug.IsEnabled())
            {
                isDebug = true;
                MelonLogger.Msg("Debug mode is active");
            }
            
            melon = MelonPreferences.CreateCategory(BuildInfo.Name, BuildInfo.Name);
            visible = (MelonPreferences_Entry<bool>)melon.CreateEntry("buttonVisible", true, "Is Paste Button Visible");

            MelonLogger.Msg("Initialized!");
        }

        public override void VRChat_OnUiManagerInit()
        {
            MelonCoroutines.Start(CreateButton());
        }

        public override void OnPreferencesSaved()
        {
            if (keybardPasteButton == null && visible.Value == true)
                MelonCoroutines.Start(CreateButton(true));
            else if (keybardPasteButton != null && visible.Value == false)
                GameObject.Destroy(keybardPasteButton);
        }

        private IEnumerator CreateButton(bool ignoreWait = false)
        {
            if (!ignoreWait) yield return new WaitForSeconds(8f);
            try
            {
                // Hey skids, if you're gonna take this and add to your mod, at least give me some credit. Much appreciated!
                keybardPasteButton = UnityEngine.Object.Instantiate<GameObject>(GameObject.Find("/UserInterface/MenuContent/Popups/InputPopup/ButtonLeft"), VRCUiPopupManager.field_Public_VRCUiPopupInput_0.transform);
                keybardPasteButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(335f, -275f);
                keybardPasteButton.GetComponentInChildren<Text>().text = "Paste";
                keybardPasteButton.name = "KeyboardPasteButton";
                keybardPasteButton.GetComponent<UnityEngine.UI.Button>().onClick = new UnityEngine.UI.Button.ButtonClickedEvent();
                keybardPasteButton.GetComponent<UnityEngine.UI.Button>().m_Interactable = true;
                keybardPasteButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(new System.Action(() =>
                {
                    try
                    {
                        if (Clipboard.GetText().Length <= 64)
                        {
                            GameObject.Find("UserInterface/MenuContent/Popups/InputPopup/InputField").GetComponent<InputField>().text = Clipboard.GetText();
                        }
                        else MelonLogger.Warning("You cannot paste something more than 64 characters long in the keyboard.");
                    }
                    catch (Exception e) { MelonLogger.Error("An error has occurred:\n" + e.ToString()); }
                }));
            }
            catch (Exception e)
            {
                MelonLogger.Error("Keyboard Paste Button failure:\n" + e.ToString());
            }
            yield break;
        }

        public VRCUiPopupManager VRCUiPopupManager
        {
            get { return VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0; }
        }
    }
}