using EasyTimeTable.Constants;
using EasyTimeTable.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Markup;

namespace EasyTimeTable
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {

        private static bool currentDisplayIsNight = false;
        private static bool currentDisplayIsWeekend = false;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //필요한 테이블을 가져온다.
            //var schedules = await App.Database.GetAllSchedule();
            //await App.Database.DeleteAllSchedule();

            var monSchedules = await App.Database.GetAllScheduleByWeekdate(0);
            SetMondaySchedule(monSchedules);

            var tueSchedules = await App.Database.GetAllScheduleByWeekdate(1);


            var wedSchedules = await App.Database.GetAllScheduleByWeekdate(2);


            var thuSchedules = await App.Database.GetAllScheduleByWeekdate(3);


            var friSchedules = await App.Database.GetAllScheduleByWeekdate(4);


            var satSchedules = await App.Database.GetAllScheduleByWeekdate(5);


            var sunSchedules = await App.Database.GetAllScheduleByWeekdate(6);

           

        }


        private void SetMondaySchedule(List<ScheduleTimetable> Monschedules)
        {
            foreach (var item in Monschedules)
            {
                //주간 스케쥴이면
                if (!IsNight(item.StartHour))
                {
                    if (!IsNight(item.EndHour))
                    {
                        var span = item.EndHour - item.StartHour;
                        var displaySchedule = new StackLayout
                        {
                            BackgroundColor = Color.FromHex(item.SelectedColor),
                            StyleId = "MonSchedule1"
                        };
                        Grid.SetRow(displaySchedule, item.StartHour - 8);
                        Grid.SetColumn(displaySchedule, item.WeekDate + 1);
                        Grid.SetRowSpan(displaySchedule, span);
                        
                        var title = new Label
                        {
                            Text = item.ScheduleTitle,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            HorizontalOptions = LayoutOptions.CenterAndExpand
                        };

                        var box_top = new BoxView
                        {
                            BackgroundColor = Color.White,
                            HeightRequest=20
                        };

                        var box_bottom = new BoxView
                        {
                            BackgroundColor = Color.White,
                            HeightRequest=20
                        };

                        displaySchedule.Children.Add(box_top);
                        displaySchedule.Children.Add(title);
                        displaySchedule.Children.Add(box_bottom);

                        DayHourWeekdayTableBody.Children.Add(displaySchedule);
                    }

                    //마지막이 19시를 넘어가는 경우 19시 모두 색칠
                    else
                    {
                        //19시까지
                    }
                }



            }
            
        }

        private void SetTuesdaySchedule(List<ScheduleTimetable> Monschedules)
        {

        }
        private void SetWednesdaySchedule(List<ScheduleTimetable> Monschedules)
        {

        }
        private void SetThursdaySchedule(List<ScheduleTimetable> Monschedules)
        {

        }
        private void SetFridaySchedule(List<ScheduleTimetable> Monschedules)
        {

        }
        private void SetSaturdaySchedule(List<ScheduleTimetable> Monschedules)
        {

        }
        private void SetSundaySchedule(List<ScheduleTimetable> Monschedules)
        {

        }


        private bool IsNight(int hour)
        {
            if(hour < 20&& hour > 07)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        private async void OnAddButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MenuItemPage());
        }

        #region NightDayWeekendWeekday
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
        #endregion
    }
}

