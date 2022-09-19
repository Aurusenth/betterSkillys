﻿using TKR.Shared;
using TKR.WorldServer.core.miscfile.thread;
using TKR.WorldServer.core.worlds.logic;
using TKR.WorldServer.core.objects;

namespace TKR.WorldServer.core.commands
{
    public abstract partial class Command
    {
        internal class ToggleLoop : Command
        {
            public override RankingType RankRequirement => RankingType.Admin;
            public override string CommandName => "toggleloop";

            protected override bool Process(Player player, TickTime time, string args)
            {
                RootWorldThread.TickWithSleep = !RootWorldThread.TickWithSleep;
                if (RootWorldThread.TickWithSleep)
                    player.SendInfo($"Removed Thread Sleep from RootWorldThread");
                else
                    player.SendInfo($"Added Thread Sleep back to RootWorldThread");
                return true;
            }
        }

        internal class A : Command
        {
            public override RankingType RankRequirement => RankingType.Admin;
            public override string CommandName => "spawn15";

            protected override bool Process(Player player, TickTime time, string args)
            {
                // + 1 from automated realm creation system
                for (var i = 0; i < 14; i++)
                    player.World.GameServer.WorldManager.Nexus.PortalMonitor.CreateNewRealm();
                player.SendInfo("Creating 15 kingdoms");
                return true;
            }
        }
        internal class B : Command
        {
            public override RankingType RankRequirement => RankingType.Admin;
            public override string CommandName => "remove15";

            protected override bool Process(Player player, TickTime time, string args)
            {
                foreach(var world in player.GameServer.WorldManager.GetWorlds())
                {
                    if(world is RealmWorld rw)
                        rw.CloseRealm();
                }
                player.SendInfo("Closing all kingdoms");
                return true;
            }
        }

        internal class CloseRealm : Command
        {
            public override RankingType RankRequirement => RankingType.Admin;
            public override string CommandName => "closerealm";
            public override string Alias => "cr";

            protected override bool Process(Player player, TickTime time, string args)
            {
                if (!(player.World is RealmWorld gw))
                {
                    player.SendError("You must be in a kingdom to clsoe it.");
                    return true;
                }

                if (!gw.CloseRealm())
                {
                    player.SendError("Kingdom has already closed its borders.");
                    return true;
                }

                player.SendInfo("You have closed the kingdom borders.");
                return true;
            }
        }
    }
}
