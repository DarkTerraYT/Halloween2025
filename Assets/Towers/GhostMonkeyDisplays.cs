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

public class GhostMonkey000 : ModTowerCustomDisplay<GhostMonkey>
{
    public override bool UseForTower(params int[] tiers) => true;

    public override string AssetBundleName => "halloween2025";
    public override string PrefabName => Name;
}

public class GhostBolt : ModDisplay
{
    public override string BaseDisplay => Generic2dDisplay;
    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        Set2DTexture(node, Name);
    }
}