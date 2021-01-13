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
                    DBModel tabulkaModel = new DBModel
                    {
                        ID = 0,
                        Jmeno = String.Empty,
                        Experience = 0,
                        CelkemPrikladu = 0,
                        CelkemScitani = 0,
                        CelkemScitaniSpravne = 0,
                        CelkemOdcitani = 0,
                        CelkemOdcitaniSpravne = 0,
                        CelkemNasobeni = 0,
                        CelkemNasobeniSpravne = 0,
                        CelkemDeleni = 0,
                        CelkemDeleniSpravne = 0,
                        CelkemPrikladuSpravne = 0,
                    };
                    await database.InsertAsync(tabulkaModel);
                }
            });
            Task.WaitAll(task);

            database.CreateTableAsync<SettingsModel>().Wait();
            Task task1 = Task.Run(async () =>
            {
                if (await database.Table<SettingsModel>().CountAsync() == 0)
                {
                    SettingsModel tabulkaNastaveni = new SettingsModel
                    {
                        ID = 0,
                        DarkMode = false,
                        BackgroundHex = "#FAFAFA"
                    };
                    await database.InsertAsync(tabulkaNastaveni);
                }
            });
            Task.WaitAll(task1);
        }

        public async Task<DBModel> GetTable(int id = 0)
        {
            return await database.Table<DBModel>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<SettingsModel> GetSettings(int id = 0)
        {
            return await database.Table<SettingsModel>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> UpdateTable(DBModel zapis)
        {
            if (zapis.ID == 0)
            {
                return database.UpdateAsync(zapis);
            }
            else
            {
                return database.InsertAsync(zapis);
            }
        }

        public Task<int> UpdateSettings(SettingsModel zapis)
        {
            return database.UpdateAsync(zapis);
        }
    }
}