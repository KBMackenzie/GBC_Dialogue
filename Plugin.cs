using BepInEx;
using GBC;
using HarmonyLib;
using BepInEx.Logging;
using UnityEngine;
using DiskCardGame;
using GBC_Dialogue.Patches;
using GBC_Dialogue.Helpers;
using InscryptionAPI.Helpers;
using System.Collections;
using System.Collections.Generic;

namespace GBC_Dialogue;

// Plugin base:
[BepInPlugin(PluginGuid, PluginName, PluginVersion)]
[BepInDependency("cyantist.inscryption.api", BepInDependency.DependencyFlags.HardDependency)]
public class Plugin : BaseUnityPlugin
{
    public const string PluginGuid = "kel.inscryption.gbcdialogue";
    public const string PluginName = "GBC_Dialogue";
    public const string PluginVersion = "1.0.0";

    internal static ManualLogSource myLogger; // Log source.
    private void Awake()
    {

        myLogger = Logger; // Make log source.

        Harmony harmony = new Harmony("kel.harmony.gbcdialogue");
        harmony.PatchAll();
    }
}