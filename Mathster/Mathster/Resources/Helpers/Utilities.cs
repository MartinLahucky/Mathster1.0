using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mathster.Resources.Helpers
{
    public static class Utilities
    {
        //TODO Try 
        // await Navigation.PushAsync(new MainPage());
        // var existingPages = Navigation.NavigationStack.ToList();
        //     foreach (var page in existingPages)
        // {
        //     Navigation.RemovePage(page);
        // }
        
        public static string FormartNumber(float number, string variable)
        {
            if (number == 0)
            {
                return "";
            }

            if (number > 0)
            {
                return $" +{number}{variable}";
            }

            return $" {number}{variable}";
        }
        
        public static string FormartNumber(float number)
        {
            if (number == 0)
            {
                return "";
            }

            if (number > 0)
            {
                return $" +{number}";
            }

            return $" {number}";
        }
    }
}