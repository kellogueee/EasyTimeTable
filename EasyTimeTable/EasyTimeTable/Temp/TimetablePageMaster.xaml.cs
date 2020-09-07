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

namespace EasyTimeTable
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimetablePageMaster : ContentPage
    {
        public ListView ListView;

        public TimetablePageMaster()
        {
            InitializeComponent();

            BindingContext = new TimetablePageMasterViewModel();
            ListView = MenuItemsListView;
        }

        //private async void OnMenuItemSelected(object sender, SelectionChangedEventArgs e)
        //{
        //    var currentItem = (TimetablePageMasterMenuItem)(e.CurrentSelection.FirstOrDefault());

        //    switch (currentItem.Id)
        //    {
        //        case 0:
        //            await Navigation.PushAsync(new AddTimetableSchedulePage());
        //            break;
        //        case 1:
        //            await Navigation.PushAsync(new DisplayCalendarPage());
        //            break;
        //        case 2:
        //            await Navigation.PushAsync(new ConfigurationPage());
        //            break;
        //    }
        //    MenuItemsListView.SelectedItem = null;
        //}



        class TimetablePageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<TimetablePageMasterMenuItem> MenuItems { get; set; }

            public TimetablePageMasterViewModel()
            {
                MenuItems = new ObservableCollection<TimetablePageMasterMenuItem>(new[]
                {
                    new TimetablePageMasterMenuItem { Id = 0, Title = "시간표추가", TargetType=typeof(AddTimetableSchedulePage) },
                    new TimetablePageMasterMenuItem { Id = 1, Title = "달력보기",TargetType=typeof(DisplayCalendarPage) },
                    new TimetablePageMasterMenuItem { Id = 2, Title = "설정",TargetType=typeof(ConfigurationPage) },
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