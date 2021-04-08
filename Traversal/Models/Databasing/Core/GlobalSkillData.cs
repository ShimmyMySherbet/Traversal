using Newtonsoft.Json;
using ShimmyMySherbet.MySQL.EF.Models;

namespace Traversal.Models.Databasing
{
    public class GlobalSkillData : GlobalDataModel
    {
        public uint Experience;
        public int Reputation;
        public byte Boost;

        [SQLIgnore]
        public InnerSkillData Data;

        [SQLPropertyName("Data")]
        public string DataValue;

        public void Load()
        {
            if (DataValue != null)
            {
                Data = JsonConvert.DeserializeObject<InnerSkillData>(DataValue);
            } else
            {
                Data = new InnerSkillData();
            }
        }

        public void Save()
        {
            if (Data != null)
            {
                DataValue = JsonConvert.SerializeObject(Data);
            }
        }
    }
}