using System.Collections.Generic;
using CustomPlayerEffects;
using LabApi.Features.Wrappers;
using MEC;
using RoleAPI.API.Interfaces;
using RoleAPI.API.Managers;
using UnityEngine;

namespace Scp999.Features.Abilities;

public class AnimationAbility : Ability
{
    public override string Name => "Dance";
    public override string Description => "Play a random funny animation";
    public override int KeyId => 9994;
    public override KeyCode KeyCode => KeyCode.T;
    public override float Cooldown => 15f;

    protected override void ActivateAbility(Player player, ObjectManager manager)
    {
        player.EnableEffect<Ensnared>(duration: 30f);

        var rand = Random.Range(0, 100) + 1;
        switch (rand)
        {
            // throwing balls
            case > 0 and <= 15:
            {
                manager.Animator?.Play("FunAnimation1");
                if (!AudioClipStorage.AudioClips.ContainsKey("circus"))
                    LogManager.Error(
                        "[Scp999] The audio file 'circus.ogg' was not found for playback. Please ensure the file is placed in the correct directory.");
                else
                    manager.AudioPlayer?.AddClip("circus");
            }
                break;

            // Jump x3
            case > 15 and <= 60:
            {
                manager.Animator?.Play("FunAnimation2");
                if (!AudioClipStorage.AudioClips.ContainsKey("jump"))
                    LogManager.Error(
                        "[Scp999] The audio file 'jump.ogg' was not found for playback. Please ensure the file is placed in the correct directory.");
                else
                    manager.AudioPlayer?.AddClip("jump");
            }
                break;

            // Shrinking
            case > 60 and <= 90:
            {
                manager.Animator?.Play("FunAnimation3");
                if (!AudioClipStorage.AudioClips.ContainsKey("funnytoy"))
                    LogManager.Error(
                        "[Scp999] The audio file 'funnytoy.ogg' was not found for playback. Please ensure the file is placed in the correct directory.");
                else
                    manager.AudioPlayer?.AddClip("funnytoy");
            }
                break;

            // UwU - Secret animation
            case > 90:
            {
                manager.Animator?.Play("FunAnimation4");
                if (!AudioClipStorage.AudioClips.ContainsKey("uwu"))
                    LogManager.Error(
                        "[Scp999] The audio file 'uwu.ogg' was not found for playback. Please ensure the file is placed in the correct directory.");
                else
                    manager.AudioPlayer?.AddClip("uwu");
            }
                break;
        }

        Timing.RunCoroutine(CheckEndOfAnimation(player, manager.Animator));
    }

    private static IEnumerator<float> CheckEndOfAnimation(Player player, Animator animator)
    {
        yield return Timing.WaitForSeconds(0.1f);
        var initialClipName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

        while (true)
        {
            var clipInfo = animator.GetCurrentAnimatorClipInfo(0);
            if (clipInfo[0].clip.name != initialClipName)
            {
                player.DisableEffect<Ensnared>();
                yield break;
            }

            yield return Timing.WaitForSeconds(0.5f);
        }
    }
}