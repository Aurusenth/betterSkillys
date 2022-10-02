﻿using TKR.Shared.resources;
using System;
using TKR.WorldServer.core.miscfile.thread;

namespace TKR.WorldServer.core.objects
{
    public partial class Player
    {
        private int _canTpCooldownTime;
        private int _newbieTime;

        public bool IsVisibleToEnemy()
        {
            if (_newbieTime > 0)
                return false;

            if (HasConditionEffect(ConditionEffectIndex.Paused))
                return false;

            if (HasConditionEffect(ConditionEffectIndex.Invisible))
                return false;

            if (HasConditionEffect(ConditionEffectIndex.Hidden))
                return false;

            return true;
        }

        public bool TPCooledDown() => !(_canTpCooldownTime > 0);

        internal void RestartTPPeriod() => _canTpCooldownTime = 0;

        internal void SetNewbiePeriod() => _newbieTime = 3000;

        internal void SetTPDisabledPeriod() => _canTpCooldownTime = 10000;

        private bool CanHpRegen() => !(HasConditionEffect(ConditionEffectIndex.Bleeding) || HasConditionEffect(ConditionEffectIndex.Sick));

        private bool CanMpRegen() => !(HasConditionEffect(ConditionEffectIndex.Quiet) || HasConditionEffect(ConditionEffectIndex.NinjaSpeedy));

        private void HandleEffects(ref TickTime time)
        {
            if (Client == null || Client.Account == null) return;

            if (Client.Account.Hidden && !HasConditionEffect(ConditionEffectIndex.Hidden))
            {
                RemoveCondition(ConditionEffectIndex.Hidden);
                RemoveCondition(ConditionEffectIndex.Invincible);
                GameServer.ConnectionManager.Clients[Client].Hidden = true;
            }

            if (HasConditionEffect(ConditionEffectIndex.Quiet) && MP > 0)
                MP = 0;

            if (HasConditionEffect(ConditionEffectIndex.Bleeding) && HP > 1)
            {
                HP -= (int)(20 * time.DeltaTime); // 20 per second
                if (HP < 1)
                    HP = 1;
            }

            if (HasConditionEffect(ConditionEffectIndex.NinjaSpeedy))
            {
                if (HasTalismanEffect(TalismanEffectType.BloodExchange))
                {
                    HP = Math.Max(1, (int)(HP - 20 * time.DeltaTime));
                    if (HP == 1)
                        RemoveCondition(ConditionEffectIndex.NinjaSpeedy);
                }
                else
                {
                    MP = Math.Max(0, (int)(MP - 10 * time.DeltaTime));
                    if (MP == 0)
                        RemoveCondition(ConditionEffectIndex.NinjaSpeedy);
                }
            }

            if (HasConditionEffect(ConditionEffectIndex.NinjaBerserk))
            {
                if (HasTalismanEffect(TalismanEffectType.BloodExchange))
                {
                    HP = Math.Max(1, (int)(HP - 20 * time.DeltaTime));
                    if (HP == 1)
                        RemoveCondition(ConditionEffectIndex.NinjaBerserk);
                }
                else
                {
                    MP = Math.Max(0, (int)(MP - 10 * time.DeltaTime));
                    if (MP == 0)
                        RemoveCondition(ConditionEffectIndex.NinjaBerserk);
                }
            }

            if (_newbieTime > 0)
            {
                _newbieTime -= time.ElapsedMsDelta;
                if (_newbieTime < 0)
                    _newbieTime = 0;
            }

            if (_canTpCooldownTime > 0)
            {
                _canTpCooldownTime -= time.ElapsedMsDelta;
                if (_canTpCooldownTime <= 0)
                    _canTpCooldownTime = 0;
            }
        }
    }
}
