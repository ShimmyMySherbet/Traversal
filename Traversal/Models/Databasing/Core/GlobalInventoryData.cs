using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShimmyMySherbet.MySQL.EF.Models;
using Newtonsoft.Json;

namespace Traversal.Models.Databasing
{
    public class GlobalInventoryData : GlobalDataModel
    {
        [SQLIgnore]
        public InventoryContent Content;

        [SQLPropertyName("Content")]
        public string ContentValue;

        public void Load() => Content = JsonConvert.DeserializeObject<InventoryContent>(ContentValue);
        public void Save() => ContentValue = JsonConvert.SerializeObject(Content);
    }
}
