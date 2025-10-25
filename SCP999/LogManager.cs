using System;
using LabApi.Features.Console;

namespace Scp999;

internal static class LogManager
{
    private static bool DebugEnabled => Scp999.Instance?.Config?.Debug == true;

    public static void Debug(string message)
    {
        if (!DebugEnabled)
            return;

        Logger.Raw($"[DEBUG] [{Scp999.Instance?.Name ?? "Scp999"}] {message}", ConsoleColor.Green);
    }

    public static void Info(string message, ConsoleColor color = ConsoleColor.Cyan)
    {
        Logger.Raw($"[INFO] [{Scp999.Instance?.Name ?? "Scp999"}] {message}", color);
    }

    public static void Warn(string message)
    {
        Logger.Warn(message);
    }

    public static void Error(string message)
    {
        Logger.Raw($"[ERROR] [{Scp999.Instance?.Name ?? "Scp999"}] {message}", ConsoleColor.Red);
    }
}