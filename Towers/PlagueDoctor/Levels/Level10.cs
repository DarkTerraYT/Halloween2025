using System.Collections;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Halloween2025.Assets.Towers.PlagueDoctor;
using HarmonyLib;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppNinjaKiwi.Common.ResourceUtils;

namespace Halloween2025.Towers.PlagueDoctor.Levels;

public class Level10 : HeroLevel
{
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var ability = new AbilityModel("plague_bringer_1", "Plague Bringer", "Spawns a plague bloon, which all spread a mega plague to other bloons.",
            1, 0, GetSpriteReferenceOrDefault("AmogAA2"), 40, null, false, false, Id,
            0, 0, 999999999, -1, false, false);

        var atk = Game.instance.model.GetTowerFromId("SpikeFactory").GetAttackModel().Duplicate();
        atk.range = 99999;
        var weapon = atk.weapons[0]!;
        weapon.rate = 1f;
        var proj = weapon.projectile;
        proj.pierce = 9999999999999;
        proj.ApplyDisplay<PlagueBloon>();
        proj.RemoveBehavior<SetSpriteFromPierceModel>();
        proj.RemoveBehavior<AgeModel>();
        proj.AddBehavior(new TravelAlongPathModel("TravelAlongPathModel", 20, 25, true, true, 0));
        var addBehaviors = towerModel.GetWeapon().projectile.GetBehavior<AddBehaviorToBloonModel>().Duplicate();
        addBehaviors.mutationId = "Amog_MegaPlague";
        addBehaviors.RemoveBehavior<CarryProjectileModel>();
        addBehaviors.ApplyOverlay<MegaPoison>();
        var dot = addBehaviors.GetBehavior<DamageOverTimeModel>();
        dot.damage = 100;
        dot.interval = 0.25f;
        addBehaviors.lifespan = 5;
        
        proj.AddBehavior(addBehaviors);
        proj.UpdateCollisionPassList();
        proj.radius = 10;
        
        var activateAttack = new ActivateAttackModel("ActivateAttackModel_", 0.95f, true,
            new Il2CppReferenceArray<AttackModel>([atk]), false, true, false, false, false, true);
        ability.AddBehavior(activateAttack);
        
        towerModel.AddBehavior(ability);
    }

    public override string Description => "Ability, Plague Bringer: Spawns a plague bloon, which all spread a mega plague to other bloons. This mega plague cannot spread to other bloons.";

    public override string AbilityDescription =>
        "Spawns a plague bloon, which all spread a mega plague to other bloons. This mega plague cannot spread to other bloons.";

    public override string AbilityName => "Plague Bringer";
}