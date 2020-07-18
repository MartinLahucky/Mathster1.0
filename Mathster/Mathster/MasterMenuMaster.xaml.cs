using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Mathster.Helpers.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mathster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterMenuMaster : ContentPage
    {
        public ListView ListView;

        public MasterMenuMaster()
        {
            InitializeComponent();

            BindingContext = new MasterMenuMasterViewModel();
            ListView = MenuItemsListView;
        }

        class MasterMenuMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MasterMenuMasterMenuItem> MenuItems { get; set; }

            public MasterMenuMasterViewModel()
            {
                MenuItems = new ObservableCollection<MasterMenuMasterMenuItem>(new[]
                {
                    new MasterMenuMasterMenuItem { Id = 0, Title = AppResource.Menu, TargetType = typeof(Menu)},
                   // new MasterMenuMasterMenuItem { Id = 1, Title = AppResource.Statistiky, TargetType = typeof(Menu)},
                   // new MasterMenuMasterMenuItem { Id = 2, Title = AppResource.Nastaveni, TargetType = typeof(Menu)},
                    new MasterMenuMasterMenuItem { Id = 3, Title = AppResource.OAplikaci, TargetType = typeof(Menu)},
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}