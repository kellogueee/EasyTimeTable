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

namespace EasyTimeTable.Model
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddScheduleModelMaster : ContentPage
    {
        public ListView ListView;

        public AddScheduleModelMaster()
        {
            InitializeComponent();

            BindingContext = new AddScheduleModelMasterViewModel();
            ListView = MenuItemsListView;
        }

        class AddScheduleModelMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<AddScheduleModelMasterMenuItem> MenuItems { get; set; }

            public AddScheduleModelMasterViewModel()
            {
                MenuItems = new ObservableCollection<AddScheduleModelMasterMenuItem>(new[]
                {
                    new AddScheduleModelMasterMenuItem { Id = 0, Title = "Page 1" },
                    new AddScheduleModelMasterMenuItem { Id = 1, Title = "Page 2" },
                    new AddScheduleModelMasterMenuItem { Id = 2, Title = "Page 3" },
                    new AddScheduleModelMasterMenuItem { Id = 3, Title = "Page 4" },
                    new AddScheduleModelMasterMenuItem { Id = 4, Title = "Page 5" },
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