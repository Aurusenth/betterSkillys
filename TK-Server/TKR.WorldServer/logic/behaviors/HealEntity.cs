﻿using System.Linq;
using TKR.Shared.resources;
using TKR.WorldServer.core.miscfile;
using TKR.WorldServer.core.miscfile.datas;
using TKR.WorldServer.core.miscfile.datas;
using TKR.WorldServer.core.miscfile.thread;
using TKR.WorldServer.core.objects;
using TKR.WorldServer.networking.packets.outgoing;
using TKR.WorldServer.utils;

namespace TKR.WorldServer.logic.behaviors
{
    internal class HealEntity : Behavior
    {
        private readonly int? _amount;
        private readonly string _name;
        private readonly double _range;
        private Cooldown _coolDown;

        public HealEntity(double range, string name = null, int? healAmount = null, Cooldown coolDown = new Cooldown())
        {
            _range = (float)range;
            _name = name;
            _coolDown = coolDown.Normalize();
            _amount = healAmount;
        }

        protected override void OnStateEntry(Entity host, TickTime time, ref object state) => state = 0;

        protected override void TickCore(Entity host, TickTime time, ref object state)
        {
            var cool = (int)state;

            if (cool <= 0)
            {
                if (host.HasConditionEffect(ConditionEffectIndex.Stunned))
                    return;

                foreach (var entity in host.GetNearestEntitiesByName(_range, _name).OfType<Enemy>())
                {
                    var newHp = entity.MaximumHP;

                    if (_amount != null)
                    {
                        var newHealth = (int)_amount + entity.HP;

                        if (newHp > newHealth)
                            newHp = newHealth;
                    }
                    if (newHp != entity.HP)
                    {
                        var n = newHp - entity.HP;

                        entity.HP = newHp;
                        entity.World.BroadcastIfVisible(new ShowEffect() { EffectType = EffectType.Potion, TargetObjectId = entity.Id, Color = new ARGB(0xffffffff) }, entity);
                        entity.World.BroadcastIfVisible(new ShowEffect()
                        {
                            EffectType = EffectType.Trail,
                            TargetObjectId = host.Id,
                            Pos1 = new Position() { X = entity.X, Y = entity.Y },
                            Color = new ARGB(0xffffffff)
                        }, host);
                        entity.World.BroadcastIfVisible(new Notification() { ObjectId = entity.Id, Message = "+" + n, Color = new ARGB(0xff00ff00) }, entity);
                    }
                }

                cool = _coolDown.Next(Random);
            }
            else
                cool -= time.ElapsedMsDelta;

            state = cool;
        }
    }
}
