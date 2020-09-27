using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyTimeTable
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuItemPage : ContentPage
    {
        public MenuItemPage()
        {
            InitializeComponent();
        }

        //완료
        private async void OnAddTimetableButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddTimetableSchedulePage());
        }

        private async void OnDisplayCalendarButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CalendarPage());
        }
        private async void OnDisplayDailyScheduleButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DailySchedulePage());
        }


        private async void OnConfigurationButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ConfigurationPage());
        }
    }
}