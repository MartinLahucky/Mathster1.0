using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mathster
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Device.StartTimer(new TimeSpan(0, 0, 0, 2, 50), () =>
            {
                Device.BeginInvokeOnMainThread (() =>
                {
                    Navigation.PushAsync(new Menu());
                    var existingPages = Navigation.NavigationStack.ToList();
                    foreach(var page in existingPages)
                    {
                        Navigation.RemovePage(page);
                    }
                });
                return false;
            });
        }
    }
}