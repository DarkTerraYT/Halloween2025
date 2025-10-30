using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Halloween2025.Assets.Towers.PlagueDoctor;
using Il2CppAssets.Scripts.Models.Audio;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Filters;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Weapons.Behaviors;
using Il2CppAssets.Scripts.Unity;

namespace Halloween2025.Towers.PlagueDoctor;

public class PlagueDoctor : ModHero
{
    public override void ModifyBaseTowerModel(TowerModel towerModel)
    {
        towerModel.range = 50;
        towerModel.GetAttackModel().range = 50;
        
        var weapon = towerModel.GetWeapon();
        var projectile = weapon.projectile;
        
        weapon.RemoveBehavior<CreateSoundOnProjectileCreatedModel>();
        
        projectile.ApplyDisplay<Needle>();
        projectile.GetDamageModel().damage = 1;

        projectile.pierce = 0;
        projectile.GetBehavior<TravelStraitModel>().lifespan *= 1.5625f;

        var addBehaviors = Game.instance.model.GetTower(TowerType.MortarMonkey, pathThreeTier: 2).GetWeapon().projectile.GetDescendant<AddBehaviorToBloonModel>().Duplicate();
        var dot = addBehaviors.GetBehavior<DamageOverTimeModel>();

        addBehaviors.mutationId = "Amog_Plague";
        addBehaviors.lifespan = 3;
        addBehaviors.lastAppliesFirst = true;
        addBehaviors.applyOnlyIfDamaged = false;
        addBehaviors.isUnique = true;
        addBehaviors.dontCopy = false;
        addBehaviors.parentDamageModel = projectile.GetDamageModel();
        dot.damage = 1;
        dot.interval = 1;
        dot.isFireBased = false;
        
        addBehaviors.ApplyOverlay<Poison>();
        
        projectile.AddBehavior(addBehaviors);
        projectile.UpdateCollisionPassList();
        towerModel.RemoveBehavior<CreateSoundOnTowerPlaceModel>();
        towerModel.RemoveBehavior<CreateSoundOnSelectedModel>();
        towerModel.RemoveBehavior<CreateSoundOnBloonEnterTrackModel>();
        towerModel.RemoveBehavior<CreateSoundOnAbilityModel>();
        var createSoundOnUpgradeModel = towerModel.GetBehavior<CreateSoundOnUpgradeModel>();
        createSoundOnUpgradeModel.sound1 = null;
        createSoundOnUpgradeModel.sound2= null;
        createSoundOnUpgradeModel.sound3 = null;
        createSoundOnUpgradeModel.sound4 = null;
        createSoundOnUpgradeModel.sound5 = null;
        createSoundOnUpgradeModel.sound6 = null;
        createSoundOnUpgradeModel.sound7 = null;
        createSoundOnUpgradeModel.sound8 = null;
        towerModel.RemoveBehavior<CreateSoundOnBloonDestroyedModel>();
        towerModel.RemoveBehavior<CreateSoundOnBloonLeakModel>();
        
        var animations = towerModel.GetBehavior<PlayAnimationIndexModel>();
        animations.placeAnimation = 2;
        animations.upgradeAnimation = 3;
        
        towerModel.GetDescendants<FilterInvisibleModel>().ForEach(filter => filter.isActive = false);
    }

    public override int Cost => 765;
    public override float XpRatio => 1.15f;
    public override string Title => "The Plague Doctor";

    public override string Description => "Amog is a doctor who deals in diseases. Though, instead of curing them, he spreads them! Don't worry, these diseases can only infect bloons as their immune systems are practically non-existent! Can also see camo bloons.";

    public override string Icon => Portrait;

    public override string DisplayName => "Amog";

    public override string Level1Description =>
        "Pricks bloons with infected needles. This infection does 1 damage/s for 3 seconds.";
}