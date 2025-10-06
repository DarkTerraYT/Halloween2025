using System.IO;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Extensions;
using Halloween2025.Towers.GhostMonkey;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using System;
using MelonLoader.Utils;
using UnityEngine;

namespace Halloween2025.Assets.Towers;

public class GhostMonkey000 : /*ModTowerDisplay<GhostMonkey>*/ModTowerCustomDisplay<GhostMonkey>
{
    public override bool UseForTower(params int[] tiers) => true;

    public override string AssetBundleName => "halloween2025";
    public override string PrefabName => Name;
    //public override string BaseDisplay => GetDisplay("DartMonkey");

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        foreach (var renderer in node.GetRenderers())
        {
            var shader = renderer.material.shader;
            for (int i = 0; i < shader.GetPropertyCount(); i++)
            {
                string name = shader.GetPropertyName(i);
                mod.LoggerInstance.Msg($"{name} is {shader.GetPropertyType(i)}");
            }
        }
    }
}

public class GhostBolt : ModDisplay
{
    public override string BaseDisplay => Generic2dDisplay;
    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        Set2DTexture(node, Name);
    }
}