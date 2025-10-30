global using static Halloween2025.Halloween2025;
using System.Collections.Generic;
using System.Linq;
using MelonLoader;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Halloween2025;
using Halloween2025.Towers.PlagueDoctor;
using HarmonyLib;
using Il2Cpp;
using Il2CppAssets.Scripts;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Simulation.Towers.Projectiles;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppNinjaKiwi.Common.ResourceUtils;
using Il2CppSystem.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[assembly: MelonInfo(typeof(Halloween2025.Halloween2025), ModHelperData.Name, ModHelperData.Version, ModHelperData.Author)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace Halloween2025;

public class Halloween2025 : BloonsTD6Mod
{
    internal static MelonLogger.Instance Logger;

    public override void OnInitialize()
    {
        Logger = LoggerInstance;
    }

    private static readonly int HighlightedId = Shader.PropertyToID("_Highlighted");
    public override void OnTowerDeselected(Tower tower)
    {
        if (tower.towerModel.name.StartsWith(IDPrefix))
        {
            if (tower.GetUnityDisplayNode() == null)
            {
                return;
            }

            foreach (var renderer in tower.GetUnityDisplayNode().GetMeshRenderers())
            {
                foreach (var mat in renderer.materials)
                {
                    if (mat.HasInt(HighlightedId))
                    {
                        mat.SetInt(HighlightedId, 0);
                    }
                }
            }
        }
    }

    [HarmonyPatch(typeof(Tower), nameof(Tower.Hilight))]
    private static class Tower_Highlight
    {
        public static void Postfix(Tower __instance)
        { 
            if (__instance.towerModel.name.StartsWith(ModHelper.GetMod<Halloween2025>().IDPrefix))
            {
                if (__instance.GetUnityDisplayNode() == null)
                {
                    return;
                }

                foreach (var renderer in __instance.GetUnityDisplayNode().GetMeshRenderers())
                {
                    foreach (var mat in renderer.materials)
                    {
                        if (mat.HasInt(HighlightedId))
                        {
                            mat.SetInt(HighlightedId, 1);
                        }
                    }
                }
            }
        }
    }

    public override void OnApplicationStart()
    {
    }
}