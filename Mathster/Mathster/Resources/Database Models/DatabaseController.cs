using System;
using System.Threading.Tasks;
using SQLite;

namespace Mathster.Resources.Database_Models
{
    public class DatabaseController
    {
        readonly SQLiteAsyncConnection database;

        public DatabaseController()
        {
            database = new SQLiteAsyncConnection(App.DatabaseLocation);
            database.CreateTableAsync<DBModel>().Wait();
            Task task = Task.Run(async () =>
            {
                if (await database.Table<DBModel>().CountAsync() == 0)
                {
                    DBModel table = new DBModel
                    {
                        Name = String.Empty,
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
                        TotalSquareCorrect = 0,
                    };
                    await database.InsertAsync(table);
                }
            });
            Task.WaitAll(task);

            database.CreateTableAsync<SettingsModel>().Wait();
            Task task1 = Task.Run(async () =>
            {
                if (await database.Table<SettingsModel>().CountAsync() == 0)
                {
                    SettingsModel settings = new SettingsModel
                    {
                        DarkMode = false,
                        BackgroundHex = "#FAFAFA"
                    };
                    await database.InsertAsync(settings);
                }
            });
            Task.WaitAll(task1);
        }

        public async Task<DBModel> GetTable()
        {
            return await database.Table<DBModel>().FirstOrDefaultAsync();
        }

        public async Task<SettingsModel> GetSettings()
        {
            return await database.Table<SettingsModel>().FirstOrDefaultAsync();
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