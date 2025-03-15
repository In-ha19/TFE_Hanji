using SQLite;

using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using MonApplicationMobile.Models;

namespace MonApplicationMobile.Data
{
    public static class DatabaseHelper
    {
        private static SQLiteAsyncConnection _database;

        public static async Task<SQLiteAsyncConnection> GetDatabaseConnection()
        {
            if (_database == null)
            {
                _database = new SQLiteAsyncConnection(App.DatabasePath);
                await _database.CreateTableAsync<Category>();
                await _database.CreateTableAsync<Article>();
            }
            return _database;
        }
    }
}
