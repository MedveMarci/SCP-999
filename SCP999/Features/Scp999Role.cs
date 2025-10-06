using System.Collections.Generic;
using PlayerRoles;
using RoleAPI.API;
using RoleAPI.API.Configs;
using RoleAPI.API.CustomModules;
using Scp999.Features.Abilities;
using UncomplicatedCustomRoles.API.Enums;
using UncomplicatedCustomRoles.API.Features;
using UncomplicatedCustomRoles.API.Features.Behaviour;
using UncomplicatedCustomRoles.API.Features.CustomModules;
using UncomplicatedCustomRoles.Manager;
using UnityEngine;

namespace Scp999.Features;

public class Scp999Role : ExtendedRole
{
    public override int Id { get; set; } = 999;
    public override string Name { get; set; } = "<color=#960018>SCP-999</color>";
    public override bool OverrideRoleName { get; set; } = true;
    public override string Nickname { get; set; } = null;
    public override string CustomInfo { get; set; } = "<color=#960018>Other Alive</color>";
    public override string BadgeName { get; set; } = "";
    public override string BadgeColor { get; set; } = "";
    public override string SpawnHint { get; set; } = "";
    public override RoleTypeId Role { get; set; } = RoleTypeId.Tutorial;
    public override Team? Team { get; set; } = PlayerRoles.Team.OtherAlive;
    public override RoleTypeId RoleAppearance { get; set; } = RoleTypeId.Tutorial;
    public override List<Team> IsFriendOf { get; set; } = [];

    public override HealthBehaviour Health { get; set; } = new()
    {
        Amount = 1500,
        Maximum = 1500
    };

    public override List<Effect> Effects { get; set; } =
    [
        new()
        {
            EffectType = "Fade",
            Duration = -1,
            Intensity = 255,
            Removable = false
        },
        new()
        {
            EffectType = "Slowness",
            Duration = -1,
            Intensity = 25,
            Removable = false
        },
        new()
        {
            EffectType = "SilentWalk",
            Duration = -1,
            Intensity = 255,
            Removable = false
        },
        new()
        {
            EffectType = "Ghostly",
            Duration = -1,
            Intensity = 255,
            Removable = false
        }
    ];

    public override bool CanEscape { get; set; } = false;

    public override string SpawnBroadcast { get; set; } =
        "<color=#ffa500>\ud83d\ude04 You are SCP-999 - The tickle monster! \ud83d\ude04\n" +
        "Heal Humans, dance and calm down SCPs in facility\n" +
        "Use abilities by clicking on the buttons</color>";

    public override ushort SpawnBroadcastDuration { get; set; } = 15;
    public override List<ItemType> Inventory { get; set; } = [];
    public override Dictionary<ItemType, ushort> Ammo { get; set; } = [];
    public override float DamageMultiplier { get; set; } = 0;

    public override SpawnBehaviour SpawnSettings { get; set; } = new()
    {
        CanReplaceRoles =
        [
            RoleTypeId.FacilityGuard, RoleTypeId.Scientist, RoleTypeId.ClassD
        ],
        MinPlayers = 5,
        MaxPlayers = 1,
        SpawnChance = 5,
        Spawn = SpawnType.RoleSpawn,
        SpawnRoles = [RoleTypeId.Scp096]
    };

    public override SchematicConfig SchematicConfig { get; set; } = new()
    {
        SchematicName = "SCP999",
        Offset = new Vector3(0f, -0.75f, 0f),
        Rotation = new Vector3(0f, 0f, 0f)
    };

    public override HintConfig HintConfig { get; set; } = new()
    {
        Text = "<align=right><size=50><color=#ffa500>\ud83d\ude06 <b>SCP-999</b></color></size>\n" +
               "Abilities:\n" +
               "<color=%color%>Yippee {0}</color>\n" +
               "<color=%color%>Hello {1}</color>\n" +
               "<color=%color%>Heal {2}</color>\n" +
               "<color=%color%>Dance {3}</color>\n" +
               "\n<size=18>if you cant use abilities\nremove \u2b50 in settings</size></align>",
        AvailableAbilityColor = "#ffa500",
        UnavailableAbilityColor = "#966100"
    };

    public override AudioConfig AudioConfig { get; set; } = new()
    {
        Name = "scp999",
        Volume = 100,
        IsSpatial = true,
        MinDistance = 5f,
        MaxDistance = 15f
    };

    public override AbilityConfig AbilityConfig { get; set; } = new()
    {
        AbilityTypes =
        [
            typeof(YippeeAbility),
            typeof(HelloAbility),
            typeof(HealAbility),
            typeof(AnimationAbility)
        ]
    };

    public override void OnSpawned(SummonedCustomRole role)
    {
        role.AddModule(
            typeof(CustomScpAnnouncer),
            new Dictionary<string, object> { { "name", "SCP999" } }
        );
        role.AddModule(typeof(ColorfulNickname), new Dictionary<string, object> { { "color", "#960018" } });
        role.AddModule(typeof(ColorfulRaName), new Dictionary<string, object> { { "color", "#960018" } });
        role.AddModule(typeof(NoBloodDecal));
        role.AddModule(typeof(CantPickupAnyItem));
        role.AddModule(typeof(DisableAnyInteraction));
        role.AddModule(typeof(RunAway));
        base.OnSpawned(role);
    }
}