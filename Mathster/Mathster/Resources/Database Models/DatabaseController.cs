using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace Mathster.Resources.Database_Models
{
    public class DatabaseController
    {
        private readonly SQLiteAsyncConnection database;

        public DatabaseController()
        {
            database = new SQLiteAsyncConnection(App.DatabaseLocation);
            database.CreateTableAsync<DBModel>().Wait();
            var task = Task.Run(async () =>
            {
                if (await database.Table<DBModel>().CountAsync() == 0)
                {
                    var table = new DBModel
                    {
                        Name = string.Empty
                    };
                    table.ResetDb();
                    await database.InsertAsync(table);
                }
            });
            Task.WaitAll(task);

            database.CreateTableAsync<SettingsModel>().Wait();
            var task1 = Task.Run(async () =>
            {
                if (await database.Table<SettingsModel>().CountAsync() == 0)
                {
                    var settings = new SettingsModel
                    {
                        Theme = OSAppTheme.Light
                    };
                    await database.InsertAsync(settings);
                }
            });
            Task.WaitAll(task1);

            database.CreateTableAsync<ObjectsModel>().Wait();
            var task2 = Task.Run(async () =>
            {
                if (await database.Table<ObjectsModel>().CountAsync() == 0)
                {
                    var objects = new ObjectsModel();
                    await database.InsertAsync(objects);
                }
            });
            Task.WaitAll(task2);
        }

        public async Task<DBModel> GetTable()
        {
            return await database.Table<DBModel>().FirstOrDefaultAsync();
        }

        public async Task<SettingsModel> GetSettings()
        {
            return await database.Table<SettingsModel>().FirstOrDefaultAsync();
        }

        public async Task<ObjectsModel> GetObjects()
        {
            return await database.Table<ObjectsModel>().FirstOrDefaultAsync();
        }

        public Task<int> UpdateTable(DBModel table)
        {
            return database.UpdateAsync(table);
        }

        public Task<int> UpdateSettings(SettingsModel table)
        {
            return database.UpdateAsync(table);
        }
    }
}