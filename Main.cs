using MelonLoader;
using UnityEngine;
using System;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace KeyboardPaste {
    public static class BuildInfo {
        public const string Name = "KeyboardUtils";
        public const string Author = "Lily";
        public const string Company = "Minty Labs";
        public const string Version = "1.1.0";
        public const string DownloadLink = "https://github.com/MintLily/KeyboardPaste";
        public const string Description = "Simple utility that adds a paste and copy button on VRChat's in-game keyboard.";
    }

    public class Main : MelonMod {
        private bool _isDebug;
        public MelonPreferences_Category Melon;
        public MelonPreferences_Entry<bool> PasteVisible, CopyVisible;
        private GameObject _keybardPasteButton, _keybardCopyButton;
        private int _scenesLoaded = 0;
        private static readonly MelonLogger.Instance Logger = new MelonLogger.Instance(BuildInfo.Name, ConsoleColor.Yellow);

        public override void OnApplicationStart() // Runs after Game Initialization.
        {
            if (MelonDebug.IsEnabled()) {
                _isDebug = true;
                Log("Debug mode is active", true);
            }

            Melon = MelonPreferences.CreateCategory(BuildInfo.Name, BuildInfo.Name);
            PasteVisible = Melon.CreateEntry("buttonVisible", true, "Is Paste Button Visible");
            CopyVisible = Melon.CreateEntry("copyButtonVisible", true, "Is Copy Button Visible");

            MelonLogger.Msg("Initialized!");
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName) {
            if (_scenesLoaded > 2) return;
            _scenesLoaded++;
            if (_scenesLoaded != 2) return;
            CreatePasteButton();
            CreateCopyButton();
        }

        public override void OnPreferencesSaved() {
            // Paste Button
            if (_keybardPasteButton == null && PasteVisible.Value)
                CreatePasteButton();
            else if (_keybardPasteButton != null && !PasteVisible.Value) {
                Object.Destroy(_keybardPasteButton);
                Log("Destroyed the paste button.", _isDebug);
            }
            
            // Copy Button
            if (_keybardCopyButton == null && CopyVisible.Value)
                CreateCopyButton();
            else if (_keybardCopyButton != null && !CopyVisible.Value) {
                Object.Destroy(_keybardCopyButton);
                Log("Destroyed the copy button.", _isDebug);
            }
        }

        private void CreatePasteButton() {
            try {
                // Hey skids, if you're gonna take this and add to your mod, at least give me some credit. Much appreciated!
                _keybardPasteButton = Object.Instantiate(GameObject.Find("/UserInterface/MenuContent/Popups/InputPopup/ButtonLeft"),
                    VRCUiPopupManager.field_Public_VRCUiPopupInput_0.transform);
                _keybardPasteButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(335f, -275f);
                _keybardPasteButton.GetComponentInChildren<Text>().text = "Paste";
                _keybardPasteButton.name = "KeyboardPasteButton";
                _keybardPasteButton.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
                _keybardPasteButton.GetComponent<Button>().m_Interactable = true;
                _keybardPasteButton.GetComponent<Button>().onClick.AddListener(new Action(() => {
                    try {
                        if (GUIUtility.systemCopyBuffer.Length < 256)
                            GameObject.Find("UserInterface/MenuContent/Popups/InputPopup/InputField").GetComponent<InputField>().text = GUIUtility.systemCopyBuffer;
                        else
                            Logger.Warning("You cannot paste something more than 256 characters long in the keyboard.");
                    }
                    catch (Exception e) {
                        Logger.Error($"An error has occurred:\n{e}");
                    }
                }));
                Log("Created the paste button.", _isDebug);
            }
            catch (Exception e) {
                Logger.Error($"Paste:\n{e}");
            }
        }

        private void CreateCopyButton() {
            try {
                _keybardCopyButton = Object.Instantiate(GameObject.Find("/UserInterface/MenuContent/Popups/InputPopup/ButtonLeft"),
                    VRCUiPopupManager.field_Public_VRCUiPopupInput_0.transform);
                _keybardCopyButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(-335f, -275f);
                _keybardCopyButton.GetComponentInChildren<Text>().text = "Copy";
                _keybardCopyButton.name = "KeyboardCopyButton";
                _keybardCopyButton.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
                _keybardCopyButton.GetComponent<Button>().m_Interactable = true;
                _keybardCopyButton.GetComponent<Button>().onClick.AddListener(new Action(() => {
                    try {
                        GUIUtility.systemCopyBuffer = GameObject.Find("UserInterface/MenuContent/Popups/InputPopup/InputField").GetComponent<InputField>().text;
                    }
                    catch (Exception e) {
                        Logger.Error($"An error has occurred:\n{e}");
                    }
                }));
                Log("Created the copy button.", _isDebug);
            }
            catch (Exception e) {
                Logger.Error($"Copy:\n{e}");
            }
        }

        private VRCUiPopupManager VRCUiPopupManager => VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0;

        private void Log(string message, bool isDebug = false) {
            if (isDebug) {
                Logger.Msg(ConsoleColor.Green, message);
                return;
            }
            Logger.Msg(message);
        }
    }
}