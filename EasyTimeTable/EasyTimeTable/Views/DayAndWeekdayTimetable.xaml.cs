using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyTimeTable.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DayAndWeekdayTimetable : ContentPage
    {
        public DayAndWeekdayTimetable()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if(Navigation.NavigationStack[0] is FirstInformationCarouselPage)
            {
                Navigation.RemovePage(Navigation.NavigationStack[0]);
            }    
        }
    }
}