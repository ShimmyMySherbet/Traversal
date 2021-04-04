using ShimmyMySherbet.MySQL.EF.Core;

namespace Traversal.Models
{
    public interface IPlayerDataProvider<T>
    {
        void CheckSchema(MySQLEntityClient client);

        /// <returns>If load was successful</returns>
        bool Load(T instance, MySQLEntityClient database);

        /// <returns>If save was successful</returns>
        bool Save(T instance, MySQLEntityClient database);
    }
}