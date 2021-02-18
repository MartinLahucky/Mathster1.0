using System.Threading.Tasks;
using SQLite;

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
                        Name = string.Empty,
                        Experience = 0,
                        TotalExercises = 0,
                        TotalAdd = 0,
                        TotalAddCorrect = 0,
                        TotalSub = 0,
                        TotalSubCorrect = 0,
                        TotalMul = 0,
                        TotalMulCorrect = 0,
                        TotalDiv = 0,
                        TotalDivCorrect = 0,
                        TotalExercisesCorrect = 0,
                        TotalLinear = 0,
                        TotalLinearCorrect = 0,
                        TotalQuadratic = 0,
                        TotalQuadraticCorrect = 0,
                        TotalSquare = 0,
                        TotalSquareCorrect = 0
                    };
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
                        DarkMode = false,
                        BackgroundHex = "#FAFAFA"
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