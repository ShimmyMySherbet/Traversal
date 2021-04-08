using System.Collections.Generic;
using SDG.Unturned;
using ShimmyMySherbet.MySQL.EF.Core;
using Traversal.Models;
using Traversal.Models.Databasing;
using Traversal.Models.Databasing.Scoped;

namespace Traversal.PlayerDataProviders
{
    public class InventoryDataProvider : IPlayerDataProvider<PlayerInventory>
    {
        public const string TableName = "PlayerData_Inventory";

        public void CheckSchema(MySQLEntityClient client)
        {
            if (!client.TableExists(TableName))
            {
                client.CreateTable<PlayerInventoryData>(TableName);
            }
        }

        public bool Load(PlayerInventory instance, MySQLEntityClient database)
        {
            PlayerInventoryData data = database.QuerySingle<PlayerInventoryData>($"SELECT * FROM `{TableName}` WHERE PlayerID=@0 AND Slot=@1 AND ServerID=@2 AND Map=@3", instance.channel.GetPlayerID(), instance.channel.GetPlayerSlotID(), Traversal.ServerID, Provider.map);
            if (data == null)
            {
                return false;
            }
            data.Load();
            Items[] items = instance.items;
            var loadPages = data.Content.Pages;
            for (byte page = 0; page < 7; page++)
            {
                var loadContent = loadPages[page];
                items[page].loadSize(loadContent.Width, loadContent.Height);
                for (int i = 0; i < loadContent.Items.Count; i++)
                {
                    var item = loadContent.Items[i];
                    if (Assets.find(EAssetType.ITEM, item.ItemID) != null)
                    {
                        items[page].loadItem(item.X, item.Y, item.Rot, new Item(item.ItemID, item.Amount, item.Quality, item.State));
                    }
                }
            }

            instance.SetValue("wasLoadCalled", true);

            return true;
        }

        public bool Save(PlayerInventory instance, MySQLEntityClient database)
        {
            PlayerInventoryData data = new PlayerInventoryData()
            {
                PlayerID = instance.channel.GetPlayerID(),
                ServerID = Traversal.ServerID,
                Slot = instance.channel.GetPlayerSlotID(),
                Content = new InventoryContent(),
                Map = Provider.map
            };

            for (byte page = 0; page < 7; page++)
            {
                var savePage = instance.items[page];
                InventoryPage outPage = new InventoryPage()
                {
                    Height = savePage.height,
                    Width = savePage.width,
                    Index = page,
                    Items = new List<InventoryItem>()
                };

                foreach (var item in savePage.items)
                {
                    outPage.Items.Add(new InventoryItem()
                    {
                        ItemID = item.item.id,
                        Amount = item.item.amount,
                        Quality = item.item.quality,
                        Rot = item.rot,
                        State = item.item.state,
                        X = item.x,
                        Y = item.y
                    });
                }

                data.Content.Pages.Add(outPage);
            }
            data.Save();
            database.InsertUpdate(data, TableName);
            return true;
        }
    }
}