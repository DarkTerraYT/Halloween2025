using System.Collections.Generic;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Halloween2025.Assets;
using Il2Cpp;
using Il2CppAssets.Scripts;
using Il2CppAssets.Scripts.Models.Bloons;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppNinjaKiwi.Common.ResourceUtils;
using UnityEngine;

namespace Halloween2025.Towers.GhostMonkey.MiddlePath;

public class LifeDrain : ModUpgrade<GhostMonkey>
{
    public override string Description =>
        "Jiangshi drains 50% extra health from the bloons. Affected bloons drain life quickly after getting hit.";

    public override int Path => Top;
    public override int Tier => 4;
    public override int Cost => 3500;

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var weapon = towerModel.GetWeapons().Find(w => w.name == "Jiangshi")!;
        var projectile = weapon.projectile;

        projectile.GetDamageModel().damage *= 1.5f;

        var addBehaviors = new AddBehaviorToBloonModel("AddBehaviorToBloonModel_", "lifedrain", 10, 99999, null, null,
            new Il2CppReferenceArray<BloonBehaviorModel>(0), "", false, true, false, false, 0, true, 1,
            projectile.GetDamageModel(), false, 100, true);
        var dot = new DamageOverTimeModel("DamageOverTimeModel_", 1f, 0.1f, BloonProperties.None, BloonProperties.None,
            new PrefabReference(""), 10, false, ObjectId.FromData(0), false, 0, false, false, true,
            new Il2CppReferenceArray<DamageModifierModel>(0), false);
        addBehaviors.AddBehavior(dot);
        addBehaviors.ApplyOverlay<LifeDrainEffect>();
        projectile.AddBehavior(addBehaviors);
        projectile.UpdateCollisionPassList();
    }

    public class LifeDrainEffect : ModBloonOverlay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override IEnumerable<BloonOverlayClass> BloonOverlayClasses =>
        [
            BloonOverlayClass.Red, BloonOverlayClass.Blue, BloonOverlayClass.Green, BloonOverlayClass.Yellow,
            BloonOverlayClass.Pink, BloonOverlayClass.White, BloonOverlayClass.Moab, BloonOverlayClass.Bfb,
            BloonOverlayClass.Zomg, BloonOverlayClass.Bad
        ];

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            node.transform.gameObject.DestroyAllChildren();
            var particles = Object.Instantiate(AssetHelper.GetObject(Name), node.transform);
            node.particles = particles.GetComponentInChildren<ParticleSystem>();
            node.particles.transform.localPosition = Vector3.zero;
            particles.transform.localPosition = Vector3.zero;
        }
    }
}