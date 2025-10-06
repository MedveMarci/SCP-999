using System.ComponentModel;
using Scp999.Features;

namespace Scp999;

public class Config
{
    [Description("Enable debug messages in the console.")]
    public bool Debug { get; set; } = false;

    [Description("Distance in which the heal ability can heal players.")]
    public float Distance { get; set; } = 3f;

    [Description("The config for the role")]
    public Scp999Role Scp999Role { get; set; } = new();
}