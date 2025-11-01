using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Audio;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;

namespace Halloween2025.Towers.GhostMonkey.TopPath;

public class SuperScreams : ModUpgrade<GhostMonkey>
{
    public override int Path => Middle;
    public override int Tier => 5;
    public override int Cost => 50000;

    public override string Description =>
        "Mega scream makes monkeys attack even faster, and deals lots of damage to bloons.";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var ability = towerModel.GetAbility();
        ability.icon = GetSpriteReferenceOrDefault(Icon);
        ability.GetBehavior<CreateSoundOnAbilityModel>().sound =
            new SoundModel("MegaScream_2", GetAudioClipReference("GhostAA2"));
        var abilityWeapon = ability.GetBehavior<ActivateAttackModel>().attacks[0].weapons[0];
        abilityWeapon.projectile.GetDamageModel().damage = 1000;

        ability.GetBehavior<ActivateRateSupportZoneModel>().rateModifier *= 0.5f;
    }
}