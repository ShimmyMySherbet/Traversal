using Newtonsoft.Json;
using ShimmyMySherbet.MySQL.EF.Models;

namespace Traversal.Models.Databasing
{
    public class GlobalInventoryData : GlobalDataModel
    {
        [SQLIgnore]
        public InventoryContent Content;

        [SQLPropertyName("Content")]
        public string ContentValue;

        public void Load()
        {
            if (ContentValue != null)
            {
                Content = JsonConvert.DeserializeObject<InventoryContent>(ContentValue);
            }
            else
            {
                Content = new InventoryContent();
            }
        }

        public void Save()
        {
            if (Content != null)
                ContentValue = JsonConvert.SerializeObject(Content);
        }
    }
}