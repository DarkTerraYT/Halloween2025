using System.Collections.Generic;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Halloween2025.Assets;
using Il2Cpp;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppNinjaKiwi.Common;
using UnityEngine;

namespace Halloween2025.Towers.GhostMonkey.TopPath;

public class SpookyBlasts : ModUpgrade<GhostMonkey>
{
    public override string Description =>
        "Soul bolts now spook bloons slowing them down for a short amount of time. All Soul bolts from any ghost monkey do more damage to spooked bloons. \"Boo!\"";

    public override int Path => Middle;
    public override int Tier => 1;
    public override int Cost => 600;

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var weapon = towerModel.GetWeapon();
        var projectile = weapon.projectile;
        weapon.rate *= 0.9f;

        SlowModel slow = new("SlowModel_Spook", 0.75f, 2, "Spooked", -1, GetId("SpookOverlay"), true, false, null,
            false, false, false, 1);
        projectile.AddBehavior(slow);
        projectile.UpdateCollisionPassList();
    }

    public class SpookOverlay : ModBloonOverlay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override IEnumerable<BloonOverlayClass> BloonOverlayClasses =>
        [
            BloonOverlayClass.Red,
            BloonOverlayClass.Blue,
            BloonOverlayClass.Green,
            BloonOverlayClass.Yellow,
            BloonOverlayClass.Pink,
            BloonOverlayClass.White,
            BloonOverlayClass.RedRegrow,
            BloonOverlayClass.BlueRegrow,
            BloonOverlayClass.GreenRegrow,
            BloonOverlayClass.YellowRegrow,
            BloonOverlayClass.PinkRegrow,
            BloonOverlayClass.WhiteRegrow
        ];

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            node.transform.DestroyAllChildren();
            var sweat = AssetHelper.GetObject("Sweat");
            sweat = Object.Instantiate(sweat.transform.gameObject);
            sweat.transform.SetParent(node.transform);
            sweat.transform.localPosition = new Vector3(0, 1, 0);
            sweat.transform.localScale = new Vector3(2, 2, 2);
        }
    }
}