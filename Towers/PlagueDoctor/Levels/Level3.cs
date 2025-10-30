using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Halloween2025.Assets.Towers.PlagueDoctor;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;
using Il2CppInterop.Runtime.InteropTypes.Arrays;

namespace Halloween2025.Towers.PlagueDoctor.Levels;

public class Level3 : HeroLevel
{
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var abilityModel = new AbilityModel("fray", "Fray", "Amog shoots 16 needles all around him", 1, 0,
            GetSpriteReferenceOrDefault("AmogAA1"), 20, new Il2CppReferenceArray<Model>(0), false, false, Id, 0, 0,
            99999999, 999999999, false, false);
        var atk = Game.instance.model.GetTowerFromId("TackShooter").GetAttackModel().Duplicate();
        var weapon = atk.weapons[0];
        weapon.SetProjectile(towerModel.GetWeapon().projectile);
        weapon.emission.Cast<ArcEmissionModel>().count = 16;
        weapon.fireWithoutTarget = true;
        atk.fireWithoutTarget = true;
        atk.range = towerModel.range;
        var activateAttack = new ActivateAttackModel("ActivateAttackModel_Fray", 1, true, new Il2CppReferenceArray<AttackModel>([atk]), false, false, false, false, true, false);
        abilityModel.AddBehavior(activateAttack);
        towerModel.AddBehavior(abilityModel);
    }

    public override string AbilityDescription => "Amog shoots 16 needles all around him";
    public override string AbilityName => "Fray";

    public override string Description => "Ability, Fray: Amog shoots out 16 needles all around him.";
}