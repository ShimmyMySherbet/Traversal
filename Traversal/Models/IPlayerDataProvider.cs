using ShimmyMySherbet.MySQL.EF.Core;

namespace Traversal.Models
{
    public interface IPlayerDataProvider<T>
    {
        /// <returns>If load was successful</returns>
        bool Load(T instance, MySQLEntityClient database);

        /// <returns>If save was successful</returns>
        bool Save(T instance, MySQLEntityClient database);
    }
}