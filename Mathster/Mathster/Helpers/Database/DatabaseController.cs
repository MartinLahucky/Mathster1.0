using System;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace Mathster.Helpers.Model
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
                        CelkemPrikladuDobre = 0,
                        Nachozeno = 0,
                        DruhNejcastejiPocitanychPrikladu = String.Empty,
                    };

                    SettingsModel tabulkaNastaveni = new SettingsModel
                    {
                        ID = 1,
                        DarkMode = false,
                        BarvaPrimarni = Color.FromHex("#ffa927"),
                        BarvaPrimarniSvetla = Color.FromHex("#"),
                        BarvaSekundarni = Color.FromHex("#33a716"), 
                        BarvaSekundarniSvetla = Color.FromHex("#89e771"),
                        BarvaSpravne = Color.FromHex("#6ece25"),
                        BarvaSpatne = Color.FromHex("#ffb54c"),
                        BarvaTextu = Color.FromHex("#"),
                        BarvaTlacitek = Color.FromHex("#"),
                        BarvaInfo = Color.FromHex("#ead78e"),
                        BarvaPozadi = Color.FromHex("#"),
                        BarvaPozadiDarkMode = Color.FromHex("#"),
                        
                        
                    };
                    await database.InsertAsync(tabulkaModel);
                }
            });
            Task.WaitAll(task);
        }

        public async Task<DBModel> GetTable(int id = 0)
        {
            return await database.Table<DBModel>().Where(i => i.ID == id).FirstOrDefaultAsync();
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
    }
}