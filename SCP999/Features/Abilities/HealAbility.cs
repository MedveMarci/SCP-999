using LabApi.Features.Wrappers;
using RoleAPI.API.Interfaces;
using RoleAPI.API.Managers;
using UnityEngine;

namespace Scp999.Features.Abilities;

public class HealAbility : Ability
{
    public override string Name => "Heal";
    public override string Description => "Heal all the players near you";
    public override int KeyId => 9993;
    public override KeyCode KeyCode => KeyCode.R;
    public override float Cooldown => 60f;

    protected override void ActivateAbility(Player player, ObjectManager manager)
    {
        manager.Animator?.Play("HealthAnimation");
        if (!AudioClipStorage.AudioClips.ContainsKey("health"))
            LogManager.Error(
                "[Scp999] The audio file 'health.ogg' was not found for playback. Please ensure the file is placed in the correct directory.");
        else
            manager.AudioPlayer.AddClip("health", 0.5f);

        // Heal all the players in the radius
        foreach (var ply in Player.ReadyList)
        {
            if (player == ply)
                continue;

            if (!(Vector3.Distance(player.Position, ply.Position) < Scp999.Instance.Config!.Distance)) continue;
            ply.Heal(100);
        }
    }
}