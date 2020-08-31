﻿using System;
using Mathster.Helpers.Model;
using Mathster.Helpers.Resources;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mathster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : ContentPage
    {
        public Menu()
        {
            InitializeComponent();
            Device.StartTimer(new TimeSpan(0, 0, 0, 0, 50), () =>
            {
                Device.BeginInvokeOnMainThread (() =>
                {
                    Title = AppResource.Menu;
                });
                return false;
            });
            ScitaniButton.Text = AppResource.Scitani;
            OdcitaniButton.Text = AppResource.Odecitani;
            NasobeniButton.Text = AppResource.Nasobeni;
            DeleniButton.Text = AppResource.Deleni;
            NahodneButton.Text = AppResource.Nahodne;
        }

        private async void ScitaniButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VelikostCisel(1));
        }

        private async void OdcitaniButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VelikostCisel(2));
        }

        private async void NasobeniButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VelikostCisel(3));
        }

        private async void DeleniButton_OnClicked(object sender, EventArgs e)
        { 
            await Navigation.PushAsync(new VelikostCisel(4));
        }

        private async void NahodneButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VelikostCisel(5));
        }
    }
}