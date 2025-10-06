using CustomPlayerEffects;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.Scp049Events;
using LabApi.Events.Arguments.Scp096Events;
using LabApi.Events.Arguments.Scp173Events;
using LabApi.Events.Arguments.Scp3114Events;
using LabApi.Events.CustomHandlers;
using PlayerRoles;
using PlayerStatsSystem;
using UncomplicatedCustomRoles.Extensions;

namespace Scp999;

public class EventHandlers : CustomEventsHandler
{
    public override void OnScp096AddingTarget(Scp096AddingTargetEventArgs ev)
    {
        if (ev.Target.TryGetSummonedInstance(out var role) && role.Role.Id == 999)
            ev.IsAllowed = false;

        base.OnScp096AddingTarget(ev);
    }

    public override void OnScp049ResurrectingBody(Scp049ResurrectingBodyEventArgs ev)
    {
        if (ev.Target.TryGetSummonedInstance(out var role) && role.Role.Id == 999)
            ev.IsAllowed = false;

        base.OnScp049ResurrectingBody(ev);
    }

    public override void OnScp049UsingSense(Scp049UsingSenseEventArgs ev)
    {
        if (ev.Target.TryGetSummonedInstance(out var role) && role.Role.Id == 999)
            ev.IsAllowed = false;

        base.OnScp049UsingSense(ev);
    }

    public override void OnScp173AddingObserver(Scp173AddingObserverEventArgs ev)
    {
        if (ev.Target.TryGetSummonedInstance(out var role) && role.Role.Id == 999)
            ev.IsAllowed = false;

        base.OnScp173AddingObserver(ev);
    }

    public override void OnScp3114StrangleStarting(Scp3114StrangleStartingEventArgs ev)
    {
        if (ev.Target.TryGetSummonedInstance(out var role) && role.Role.Id == 999)
            ev.IsAllowed = false;

        base.OnScp3114StrangleStarting(ev);
    }

    public override void OnPlayerHurting(PlayerHurtingEventArgs ev)
    {
        if (ev.Attacker is not { IsSCP: true } || !ev.Player.TryGetSummonedInstance(out var role) ||
            role.Role.Id != 999) return;

        if (ev.DamageHandler.DeathScreenText == DeathTranslations.Crushed.DeathscreenTranslation)
            ev.IsAllowed = false;

        if (ev.Attacker.Role is RoleTypeId.Scp049)
            ev.Player.DisableEffect<CardiacArrest>();

        ev.IsAllowed = false;
        base.OnPlayerHurting(ev);
    }

    public override void OnPlayerEnteringPocketDimension(PlayerEnteringPocketDimensionEventArgs ev)
    {
        if (ev.Player.TryGetSummonedInstance(out var role) && role.Role.Id == 999)
            ev.IsAllowed = false;

        base.OnPlayerEnteringPocketDimension(ev);
    }

    public override void OnPlayerCuffing(PlayerCuffingEventArgs ev)
    {
        if (ev.Target.TryGetSummonedInstance(out var role) && role.Role.Id == 999)
            ev.IsAllowed = false;

        base.OnPlayerCuffing(ev);
    }
}