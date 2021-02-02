using System;
using System.Linq;
using System.Threading.Tasks;
using Mathster.Resources.Database_Models;
using Mathster.Resources.Localization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mathster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();
            MenuButton.IconImageSource = "menu_icon.png";
            Title = Localization.Settings;
            NameEntry.Placeholder = Localization.EnterYourName;
            NameEntry.PlaceholderColor = Color.White;;
            NameLabel.Text = Localization.Name;
            DarkModeLabel.Text = Localization.DarkMode;
            
            Task task = Task.Run(async () =>
            {
                DBModel table = await App.Database.GetTable();

                if (table.Name == "")
                {
                    NameEntry.Text = String.Empty;
                }
                else
                {
                    NameEntry.Text = table.Name;
                }
            });
            Task.WaitAll(task);
            AboutAppLabel.Text = $"{Localization.AppVersion}: Closed Beta 1.3";
        }

        private async void MenuButton_OnClicked(object sender, EventArgs e)
        {
            DBModel table = await App.Database.GetTable();
            table.Name = NameEntry.Text;
            await App.Database.UpdateTable(table);
            await Navigation.PushAsync(new MainPage());
            var existingPages = Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                Navigation.RemovePage(page);
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            SettingsModel settings = await App.Database.GetSettings();
            BackgroundColor = Color.FromHex(settings.BackgroundHex);
            if (settings.DarkMode)
            {
                DarkModeSwitch.IsToggled = true;
                AboutAppLabel.TextColor = Color.White;
            }
        }
        
        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            DBModel table = await App.Database.GetTable();
            string newName = String.Empty;
            int nameLimit = 12;
            if (NameEntry.Text.Length <= nameLimit)
            {
                nameLimit = NameEntry.Text.Length;
            }

            for (int i = 0; i < nameLimit; i++)
            {
                newName += NameEntry.Text[i];
            }

            table.Name = newName;
            await App.Database.UpdateTable(table);
        }
        
        private async void DarkModeSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            SettingsModel settings = await App.Database.GetSettings();

            if (e.Value)
            {
                settings.BackgroundHex = "#262630";
                settings.DarkMode = true;
                AboutAppLabel.TextColor = Color.White;
            }
            else
            {
                settings.BackgroundHex = "#FAFAFA";
                settings.DarkMode = false;
                AboutAppLabel.TextColor = Color.Default;
            }

            BackgroundColor = Color.FromHex(settings.BackgroundHex);
            await App.Database.UpdateSettings(settings);
        }

        private void NameEntry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (NameEntry.Text.Length == 12)
            {
                NameEntry.Text = e.OldTextValue;
            }
        }
    }
}