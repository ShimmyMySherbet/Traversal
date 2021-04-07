using Newtonsoft.Json;
using ShimmyMySherbet.MySQL.EF.Models;

namespace Traversal.Models.Databasing
{
    public class SkillData : PlayerDataModel
    {
        public uint Experience;
        public int Reputation;
        public byte Boost;

        [SQLIgnore]
        public InnerSkillData Data;

        [SQLPropertyName("Data")]
        public string DataValue;

        public void Load() => Data = JsonConvert.DeserializeObject<InnerSkillData>(DataValue);

        public void Save() => DataValue = JsonConvert.SerializeObject(Data);
    }
}