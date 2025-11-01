global using static Halloween2025.Halloween2025;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Extensions;
using Halloween2025;
using HarmonyLib;
using Il2CppAssets.Scripts.Simulation.Towers;
using MelonLoader;
using UnityEngine;

[assembly:
    MelonInfo(typeof(Halloween2025.Halloween2025), ModHelperData.Name, ModHelperData.Version, ModHelperData.Author)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace Halloween2025;

public class Halloween2025 : BloonsTD6Mod
{
    internal static MelonLogger.Instance Logger;

    private static readonly int HighlightedId = Shader.PropertyToID("_Highlighted");

    public override void OnInitialize()
    {
        Logger = LoggerInstance;
    }

    public override void OnTowerDeselected(Tower tower)
    {
        if (tower.towerModel.name.StartsWith(IDPrefix))
        {
            if (tower.GetUnityDisplayNode() == null) return;

            foreach (var renderer in tower.GetUnityDisplayNode().GetMeshRenderers())
            foreach (var mat in renderer.materials)
                if (mat.HasInt(HighlightedId))
                    mat.SetInt(HighlightedId, 0);
        }
    }

    public override void OnApplicationStart()
    {
    }

    [HarmonyPatch(typeof(Tower), nameof(Tower.Hilight))]
    private static class Tower_Highlight
    {
        public static void Postfix(Tower __instance)
        {
            if (__instance.towerModel.name.StartsWith(ModHelper.GetMod<Halloween2025>().IDPrefix))
            {
                if (__instance.GetUnityDisplayNode() == null) return;

                foreach (var renderer in __instance.GetUnityDisplayNode().GetMeshRenderers())
                foreach (var mat in renderer.materials)
                    if (mat.HasInt(HighlightedId))
                        mat.SetInt(HighlightedId, 1);
            }
        }
    }
}