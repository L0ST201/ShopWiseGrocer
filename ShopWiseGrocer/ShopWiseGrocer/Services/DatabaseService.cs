using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopWiseGrocer.Models;

namespace ShopWiseGrocer.Services
{
    public class DatabaseService
    {
        readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<GroceryItem>().Wait();
        }

        public Task<List<GroceryItem>> GetItemsAsync()
        {
            return _database.Table<GroceryItem>().ToListAsync();
        }

        public Task<int> SaveItemAsync(GroceryItem item)
        {
            return _database.InsertAsync(item);
        }

        public Task<int> DeleteItemAsync(GroceryItem item)
        {
            return _database.DeleteAsync(item);
        }
    }
}
