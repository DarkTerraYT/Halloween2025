using System.IO;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Extensions;
using Halloween2025.Towers.GhostMonkey;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using System;
using System.Linq;
using MelonLoader.Utils;
using UnityEngine;
using Object = UnityEngine.Object;
using Vector3 = Il2CppAssets.Scripts.Simulation.SMath.Vector3;

namespace Halloween2025.Assets.Towers;

public class GhostMonkey000 : ModTowerCustomDisplay<GhostMonkey>
{
    public override bool UseForTower(params int[] tiers) => tiers.Max() < 3;

    public override string AssetBundleName => "halloween2025";
    public override string PrefabName => Name;
}
public class Jiangshi : ModTowerCustomDisplay<GhostMonkey>
{
    public override bool UseForTower(params int[] tiers) => tiers[0] == 3;

    public override string AssetBundleName => "halloween2025";
    public override string PrefabName => Name;
}
public class Jiangshi2 : ModTowerCustomDisplay<GhostMonkey>
{
    public override bool UseForTower(params int[] tiers) => tiers[0] == 4;

    public override string AssetBundleName => "halloween2025";
    public override string PrefabName => Name;
}
public class Jiangshi3 : ModTowerCustomDisplay<GhostMonkey>
{
    public override bool UseForTower(params int[] tiers) => tiers[0] == 5;

    public override string AssetBundleName => "halloween2025";
    public override string PrefabName => Name;
}

public class Banshee : ModTowerCustomDisplay<GhostMonkey>
{
    public override bool UseForTower(params int[] tiers) => tiers[1] == 3;

    public override string AssetBundleName => "halloween2025";
    public override string PrefabName => Name;
}
public class Banshee2 : ModTowerCustomDisplay<GhostMonkey>
{
    public override bool UseForTower(params int[] tiers) => tiers[1] == 4;

    public override string AssetBundleName => "halloween2025";
    public override string PrefabName => Name;
}
public class Banshee3 : ModTowerCustomDisplay<GhostMonkey>
{
    public override bool UseForTower(params int[] tiers) => tiers[1] == 5;

    public override string AssetBundleName => "halloween2025";
    public override string PrefabName => Name;
}
public class Spectre : ModTowerCustomDisplay<GhostMonkey>
{
    public override bool UseForTower(params int[] tiers) => tiers[2] == 3;

    public override string AssetBundleName => "halloween2025";
    public override string PrefabName => Name;
}
public class Spectre2 : ModTowerCustomDisplay<GhostMonkey>
{
    public override bool UseForTower(params int[] tiers) => tiers[2] == 4;

    public override string AssetBundleName => "halloween2025";
    public override string PrefabName => Name;
}
public class Spectre3 : ModTowerCustomDisplay<GhostMonkey>
{
    public override bool UseForTower(params int[] tiers) => tiers[2] == 5;

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
public class SpectreBolt : ModDisplay
{
    public override string BaseDisplay => Generic2dDisplay;
    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        Set2DTexture(node, Name);
        Object.Instantiate(AssetHelper.GetObject("SpectreEffect"), node.transform);
    }
}
public class BloonSoul : ModDisplay
{
    public override string BaseDisplay => Generic2dDisplay;
    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        Set2DTexture(node, Name);
        Object.Instantiate(AssetHelper.GetObject("SpectreEffect"), node.transform);
    }
}

public class JiangshiAura : ModDisplay2D
{
    protected override string TextureName => "GhostAura";
    public override Vector3 PositionOffset => new Vector3(0, 0, 5);

    public override float Scale => 0.25f;
}

public class Lifesteal : ModCustomDisplay
{
    public override string AssetBundleName => "halloween2025";
    public override string PrefabName => Name;
}

public class SonicWaves : ModCustomDisplay
{
    public override string AssetBundleName => "halloween2025";
    public override string PrefabName => "ScreamEffect";
}