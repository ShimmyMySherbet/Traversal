using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShimmyMySherbet.MySQL.EF.Core;
using Traversal.Models;
using Traversal.Models.Databasing.Scoped;

namespace Traversal.Providers.GlobalDataProviders
{
    public class GlobalQuestProvider : IGlobalDataProvider<PlayerQuestData>
    {
        public const string TableName = "TraversalData_Quests_Global";

        public string Name => "Quests";

        public void CheckSchema(MySQLEntityClient database)
        {
            throw new NotImplementedException();
        }

        public bool Load(ref PlayerQuestData data, MySQLEntityClient database, Scope scope)
        {
            throw new NotImplementedException();
        }

        public bool Save(PlayerQuestData data, MySQLEntityClient database, Scope scope)
        {
            throw new NotImplementedException();
        }
    }
}
