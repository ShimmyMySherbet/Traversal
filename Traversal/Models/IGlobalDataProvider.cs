using System.Text.RegularExpressions;
using ShimmyMySherbet.MySQL.EF.Core;

namespace Traversal.Models
{
    public interface IGlobalDataProvider<T>
    {
        string Name { get; }

        void CheckSchema(MySQLEntityClient database);

        bool Save(T data, MySQLEntityClient database, Scope scope);

        bool Load(ref T data, MySQLEntityClient database, Scope scope);
    }
}