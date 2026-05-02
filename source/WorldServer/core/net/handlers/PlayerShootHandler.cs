using NLog;
using Shared;
using System;
using WorldServer.core.structures;
using WorldServer.core.worlds;
using WorldServer.networking;

namespace WorldServer.core.net.handlers
{
    public class PlayerShootHandler : IMessageHandler
    {
        public override MessageId MessageId => MessageId.PLAYERSHOOT;

        protected static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public override void Handle(Client client, NetworkReader rdr, ref TickTime tickTime)
        {
            var time = rdr.ReadInt32();
            var bulletId = rdr.ReadInt32();
            var containerType = rdr.ReadInt32();
            var startingPosition = Position.Read(rdr);
            var angle = rdr.ReadSingle();

            var player = client.Player;
            if (!player.GameServer.Resources.GameData.Items.TryGetValue((ushort)containerType, out _))
            {
                client.Disconnect("Attempting to shoot a invalid item", true);
                return;
            }

            var slot = -1;
            var searchRange = client.Account.Admin ? player.Inventory.Length : 2; // Admin przeszukuje wszystko

            for (var i = 0; i < searchRange; i++)
                if (player.Inventory[i] != null && player.Inventory[i].ObjectType == containerType)
                {
                    slot = i;
                    break;
                }

            if (slot == -1)
            {
                client.Disconnect("Attempting to shoot a item that they dont have", true);
                return;
            }

            var newBulletId = player.GetNextBulletId();
            if (newBulletId != bulletId && client.Account.Admin == false) // Jeśli nie jesteś adminem, blokuj
            {
                Log.Warn($"{player.Name} ({player.ObjectDesc.DisplayId ?? player.ObjectDesc.IdName}) has desynced. [bID: {bulletId}, nbID: {newBulletId}");
                return;
            }

            // Jeśli jesteś adminem i wystąpił desync, musimy zrównać licznik serwera z klientem
            if (newBulletId != bulletId && client.Account.Admin)
            {
                while (newBulletId != bulletId)
                    newBulletId = player.GetNextBulletId();
            }

            // todo rate of fire checks

            if (player.Inventory[slot] == null || player.Inventory[slot].ObjectType != containerType)
            {
                client.Disconnect($"Invalid item: {(slot == 0 ? "Weapon" : "Ability")} {player.Inventory[slot].ObjectType} != {containerType}", true);
                return;
            }

            if (slot == 0 && player.World.DisableShooting)
            {
                client.Disconnect("Attempting to shoot in a disabled world", true);
                return;
            }

            if (slot == 1 && player.World.DisableAbilities)
            {
                client.Disconnect("Attempting to activate ability in a disabled world", true);
                return;
            }

            player.PlayerShoot(time, newBulletId, startingPosition, angle, slot);
        }
    }
}
