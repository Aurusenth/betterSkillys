using System;
using Shared;
using WorldServer.core.objects;
using WorldServer.core.structures;
using NLog;
using WorldServer.utils;
using WorldServer.core.worlds;

namespace WorldServer.core.commands
{
    public abstract partial class Command
    {
        internal class Smite : Command
        {
            public override RankingType RankRequirement => RankingType.Admin;
            public override string CommandName => "smite";

            private static readonly Logger Log = LogManager.GetCurrentClassLogger();

            protected override bool Process(Player player, TickTime time, string args)
            {
                if (string.IsNullOrWhiteSpace(args))
                {
                    player.SendError("Usage: /smite [radius] [damage]");
                    return false;
                }

                var parts = args.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 2 || !int.TryParse(parts[0], out int radius) || !int.TryParse(parts[1], out int damage))
                {
                    player.SendError("Usage: /smite [radius] [damage]");
                    return false;
                }

                if (radius <= 0 || damage <= 0)
                {
                    player.SendError("Radius and damage must be positive integers.");
                    return false;
                }

                int affected = 0;
                Log.Info("Admin {0} issued /smite {1} {2} in world {3}", player.Name, radius, damage, player.World.IdName);

                // Use the world's AOE helper to enumerate enemies within radius
                player.World.AOE(new Position { X = player.X, Y = player.Y }, radius, false, ent =>
                {
                    if (ent is Enemy enemy)
                    {
                        try
                        {
                            var dealt = enemy.Damage(player, ref time, damage, false);
                            affected++;
                            Log.Info("Smite: dealt {0} damage to enemy {1} (type {2}) at {3},{4}", dealt, enemy.Id, enemy.ObjectType, enemy.X, enemy.Y);
                        }
                        catch (Exception ex)
                        {
                            Log.Warn(ex, "Error while smiting enemy {0}", enemy.Id);
                        }
                    }
                });

                player.SendInfo($"Smited {affected} enemies within radius {radius} for {damage} damage.");
                Log.Info("Smite complete: {0} enemies affected.", affected);
                return true;
            }
        }
    }
}
