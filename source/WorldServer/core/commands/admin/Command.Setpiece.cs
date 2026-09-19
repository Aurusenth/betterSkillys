using System;
using System.Linq;
using Shared;
using Shared.resources;
using WorldServer.core.objects;
using WorldServer.core.setpieces;
using WorldServer.core.structures;
using WorldServer.core.worlds;
using WorldServer.core.worlds.impl;

namespace WorldServer.core.commands
{
    public abstract partial class Command
    {
        internal class Setpiece : Command
        {
            public override RankingType RankRequirement => RankingType.Admin;
            public override string CommandName => "setpiece";

            protected override bool Process(Player player, TickTime time, string setPiece)
            {
                if ((player.World is NexusWorld) || player.World.Id == -10 || player.World.InstanceType == WorldResourceInstanceType.Guild)
                {
                    player.SendError("You cannot use this command here");
                    return false;
                }

                if (string.IsNullOrWhiteSpace(setPiece))
                {
                    var type = typeof(ISetPiece);
                    var types = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(s => s.GetTypes())
                        .Where(p => type.IsAssignableFrom(p) && !p.IsAbstract);
                    var msg = types.Aggregate(
                        "Valid SetPieces: ", (c, p) => c + p.Name + ", ");
                    player.SendInfo(msg.Substring(0, msg.Length - 2) + ".");
                    return false;
                }

                if (!player.World.IdName.Equals("Nexus"))
                {
                    try
                    {
                        ISetPiece piece = (ISetPiece)Activator.CreateInstance(Type.GetType("WorldServer.core.setpieces." + setPiece, true, true));
                        // Center the setpiece on the player's current position instead of offsetting it
                        var size = piece.Size;
                        var px = (int)player.X - size / 2;
                        var py = (int)player.Y - size / 2;
                        // clamp to map bounds
                        px = Math.Clamp(px, 0, player.World.Map.Width - size);
                        py = Math.Clamp(py, 0, player.World.Map.Height - size);
                        piece.RenderSetPiece(player.World, new IntPoint(px, py));
                        return true;
                    }
                    catch (Exception)
                    {
                        player.SendError("Invalid SetPiece.");
                        return false;
                    }
                }
                else
                {
                    player.SendInfo("/setpiece not allowed in Nexus.");
                    return false;
                }
            }
        }
    }
}
