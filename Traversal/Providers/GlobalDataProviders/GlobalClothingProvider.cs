using ShimmyMySherbet.MySQL.EF.Core;
using Traversal.Models;
using Traversal.Models.Databasing;
using Traversal.Models.Databasing.Scoped;

namespace Traversal.Providers.GlobalDataProviders
{
    public class GlobalClothingProvider : IGlobalDataProvider<PlayerClothingData>
    {
        public const string TableName = "PlayerData_Clothing_Global";

        public string Name => "Clothing";

        public void CheckSchema(MySQLEntityClient database)
        {
            if (!database.TableExists(TableName))
            {
                database.CreateTable<GlobalClothingProvider>(TableName);
            }
        }

        public bool Load(ref PlayerClothingData data, MySQLEntityClient database, Scope scope)
        {
            GlobalClothingData global = database.QuerySingle<GlobalClothingData>($"SELECT * FROM {TableName} WHERE PlayerID=@0 AND Slot=@1", data.PlayerID, data.Slot);
            if (global == null) return false;

            if (scope["Clothing.Meta"])
            {
                data.isVisible = global.isVisible;
                data.isSkinned = global.isSkinned;
                data.isMythic = global.isMythic;
            }
            if (scope["Clothing.Shirt"])
            {
                data.ShirtID = global.ShirtID;
                data.ShirtQuality = global.ShirtQuality;
                data.ShirtState = global.ShirtState;
            }
            if (scope["Clothing.Pants"])
            {
                data.PantsID = global.PantsID;
                data.PantsQuality = global.PantsQuality;
                data.PantsState = global.PantsState;
            }

            if (scope["Clothing.Hat"])
            {
                data.HatID = global.HatID;
                data.HatQuality = global.HatQuality;
                data.HatState = global.HatState;
            }
            if (scope["Clothing.Backpack"])
            {
                data.BackpackID = global.BackpackID;
                data.BackpackQuality = global.BackpackQuality;
                data.BackpackState = global.BackpackState;
            }
            if (scope["Clothing.Vest"])
            {
                data.VestID = global.VestID;
                data.VestQuality = global.VestQuality;
                data.VestState = global.VestState;
            }
            if (scope["Clothing.Mask"])
            {
                data.MaskID = global.MaskID;
                data.MaskQuality = global.MaskQuality;
                data.MaskState = global.MaskState;
            }
            if (scope["Clothing.Glasses"])
            {
                data.GlassesID = global.GlassesID;
                data.GlassesQuality = global.GlassesQuality;
                data.GlassesState = global.GlassesState;
            }

            return true;
        }

        public bool Save(PlayerClothingData data, MySQLEntityClient database, Scope scope)
        {
            GlobalClothingData global = database.QuerySingle<GlobalClothingData>($"SELECT * FROM {TableName} WHERE PlayerID=@0 AND Slot=@1", data.PlayerID, data.Slot);
            if (global == null)
            {
                global = new GlobalClothingData()
                {
                    Slot = data.Slot,
                    PlayerID = data.PlayerID
                };
            }

            if (scope["Clothing.Meta"])
            {
                global.isVisible = data.isVisible;
                global.isSkinned = data.isSkinned;
                global.isMythic = data.isMythic;
            }
            if (scope["Clothing.Shirt"])
            {
                global.ShirtID = data.ShirtID;
                global.ShirtQuality = data.ShirtQuality;
                global.ShirtState = data.ShirtState;
            }
            if (scope["Clothing.Pants"])
            {
                global.PantsID = data.PantsID;
                global.PantsQuality = data.PantsQuality;
                global.PantsState = data.PantsState;
            }

            if (scope["Clothing.Hat"])
            {
                global.HatID = data.HatID;
                global.HatQuality = data.HatQuality;
                global.HatState = data.HatState;
            }
            if (scope["Clothing.Backpack"])
            {
                global.BackpackID = data.BackpackID;
                global.BackpackQuality = data.BackpackQuality;
                global.BackpackState = data.BackpackState;
            }
            if (scope["Clothing.Vest"])
            {
                global.VestID = data.VestID;
                global.VestQuality = data.VestQuality;
                global.VestState = data.VestState;
            }
            if (scope["Clothing.Mask"])
            {
                global.MaskID = data.MaskID;
                global.MaskQuality = data.MaskQuality;
                global.MaskState = data.MaskState;
            }
            if (scope["Clothing.Glasses"])
            {
                global.GlassesID = data.GlassesID;
                global.GlassesQuality = data.GlassesQuality;
                global.GlassesState = data.GlassesState;
            }

            database.InsertUpdate(global, TableName);
            return true;
        }
    }
}