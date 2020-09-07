using EasyTimeTable.Constants;
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
    public partial class TimetablePageDetail : ContentPage
    {
        private static bool currentDisplayIsNight = false;
        private static bool currentDisplayIsWeekend = false;


        public TimetablePageDetail()
        {
            InitializeComponent();
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //필요한 테이블을 가져온다.
            var schedules = await App.Database.GetAllSchedule();


        }


        private void OnNightDayChangeClicked(object sender, EventArgs e)
        {
            DayHourWeekdayTableBody.IsVisible = false;
            NightHourWeekdayTableBody.IsVisible = false;
            DayHourWeekendTableBody.IsVisible = false;
            NightHourWeekendTableBody.IsVisible = false;
            
            //현재 페이지: 주간 주일 
            if (!currentDisplayIsNight && !currentDisplayIsWeekend)
            {
                currentDisplayIsNight = true;
                NightDay.Text = "주간";
                NightHourWeekdayTableBody.IsVisible = true;
            }

            //주간 주말
            else if (!currentDisplayIsNight && currentDisplayIsWeekend)
            {
                currentDisplayIsNight = true;
                NightDay.Text = "주간";
                NightHourWeekendTableBody.IsVisible = true;
            }

            //야간 주일
            else if (currentDisplayIsNight && !currentDisplayIsWeekend)
            {
                currentDisplayIsNight = false;
                NightDay.Text = "야간";
                DayHourWeekdayTableBody.IsVisible = true;
            }

            //야간 주말
            else if (currentDisplayIsNight && currentDisplayIsWeekend)
            {
                currentDisplayIsNight = false;
                NightDay.Text = "야간";
                DayHourWeekendTableBody.IsVisible = true;
            }


        }

        private void OnWeekendDayChangeClicked(object sender, EventArgs e)
        {

            DayHourWeekdayTableBody.IsVisible = false;
            NightHourWeekdayTableBody.IsVisible = false;
            DayHourWeekendTableBody.IsVisible = false;
            NightHourWeekendTableBody.IsVisible = false;
            Weekday.IsVisible = false;
            Weekend.IsVisible = false;
            //현재 페이지: 주간 주일 
            if (!currentDisplayIsNight && !currentDisplayIsWeekend)
            {
                currentDisplayIsWeekend = true;
                WeekendDay.Text = "주일";
                DayHourWeekendTableBody.IsVisible = true;
                Weekend.IsVisible = true;
            }

            //주간 주말
            else if (!currentDisplayIsNight && currentDisplayIsWeekend)
            {
                currentDisplayIsWeekend = false;
                WeekendDay.Text = "주말";
                DayHourWeekdayTableBody.IsVisible = true;
                Weekday.IsVisible = true;
            }

            //야간 주일
            else if (currentDisplayIsNight && !currentDisplayIsWeekend)
            {
                currentDisplayIsWeekend = true;
                WeekendDay.Text = "주일";
                NightHourWeekendTableBody.IsVisible = true;
                Weekend.IsVisible = true;
            }

            //야간 주말
            else if (currentDisplayIsNight && currentDisplayIsWeekend)
            {
                currentDisplayIsWeekend = false;
                WeekendDay.Text = "주말";
                NightHourWeekdayTableBody.IsVisible = true;
                Weekday.IsVisible = true;
            }

        }
    }
}