using System;
using System.Linq;
using System.Threading.Tasks;
using Mathster.Resources.Helpers;
using Mathster.Resources.Helpers.Notifications;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mathster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationsTestPage : ContentPage
    {
        public NotificationsTestPage()
        {
            InitializeComponent();
            MenuButton.IconImageSource = "menu_icon.png";
        }

        private async void MenuButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
            var existingPages = Navigation.NavigationStack.ToList();
            foreach (var page in existingPages) Navigation.RemovePage(page);
        }

        private void SendNotNow_OnClicked(object sender, EventArgs e)
        {
            DependencyService.Get<INotificationManager>().SendNotification(
                $"Požadavek na notifikaci: {DateTime.Now.TimeOfDay}",
                $"Notifikace by měla zaznít v: {DateTime.Now.TimeOfDay}");
        }

        private void SendNotDelay_OnClicked(object sender, EventArgs e)
        {
            int getTime = string.IsNullOrEmpty(SendNotDelayInput.Text) ? 0 : int.Parse(SendNotDelayInput.Text);
            var task = Task.Run(async () =>
            {
                DependencyService.Get<INotificationManager>().SendNotification(
                    $"Požadavek na notifikaci: {DateTime.Now.TimeOfDay}",
                    $"Notifikace by měla zaznít v: {DateTime.Now.AddSeconds(getTime).TimeOfDay}",
                    DateTime.Now.AddSeconds(getTime));
            });
            Task.WaitAll(task);
        }

        private void SendTaskDelay_OnClicked(object sender, EventArgs e)
        {
            int getTime = string.IsNullOrEmpty(SendTaskDelayInput.Text) ? 0 : int.Parse(SendTaskDelayInput.Text);
            DependencyService.Get<INotificationManager>().StartService(
                $"Požadavek na notifikaci: {DateTime.Now.TimeOfDay}",
                $"Notifikace by měla zaznít v: {DateTime.Now.AddSeconds(getTime).TimeOfDay}",
                DateTime.Now.AddSeconds(getTime));
            DependencyService.Get<IUtilities>().CloseApplication();
        }
    }
}