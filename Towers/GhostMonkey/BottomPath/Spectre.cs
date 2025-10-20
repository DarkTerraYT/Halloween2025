using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Halloween2025.Assets.Towers;
using Il2CppAssets.Scripts.Models.Audio;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Weapons.Behaviors;

namespace Halloween2025.Towers.GhostMonkey.BottomPath;

public class Spectre : ModUpgrade<GhostMonkey>
{
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var weapon = towerModel.GetWeapon();
        weapon.name = "Spectre";
        var projectile = weapon.projectile;
        
        var sound1 = new SoundModel("SpectreAttack1", GetAudioClipReference("SpectreAttack1"));
        var sound2 = new SoundModel("SpectreAttack2", GetAudioClipReference("SpectreAttack2"));
        var sound3 = new SoundModel("SpectreAttack3", GetAudioClipReference("SpectreAttack3"));
        var createSoundOnEmitModel = new CreateSoundOnProjectileCreatedModel("CreateSoundOnProjectileCreatedModel_",
            sound1, sound2, sound3, sound1, sound2, "SpectreCast");
        weapon.AddBehavior(createSoundOnEmitModel);
        
        TrackTargetModel travelModel = new TrackTargetModel("TrackTargetModel_", 100, true, false, 270, false, 300, true, true, false);
        projectile.AddBehavior(travelModel);
        
        projectile.ApplyDisplay<SpectreBolt>();
    }

    public override string Description => "Soul bolts are now spectre bolts, doing more damage and tracking enemies.";

    public override int Path => Bottom;
    public override int Tier => 3;
    public override int Cost => 820;
}