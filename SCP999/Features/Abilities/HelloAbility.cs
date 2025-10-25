using System.Collections.Generic;
using CustomPlayerEffects;
using LabApi.Features.Wrappers;
using MEC;
using RoleAPI.API.Interfaces;
using RoleAPI.API.Managers;
using UnityEngine;

namespace Scp999.Features.Abilities;

public class HelloAbility : Ability
{
    public override string Name => "Hello";
    public override string Description => "Greet the players and wave your paw";
    public override int KeyId => 9992;
    public override KeyCode KeyCode => KeyCode.F;
    public override float Cooldown => 15f;

    protected override void ActivateAbility(Player player, ObjectManager manager)
    {
        player.EnableEffect<Ensnared>(duration: 3f);
        manager.Animator?.Play("HelloAnimation");

        var clipName = Random.Range(0, 2) == 0 ? "hello" : "hi";
        if (!AudioClipStorage.AudioClips.ContainsKey($"{clipName}"))
            LogManager.Error(
                $"[Scp999] The audio file '{clipName}.ogg' was not found for playback. Please ensure the file is placed in the correct directory.");
        else
            manager.AudioPlayer?.AddClip(clipName, 0.5f);
        Timing.RunCoroutine(CheckEndOfAnimation(player, manager.Animator));
    }

    private static IEnumerator<float> CheckEndOfAnimation(Player player, Animator animator)
    {
        yield return Timing.WaitForSeconds(0.1f);
        while (true)
        {
            var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (!stateInfo.IsName("HelloAnimation"))
            {
                player.DisableEffect<Ensnared>();
                yield break;
            }

            yield return Timing.WaitForSeconds(0.3f);
        }
    }
}