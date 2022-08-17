﻿using common;
using common.isc.data;
using System.Linq;
using System.Text;
using wServer.core.objects;

namespace wServer.core.commands
{
    public abstract partial class Command
    {
        internal class Online : Command
        {
            public override string CommandName => "online";

            protected override bool Process(Player player, TickTime time, string args)
            {
                var playerSvr = player.GameServer.Configuration.serverInfo.name;
                var servers = player.GameServer.InterServerManager.GetServerList();
                var s = servers.Where(_ => _.type != ServerType.Account);
                var sb = new StringBuilder($"There are: {s.Sum(_ => _.players)} Online Across: {string.Join(", ", s.Select(_ => _.name))}: ");
                player.SendInfo(sb.ToString());
                return true;
            }
        }
    }
}