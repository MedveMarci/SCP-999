using LabApi.Features.Wrappers;
using RoleAPI.API.Interfaces;
using RoleAPI.API.Managers;
using UnityEngine;

namespace Scp999.Features.Abilities;

public class YippeeAbility : Ability
{
    public override string Name => "Yippee";
    public override string Description => "Play just a funny Yippee sound";
    public override int KeyId => 9990;
    public override KeyCode KeyCode => KeyCode.Q;
    public override float Cooldown => 3f;

    protected override void ActivateAbility(Player player, ObjectManager manager)
    {
        if (manager.AudioPlayer is null)
            return;

        // I would like a default yippee-tbh1.ogg to be used more often than yippee-tbh2.ogg
        var value = 1;
        var chance = Random.Range(0, 100);
        if (chance >= 60) value = 2;
        if (!AudioClipStorage.AudioClips.ContainsKey($"yippee-tbh{value}"))
            LogManager.Error(
                $"[Scp999] The audio file 'yippee-tbh{value}.ogg' was not found for playback. Please ensure the file is placed in the correct directory.");
        else
            manager.AudioPlayer.AddClip($"yippee-tbh{value}", 0.5f);
    }
}