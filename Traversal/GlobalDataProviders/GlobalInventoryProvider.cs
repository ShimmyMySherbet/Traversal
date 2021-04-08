using ShimmyMySherbet.MySQL.EF.Core;
using Traversal.Models;
using Traversal.Models.Databasing;
using Traversal.Models.Databasing.Scoped;

namespace Traversal.GlobalDataProviders
{
    public class GlobalInventoryProvider : IGlobalDataProvider<PlayerInventoryData>
    {
        public const string TableName = "PlayerData_Inventory_Global";

        public string Name => "Inventory";

        public void CheckSchema(MySQLEntityClient database)
        {
            if (!database.TableExists(TableName))
            {
                database.CreateTable<GlobalInventoryData>(TableName);
            }
        }

        public bool Load(ref PlayerInventoryData data, MySQLEntityClient database, Scope scope)
        {
            GlobalInventoryData global = database.QuerySingle<GlobalInventoryData>($"SELECT * FROM {TableName} WHERE PlayerID=@0 AND Slot=@1", data.PlayerID, data.Slot);

            if (global == null) return false;
            global.Load();

            if (scope["Inventory.Content"])
            {
                data.Content = global.Content;
            }
            else
            {
                if (scope["Inventory.Primary"])
                {
                    data.Content.Pages[0] = global.Content.Pages[0];
                }

                if (scope["Inventory.Secondary"])
                {
                    data.Content.Pages[1] = global.Content.Pages[1];
                }

                if (scope["Inventory.Hands"])
                {
                    data.Content.Pages[2] = global.Content.Pages[2];
                }
            }

            return true;
        }

        public bool Save(PlayerInventoryData data, MySQLEntityClient database, Scope scope)
        {
            GlobalInventoryData global = database.QuerySingle<GlobalInventoryData>($"SELECT * FROM {TableName} WHERE PlayerID=@0 AND Slot=@1", data.PlayerID, data.Slot);
            if (global == null)
            {
                global = new GlobalInventoryData()
                {
                    PlayerID = data.PlayerID,
                    Slot = data.Slot
                };
            }
            global.Load();
            if (scope["Inventory.Content"])
            {
                global.Content = data.Content;
            }
            else
            {
                if (scope["Inventory.Primary"])
                {
                    global.Content.Pages[0] = data.Content.Pages[0];
                }

                if (scope["Inventory.Secondary"])
                {
                    global.Content.Pages[1] = data.Content.Pages[1];
                }

                if (scope["Inventory.Hands"])
                {
                    global.Content.Pages[2] = data.Content.Pages[2];
                }
            }

            global.Save();

            database.InsertUpdate(data, TableName);

            return true;
        }
    }
}