using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Halloween2025.Assets;
using Halloween2025.Assets.Towers;
using Il2Cpp;
using Il2CppAssets.Scripts.Models.Audio;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Effects;
using Il2CppAssets.Scripts.Models.Map;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Weapons.Behaviors;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppNinjaKiwi.Common;
using Il2CppNinjaKiwi.Common.ResourceUtils;

namespace Halloween2025.Towers.GhostMonkey.TopPath;

public class Banshee : ModUpgrade<GhostMonkey>
{
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var weapon = towerModel.GetWeapon();
        if (towerModel.tiers[Top] >= 3 || towerModel.tiers[Bottom] >= 3)
        {
            weapon = weapon.Duplicate();
        }
        
        weapon.name = "Banshee";

        weapon.ejectZ = 7;
        weapon.ejectX = 0;
        weapon.ejectY = 3;
        var projectile = weapon.projectile;

        var sound1 = new SoundModel("BansheeAttack1", GetAudioClipReference("BansheeAttack1"));
        var sound2 = new SoundModel("BansheeAttack2", GetAudioClipReference("BansheeAttack2"));
        var sound3 = new SoundModel("BansheeAttack3", GetAudioClipReference("BansheeAttack3"));
        var createSoundOnEmitModel = new CreateSoundOnProjectileCreatedModel("CreateSoundOnProjectileCreatedModel_",
            sound1, sound2, sound3, sound1, sound3, "BansheeScream");
        weapon.AddBehavior(createSoundOnEmitModel);
        
        projectile.ApplyDisplay<SonicWaves>();
        var travelModel = projectile.GetBehavior<TravelStraitModel>();
        travelModel.lifespan = 1.5f;
        travelModel.speed = 50;

        var slowModel =projectile.GetBehavior<SlowModel>();
        slowModel.lifespan = 0.75f;
        slowModel.multiplier = 1;
        slowModel.mutationId = "Spooked2";

        var DOT = Game.instance.model.GetTower("MortarMonkey", pathThreeTier: 3).GetDescendant<AddBehaviorToBloonModel>().Duplicate();
        DOT.GetBehavior<DamageOverTimeModel>().damage = 2;
        DOT.lifespan = 0.75f;
        DOT.GetBehavior<DamageOverTimeModel>().interval = 0.15f;
        projectile.AddBehavior(DOT);
        projectile.UpdateCollisionPassList();
        
        if (towerModel.tiers[Top] >= 3 || towerModel.tiers[Bottom] >= 3)
        {
            towerModel.GetAttackModel().AddWeapon(weapon);
        }
    }

    public override string Description => "Ghost monkey is now a Banshee, screaming at bloons. This freezes them in place for a very short, and causes ghost monkeys to do even more damage to these bloons.";

    public override int Path => Middle;
    public override int Tier => 3;
    public override int Cost => 820;
}