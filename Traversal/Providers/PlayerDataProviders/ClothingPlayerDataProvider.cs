using Rocket.Core.Logging;
using SDG.Unturned;
using ShimmyMySherbet.MySQL.EF.Core;
using Traversal.Core;
using Traversal.Models;
using Traversal.Models.Databasing;
using Traversal.Models.Databasing.Scoped;

namespace Traversal.Providers.PlayerDataProviders
{
    public class ClothingPlayerDataProvider : IPlayerDataProvider<PlayerClothing>
    {
        public const string TableName = "TraversalData_Clothing";

        public void CheckSchema(MySQLEntityClient client)
        {
            if (!client.TableExists(TableName))
            {
                client.CreateTable<PlayerClothingData>(TableName);
            }
        }

        public bool Load(PlayerClothing instance, MySQLEntityClient database)
        {
            PlayerClothingData data = database.QuerySingle<PlayerClothingData>($"SELECT * FROM `{TableName}` WHERE PlayerID=@0 AND Slot=@1 AND ServerID=@2 AND Map=@3", instance.channel.GetPlayerID(), instance.channel.GetPlayerSlotID(), Traversal.ServerID, Provider.map);

            if (data == null)
            {
                return false;
            }
            SyncManager.AskSync(ref data);


            var thirdClothes = instance.thirdClothes;

            thirdClothes.visualShirt = instance.channel.owner.shirtItem;
            thirdClothes.visualPants = instance.channel.owner.pantsItem;
            thirdClothes.visualHat = instance.channel.owner.hatItem;
            thirdClothes.visualBackpack = instance.channel.owner.backpackItem;
            thirdClothes.visualVest = instance.channel.owner.vestItem;
            thirdClothes.visualMask = instance.channel.owner.maskItem;
            thirdClothes.visualGlasses = instance.channel.owner.glassesItem;

            thirdClothes.shirt = data.ShirtID;
            instance.shirtQuality = data.ShirtQuality;
            instance.shirtState = data.ShirtState;

            thirdClothes.pants = data.PantsID;
            instance.pantsQuality = data.PantsQuality;
            instance.pantsState = data.PantsState;

            thirdClothes.hat = data.HatID;
            instance.hatQuality = data.HatQuality;
            instance.hatState = data.HatState;

            thirdClothes.backpack = data.BackpackID;
            instance.backpackQuality = data.BackpackQuality;
            instance.backpackState = data.BackpackState;

            thirdClothes.vest = data.VestID;
            instance.vestQuality = data.VestQuality;
            instance.vestState = data.VestState;

            thirdClothes.mask = data.MaskID;
            instance.maskQuality = data.MaskQuality;
            instance.maskState = data.MaskState;

            thirdClothes.glasses = data.GlassesID;
            instance.glassesQuality = data.GlassesQuality;
            instance.glassesState = data.GlassesState;

            thirdClothes.isVisual = data.isVisible;

            instance.SetValue("isSkinned", data.isSkinned);

            thirdClothes.isMythic = data.isMythic;

            thirdClothes.apply();

            instance.SetValue("wasLoadCalled", true);

            return true;
        }

        public bool Save(PlayerClothing instance, MySQLEntityClient database)
        {
            var thirdClothes = instance.thirdClothes;
            PlayerClothingData data = new PlayerClothingData()
            {
                BackpackID = thirdClothes.backpack,
                GlassesID = thirdClothes.glasses,
                HatID = thirdClothes.hat,
                MaskID = thirdClothes.mask,
                PantsID = thirdClothes.pants,
                PlayerID = instance.channel.GetPlayerID(),
                ServerID = Traversal.ServerID,
                ShirtID = thirdClothes.shirt,
                VestID = thirdClothes.vest,
                BackpackQuality = instance.backpackQuality,
                BackpackState = instance.backpackState,
                GlassesQuality = instance.glassesQuality,
                GlassesState = instance.glassesState,
                HatQuality = instance.hatQuality,
                HatState = instance.hatState,
                isMythic = instance.isMythic,
                isSkinned = instance.isSkinned,
                isVisible = thirdClothes.isVisual,
                MaskQuality = instance.maskQuality,
                MaskState = instance.maskState,
                PantsQuality = instance.pantsQuality,
                PantsState = instance.pantsState,
                ShirtQuality = instance.shirtQuality,
                ShirtState = instance.shirtState,
                Slot = instance.channel.GetPlayerSlotID(),
                VestQuality = instance.vestQuality,
                VestState = instance.vestState,
                Map = Provider.map
            };
                database.InsertUpdate(data, TableName);
            SyncManager.AskSave(data);

            return true;
        }
    }
}