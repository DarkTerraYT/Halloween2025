using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Halloween2025.Assets;
using Il2Cpp;
using Il2CppAssets.Scripts.Models.Effects;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppNinjaKiwi.Common;

namespace Halloween2025.Towers.GhostMonkey.TopPath;

public class SpookyBlasts : ModUpgrade<GhostMonkey>
{
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var weapon = towerModel.GetWeapon();
        var projectile = weapon.projectile;
        projectile.pierce += 2;
        weapon.rate *= 0.9f;
        
        SlowModel slow = new("SlowModel_Spook", 0.85f, 2, "Spook", -1, GetId("SpookOverlay"), true, false, null, false, false, false, 1);
        projectile.AddBehavior(slow);
    }

    public class SpookOverlay : ModBloonOverlay
    {
        public override string BaseOverlay =>
            Game.instance.model.GetTower("GlueGunner").GetDescendant<SlowModel>().overlayType;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            node.transform.DestroyAllChildren();
            var sweat = AssetHelper.GetObject("Sweat");
            UnityEngine.Object.Instantiate(sweat.transform.GetChild(0).gameObject).transform.SetParent(node.transform);
        }
    }

    public override string Description => "Soul bolts now spook bloons slowing them down for a short amount of time. All Soul bolts from any ghost monkey do more damage to spooked bloons.";

    public override int Path => Top;
    public override int Tier => 1;
    public override int Cost => 600;
}