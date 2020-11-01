using EasyTimeTable.Resx;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyTimeTable.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EasyTimetablePageMaster : ContentPage
    {
        public ListView ListView;

        public EasyTimetablePageMaster()
        {
            InitializeComponent();

            BindingContext = new EasyTimetablePageMasterViewModel();
            ListView = MenuItemsListView;
        }



        class EasyTimetablePageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<EasyTimetablePageMasterMenuItem> MenuItems { get; set; }

            public EasyTimetablePageMasterViewModel()
            {
                MenuItems = new ObservableCollection<EasyTimetablePageMasterMenuItem>(new[]
                {
                    new EasyTimetablePageMasterMenuItem { Id = 0, Title = AppResources.Home,TargetType=typeof(EasyTimetableMainPage) },
                    new EasyTimetablePageMasterMenuItem { Id = 1, Title = AppResources.AddSchedule,TargetType=typeof(AddShcedulePage) },
                    new EasyTimetablePageMasterMenuItem { Id = 2, Title = AppResources.Configuration,TargetType=typeof(ConfigurationPage) }
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