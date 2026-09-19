using System;
using System.IO;
using System.Linq;
using Shared.resources;
using Shared.terrain;
using WorldServer.core.terrain;
using WorldServer.core.structures;
using WorldServer.core.worlds;

namespace WorldServer.core.setpieces
{
    // Simple setpiece loader for an embedded or on-disk .wmap named "ShtrsDefenseSystem"
    // If no .wmap is found, falls back to a small procedural placeholder.
    internal class ShtrsDefenseSystem : ISetPiece
    {
        // Default size used by placement logic. If your .wmap has a different size
        // you can change this to match it. RenderSetPiece will still try to load the
        // actual .wmap and project it (which uses its own dimensions).
        public override int Size => 60;

        public override void RenderSetPiece(World world, IntPoint pos)
        {
            // Attempt to load the .jm (json map) from the server resources and render it
            try
            {
                var resourcePath = world.GameServer.Resources.ResourcePath ?? AppDomain.CurrentDomain.BaseDirectory;
                var candidate = Path.Combine(resourcePath, "worlds", "Setpieces", "Shtrs Defense System.jm");
                if (File.Exists(candidate))
                {
                    var json = File.ReadAllText(candidate);
                    var data = Json2Wmap.Convert(world.GameServer.Resources.GameData, json);
                    SetPieces.RenderFromData(world, pos, data);
                    return;
                }
            }
            catch (Exception)
            {
                // fallthrough to fallback
            }

            // Fallback: simple square floor so setpiece is visible even if .jm not found or failed
            var dat = world.GameServer.Resources.GameData;
            var floorId = (ushort)(dat.IdToTileType.ContainsKey("Jungle Temple Floor") ? dat.IdToTileType["Jungle Temple Floor"] : 0);

            for (var x = 0; x < Size; x++)
                for (var y = 0; y < Size; y++)
                {
                    if (!world.Map.Contains(pos.X + x, pos.Y + y))
                        continue;

                    var tile = world.Map[x + pos.X, y + pos.Y];
                    tile.TileId = floorId;
                    tile.ObjType = 0;
                    tile.UpdateCount++;
                }
        }
    }
}
