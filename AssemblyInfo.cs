using System;
using System.Resources;
using System.Reflection;
using System.Runtime.InteropServices;
using MelonLoader;

[assembly: AssemblyTitle(KeyboardPaste.BuildInfo.Name)]
[assembly: AssemblyDescription(KeyboardPaste.BuildInfo.Description)]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(KeyboardPaste.BuildInfo.Company)]
[assembly: AssemblyProduct(KeyboardPaste.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + KeyboardPaste.BuildInfo.Author)]
[assembly: AssemblyTrademark(KeyboardPaste.BuildInfo.Company)]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
//[assembly: Guid("")]
[assembly: AssemblyVersion(KeyboardPaste.BuildInfo.Version)]
[assembly: AssemblyFileVersion(KeyboardPaste.BuildInfo.Version)]
[assembly: NeutralResourcesLanguage("en")]
[assembly: MelonInfo(typeof(KeyboardPaste.Main),
    KeyboardPaste.BuildInfo.Name,
    KeyboardPaste.BuildInfo.Version,
    KeyboardPaste.BuildInfo.Author,
    KeyboardPaste.BuildInfo.DownloadLink)]
[assembly: MelonColor(ConsoleColor.Yellow)]

//[assembly: MelonOptionalDependencies("", "", "", "")]
// Create and Setup a MelonModGame to mark a Mod as Universal or Compatible with specific Games.
// If no MelonModGameAttribute is found or any of the Values for any MelonModGame on the Mod is null or empty it will be assumed the Mod is Universal.
// Values for MelonModGame can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame("VRChat", "VRChat")]