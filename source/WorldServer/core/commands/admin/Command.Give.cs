using Shared;
using Shared.database.character.inventory;
using Shared.resources;
using System;
using System.Linq;
using WorldServer.core.objects;
using WorldServer.core.objects.containers;
using WorldServer.core.worlds;

namespace WorldServer.core.commands
{
    public abstract partial class Command
    {
        internal class Give : Command
        {
            public override RankingType RankRequirement => RankingType.Admin;
            public override string CommandName => "give";

            protected override bool Process(Player player, TickTime time, string args)
            {
                var gameData = player.GameServer.Resources.GameData;
                var splitArgs = args.Split(' ');
                int amount = 1;
                string itemName = args;

                // Parsowanie argumentów: ilość i nazwa przedmiotu
                if (splitArgs.Length > 1 && int.TryParse(splitArgs[0], out int requestedAmount))
                {
                    amount = requestedAmount;
                    itemName = string.Join(" ", splitArgs.Skip(1));
                }

                // Szukanie przedmiotu w bazie danych
                if (!gameData.DisplayIdToObjectType.TryGetValue(itemName, out ushort objType))
                {
                    if (!gameData.IdToObjectType.TryGetValue(itemName, out objType))
                    {
                        player.SendError($"Unable to find item: {itemName}!");
                        return false;
                    }
                }

                if (!gameData.Items.ContainsKey(objType))
                {
                    player.SendError($"Unable to find item: {itemName}!");
                    return false;
                }

                var item = gameData.Items[objType];

                // 1. Pobranie typu woreczka z Twojego XML
                if (!gameData.IdToObjectType.TryGetValue("Soulbound Loot Bag", out var bagType))
                {
                    bagType = 0x0503; // Fallback do standardowego ID fioletowego woreczka
                }

                // 2. Inicjalizacja kontenera (czas zniknięcia: 120000ms = 2 minuty)
                var container = new Container(player.GameServer, bagType, 120000, true);

                // 3. Wypełnienie ekwipunku woreczka wygenerowanym przedmiotem
                // Uwzględniamy logikę stackowania (ItemData), którą masz w systemie lootu
                for (int j = 0; j < amount && j < 8; j++)
                {
                    if (item.Quantity > 0 && item.QuantityLimit > 0)
                    {
                        container.Inventory.Data[j] = new ItemData()
                        {
                            Stack = item.Quantity,
                            MaxStack = item.QuantityLimit
                        };
                    }
                    container.Inventory[j] = item;
                }

                // 4. Przypisanie właściciela (tylko Ty go zobaczysz)
                container.BagOwners = new int[] { player.AccountId };

                // 5. Przesunięcie woreczka na pozycję gracza
                container.Move(player.X, player.Y);

                // 6. Spawn woreczka w świecie przy użyciu metody potwierdzonej w Loots.cs
                player.World.EnterWorld(container);

                player.SendInfo($"Gave {amount}x {itemName} in a Soulbound Loot Bag!");
                return true;
            }
        }
    }
}