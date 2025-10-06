using MelonLoader;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Halloween2025;
using HarmonyLib;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Simulation.Towers;

[assembly: MelonInfo(typeof(Halloween2025.Halloween2025), ModHelperData.Name, ModHelperData.Version, ModHelperData.Author)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace Halloween2025;

public class Halloween2025 : BloonsTD6Mod
{
    public override void OnTowerDeselected(Tower tower)
    {
        if (tower.towerModel.name.StartsWith(IDPrefix))
        {
            if (tower.GetUnityDisplayNode() == null)
            {
                return;
            }
            tower.GetUnityDisplayNode().GetMeshRenderer().materials[1].SetInt("_Highlighted", 0);
        }
    }

    [HarmonyPatch(typeof(Tower), nameof(Tower.Hilight))]
    private static class Tower_Highlight
    {
        public static void Postfix(Tower __instance)
        {
            if (__instance.towerModel.name.StartsWith(ModHelper.GetMod<Halloween2025>().IDPrefix))
            {
                __instance.GetUnityDisplayNode().GetMeshRenderer().materials[1].SetInt("_Highlighted", 1);
            }
        }
    }
}