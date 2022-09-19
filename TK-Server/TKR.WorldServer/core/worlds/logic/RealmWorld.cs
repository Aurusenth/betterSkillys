﻿using TKR.Shared.resources;
using TKR.WorldServer.core.miscfile;
using TKR.WorldServer.core.miscfile.thread;
using TKR.WorldServer.core.objects;
using TKR.WorldServer.core.setpieces;
using TKR.WorldServer.networking;
using TKR.WorldServer.core.objects;

namespace TKR.WorldServer.core.worlds.logic
{
    public class RealmWorld : World
    {
        public bool Closed { get; private set; }
        public KingdomManager KingdomManager { get; private set; }

        public RealmWorld(GameServer gameServer, int id, WorldResource resource) : base(gameServer, id, resource)
        {
            KingdomManager = new KingdomManager(this);
            IsRealm = true;
        }

        public void SetMaxPlayers(int capacity) => MaxPlayers = capacity;

        public override void Init()
        {
            SetPieces.ApplySetPieces(this);
            KingdomManager.Init();
            base.Init();
        }

        public override bool AllowedAccess(Client client) => !Closed || client.Rank.IsAdmin;

        protected override void UpdateLogic(ref TickTime time)
        {
            if (IsPlayersMax())
                GameServer.WorldManager.Nexus.PortalMonitor.ClosePortal(Id);
            else if (!GameServer.WorldManager.Nexus.PortalMonitor.PortalIsOpen(Id))
                GameServer.WorldManager.Nexus.PortalMonitor.OpenPortal(Id);

            KingdomManager.Update(ref time);
            base.UpdateLogic(ref time);
        }

        public override int EnterWorld(Entity entity)
        {
            var ret = base.EnterWorld(entity);
            if (entity is Player player)
                KingdomManager?.OnPlayerEntered(player);
            return ret;
        }

        public override void LeaveWorld(Entity entity)
        {
            // use it if needed
            base.LeaveWorld(entity);
        }

        public void EnemyKilled(Enemy enemy, Player killer)
        {
            if (!enemy.Spawned)
                KingdomManager.OnEnemyKilled(enemy, killer);
        }

        public bool CloseRealm()
        {
            if (Closed)
                return false;
            Closed = true;
            KingdomManager.DisableSpawning = true;
            KingdomManager.CurrentState = KingdomState.Emptying;
            GameServer.WorldManager.Nexus.PortalMonitor.RemovePortal(Id);
            FlagForClose();
            return true;
        }
    }
}
