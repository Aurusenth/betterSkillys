using Shared;
using Shared.database.character.inventory;
using Shared.resources;
using System.Collections.Generic;
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

                if (splitArgs.Length > 1 && int.TryParse(splitArgs[0], out int requestedAmount))
                {
                    amount = requestedAmount;
                    itemName = string.Join(" ", splitArgs.Skip(1));
                }

                if (!gameData.DisplayIdToObjectType.TryGetValue(itemName, out ushort objType))
                {
                    if (!gameData.IdToObjectType.TryGetValue(itemName, out objType))
                    {
                        player.SendError($"unable to find item: {itemName}!");
                        return false;
                    }
                }

                if (!gameData.Items.ContainsKey(objType))
                {
                    player.SendError($"unable to find item: {itemName}!");
                    return false;
                }

                var item = gameData.Items[objType];

                // 1. Pobieramy ID dla Soulbound Bag (zazwyczaj 0x0503)
                // W Twoim systemie "Loot Bag 6" to White Bag, "Loot Bag 5" to fioletowy (SB).
                if (!gameData.IdToObjectType.TryGetValue("Loot Bag 5", out var bagType))
                {
                    bagType = 0x0503; // Fallback
                }

                // 2. Tworzymy kontener dokładnie tak, jak robi to Twoja metoda DropBag
                var container = new Container(player.GameServer, bagType, 120000, true);

                // 3. Wypełniamy ekwipunek kontenera
                for (int j = 0; j < amount && j < 8; j++)
                {
                    // Obsługa stackowalnych przedmiotów (zgodnie z Twoim kodem Loot.cs)
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

                // 4. Przypisujemy właściciela (Twoje AccountId w tablicy)
                container.BagOwners = new int[] { player.AccountId };

                // 5. Ustawiamy pozycję na graczu
                container.Move(player.X, player.Y);

                // 6. Spawnujemy w świecie (używając metody, którą Twój Loot.cs potwierdził: World.EnterWorld)
                player.World.EnterWorld(container);

                player.SendInfo($"Gave {amount}x {itemName} in a Soulbound Bag.");
                return true;
            }
        }
    }
}