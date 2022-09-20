﻿using System.Collections.Generic;
using TKR.Shared.resources;
using TKR.WorldServer.core.miscfile.structures;
using TKR.WorldServer.core.miscfile.thread;
using TKR.WorldServer.networking;
using TKR.WorldServer.networking.packets.outgoing;

namespace TKR.WorldServer.core.objects
{
    public partial class Player
    {
        private bool anticheat;
        private long l;

        private const float MoveSpeedThreshold = 1.1f;

        public float MoveMultiplier = 1f;
        public int MoveTime;
        public int AwaitingMoves;
        public Queue<int> AwaitingGoto;
        public float PushX;
        public float PushY;

        /*public bool ValidMove(int time, Position pos)
        {
            int diff = time - MoveTime;
            float speed = Stats.GetSpeed() * diff * MoveSpeedThreshold;
            Position pushedServer = new Position(X - (diff * PushX), Y - (diff * PushY));
            if (pos.Distance(pos, pushedServer) > speed && pos.Dist(new Position() { X = RealX, Y = RealY }) > speed)
            {
                return false;
            }
            return true;
        }*/

        public void ForceGroundHit(TickTime time, Position pos, int timeHit)
        {
            if (World == null || World.Map == null || HasConditionEffect(ConditionEffectIndex.Paused) || HasConditionEffect(ConditionEffectIndex.Invincible))
                return;

            var tile = World.Map[(int)pos.X, (int)pos.Y];
            var objDesc = tile.ObjType == 0 ? null : GameServer.Resources.GameData.ObjectDescs[tile.ObjType];
            var tileDesc = GameServer.Resources.GameData.Tiles[tile.TileId];

            if (tileDesc.Damaging && (objDesc == null || !objDesc.ProtectFromGroundDamage))
            {
                int dmg = (int)Client.Random.NextIntRange((uint)tileDesc.MinDamage, (uint)tileDesc.MaxDamage);

                HP -= dmg;

                if (HP <= 0)
                {
                    Death(tileDesc.ObjectId, tile: tile);
                    return;
                }

                World.BroadcastIfVisibleExclude(new Damage()
                {
                    TargetId = Id,
                    DamageAmount = dmg,
                    Kill = HP <= 0,
                }, this, this);
                anticheat = true;
            }
        }

        public void GroundEffect(TickTime time)
        {
            if (World == null || World.Map == null || HasConditionEffect(ConditionEffectIndex.Paused) || HasConditionEffect(ConditionEffectIndex.Invincible) || HasConditionEffect(ConditionEffectIndex.Stasis))
                return;

            var tile = World.Map[(int)X, (int)Y];
            var objDesc = tile.ObjType == 0 ? null : GameServer.Resources.GameData.ObjectDescs[tile.ObjType];
            var tileDesc = GameServer.Resources.GameData.Tiles[tile.TileId];

            //if (tileDesc.Effects != null)
            //    ApplyConditionEffect(tileDesc.Effects);

            if (time.TotalElapsedMs - l > 500 && !anticheat)
            {
                if (HasConditionEffect(ConditionEffectIndex.Paused) ||
                    HasConditionEffect(ConditionEffectIndex.Invincible))
                    return;

                if (tileDesc.Damaging && (objDesc == null || !objDesc.ProtectFromGroundDamage))
                {
                    int dmg = (int)Client.Random.NextIntRange((uint)tileDesc.MinDamage, (uint)tileDesc.MaxDamage);

                    HP -= dmg;

                    if (HP <= 0)
                    {
                        Death(tileDesc.ObjectId, tile: tile);
                        return;
                    }

                    World.BroadcastIfVisibleExclude(new Damage()
                    {
                        TargetId = Id,
                        DamageAmount = dmg,
                        Kill = HP <= 0,
                    }, this, this);

                    l = time.TotalElapsedMs;
                }
            }
        }
    }
}
