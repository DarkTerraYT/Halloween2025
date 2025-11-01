using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Halloween2025.Assets.Towers;
using HarmonyLib;
using Il2CppAssets.Scripts.Models.Audio;
using Il2CppAssets.Scripts.Models.Effects;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Weapons.Behaviors;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Simulation.Towers.Projectiles;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppNinjaKiwi.Common.ResourceUtils;
using UnityEngine;

namespace Halloween2025.Towers.GhostMonkey.MiddlePath;

public class Jiangshi : ModUpgrade<GhostMonkey>
{
    public override string Description =>
        "Ghost monkey becomes a Jiangshi, damaging all bloons around it, gaining a life every 100 bloons popped by Jiangsu. \"Though, I suppose a Jiangshi is more so a ghoul...\"";

    public override int Path => Top;
    public override int Tier => 3;
    public override int Cost => 1500;

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var orbit = Game.instance.model.GetTower(TowerType.BoomerangMonkey, 5).GetBehavior<OrbitModel>().Duplicate();
        orbit.range = 0;
        orbit.count = 1;
        var orbitProj = orbit.projectile;
        orbitProj.scale = towerModel.range / 12;
        orbitProj.ApplyDisplay<JiangshiAura>();
        orbitProj.pierce = -1;
        orbitProj.RemoveBehavior<DamageModel>();
        orbitProj.RemoveBehavior<RotateModel>();
        towerModel.AddBehavior(orbit);

        var weapon = towerModel.GetWeapon();
        if (towerModel.tiers[Middle] < 3 && towerModel.tiers[Bottom] < 3)
            towerModel.GetAttackModel().RemoveWeapon(weapon);

        weapon = Game.instance.model.GetTower(TowerType.MonkeySub, 4).GetBehavior<SubmergeModel>().submergeAttackModel
            .Cast<AttackModel>().weapons[1].Duplicate()!;

        weapon.name = "Jiangshi";
        weapon.fireWithoutTarget = true;

        weapon.rate = 1.5f;

        weapon.projectile.RemoveBehavior<AddTagToBloonModel>();
        var addTag = new AddTagToBloonModel("AddTagToBloonModel_", "Jiangshi", 0.5f, "Jiangshi", 99999, true, "");
        addTag.ApplyOverlay<LifeStealOverlay>();
        weapon.projectile.AddBehavior(addTag);
        weapon.projectile.UpdateCollisionPassList();

        //weapon.projectile.ApplyDisplay<Lifesteal>();
        weapon.projectile.display = new PrefabReference("");
        weapon.projectile.pierce = 99999999999;
        weapon.projectile.GetDamageModel().damage = 8;
        weapon.projectile.id = "h25lifesteal_1";
        weapon.SetEject(new Vector3(0, 0, 0));
        var ageModel = weapon.projectile.GetBehavior<AgeModel>();

        weapon.AddBehavior(new EjectEffectModel("EjectionEffect_",
            new EffectModel("Lifesteal", new PrefabReference(GetDisplayGUID<Lifesteal>()), 1, ageModel.lifespan),
            ageModel.lifespan, Fullscreen.No, false, false, false, false));

        var sound1 = new SoundModel("JiangshiAttack1", GetAudioClipReference("JiangshiAttack1"));
        var sound2 = new SoundModel("JiangshiAttack2", GetAudioClipReference("JiangshiAttack2"));
        var sound3 = new SoundModel("JiangshiAttack3", GetAudioClipReference("JiangshiAttack3"));
        var sound4 = new SoundModel("JiangshiAttack4", GetAudioClipReference("JiangshiAttack4"));
        var sound5 = new SoundModel("JiangshiAttack5", GetAudioClipReference("JiangshiAttack5"));
        var createSoundOnEmitModel = new CreateSoundOnProjectileCreatedModel("CreateSoundOnProjectileCreatedModel_",
            sound1, sound2, sound3, sound4, sound5, "JiangshiHiss");
        weapon.AddBehavior(createSoundOnEmitModel);

        var linkRadiusToRange =
            new LinkProjectileRadiusToTowerRangeModel("LinkProjectileRadiusToTowerRangeModel_Jiangshi",
                weapon.projectile, 47, 6, 1);
        towerModel.AddBehavior(linkRadiusToRange);
        towerModel.GetAttackModel().AddWeapon(weapon);
    }

    public class LifeStealOverlay : ModBloonOverlay
    {
        public override string BaseOverlay => "MonkeySubRadiation";

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            node.GetRenderer<SpriteRenderer>().material.color = new Color(3, 0, 0);
        }
    }

    [HarmonyPatch(typeof(Bloon), nameof(Bloon.DestroyBloon))]
    private static class Bloon_DestroyBloon
    {
        private static float bloonsPopped;

        public static void Postfix(Projectile projectile, float amount)
        {
            var model = projectile.projectileModel;
            if (!model.id.StartsWith("h25lifesteal_")) return;

            var multiplier = float.Parse(model.id.Split('_')[^1]);
            bloonsPopped += multiplier * amount;
            var livesToGain = 0;

            ModHelper.Log<Halloween2025>(bloonsPopped);

            while (bloonsPopped >= 100)
            {
                bloonsPopped -= 100;
                livesToGain++;
            }

            if (livesToGain > 0)
            {
                InGame.instance.AddMaxHealth(livesToGain);
                InGame.instance.AddHealth(livesToGain);
            }
        }
    }
}