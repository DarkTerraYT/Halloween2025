using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Halloween2025.Assets.Towers;
using Il2CppAssets.Scripts.Models.Audio;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.TowerFilters;
using Il2CppAssets.Scripts.Unity;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppNinjaKiwi.Common.ResourceUtils;

namespace Halloween2025.Towers.GhostMonkey.TopPath;

public class SoulShakingScreams : ModUpgrade<GhostMonkey>
{
    public override int Path => Middle;
    public override int Tier => 4;
    public override int Cost => 5500;

    public override string Description =>
        "Ability, Mega Scream: These screams stun all bloons, and causes all monkeys to attack faster.";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var ability = Game.instance.model.GetTowerFromId("MortarMonkey-050").GetAbility().Duplicate();
        ability.icon = GetSpriteReferenceOrDefault(Icon);
        ability.GetBehavior<CreateSoundOnAbilityModel>().sound =
            new SoundModel("MegaScream_1", GetAudioClipReference("GhostAA1"));
        ability.RemoveBehavior<TurboModel>();
        ability.GetBehavior<ActivateAttackModel>().lifespan = 6;
        ability.GetBehavior<ActivateAttackModel>().attacks[0].weapons[0].GetDescendant<DamageModel>().damage = 0;
        ability.GetBehavior<CreateEffectOnAbilityModel>().centerEffect = false;
        ability.GetBehavior<CreateEffectOnAbilityModel>().useAttackTransform = false;
        ability.GetBehavior<CreateEffectOnAbilityModel>().effectModel.assetId =
            new PrefabReference(GetDisplayGUID<SuperScreamEffect>());

        var ghostMonkeyFilter = new FilterInBaseTowerIdModel("FilterInBaseTowerIdModel",
            new Il2CppStringArray([TowerID<GhostMonkey>()]));
        ability.AddBehavior(new ActivateRateSupportZoneModel("ActivateRateSupportZoneModel_", "SoulShakingScreams",
            false, 0.75f, 999, 99999999, true, 10, null, "", "",
            new Il2CppReferenceArray<TowerFilterModel>([ghostMonkeyFilter]), false));

        towerModel.AddBehavior(ability);
    }
}