using EasyTimeTable.Constants;
using EasyTimeTable.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
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
        
        private static readonly int[] dayHours = new int[] { 8,9,10,11,12,13,14,15,16,17,18,19 };
        private static readonly int[] nightHours = new int[] { 20,21,22,23,0,1,2,3,4,5,6,7};
        

        private readonly IDatabase<ScheduleTimetable> _database;

        public MainPage()
        {
            InitializeComponent();
            _database = new DatabaseService().SQLiteDatabase;

            var DHWDTB = DayHourWeekdayTableBody.Children;
            var DHWETB = DayHourWeekendTableBody.Children;
            var NHWDTB = NightHourWeekdayTableBody.Children;
            var NHWETB = NightHourWeekendTableBody.Children;

            foreach (var gridBox in DHWDTB)
            {
                var col = Grid.GetColumn(gridBox);
                if (col == 0)
                {
                    continue;
                }
                var row = Grid.GetRow(gridBox);

                var gridBoxTapGestureRecognizer = new TapGestureRecognizer();
                gridBoxTapGestureRecognizer.Tapped += async (s, e) =>
                {
                    int date = col - 1;
                    await Navigation.PushModalAsync(new AddTimetableSchedulePage(dayHours[row], date));
                };
                gridBox.GestureRecognizers.Add(gridBoxTapGestureRecognizer);
            }

            foreach (var gridBox in DHWETB)
            {
                var col = Grid.GetColumn(gridBox);
                if (col == 0)
                {
                    continue;
                }
                var row = Grid.GetRow(gridBox);

                var gridBoxTapGestureRecognizer = new TapGestureRecognizer();
                gridBoxTapGestureRecognizer.Tapped += async (s, e) =>
                {
                    int date = col +4;
                    await Navigation.PushModalAsync(new AddTimetableSchedulePage(dayHours[row], date));
                };
                gridBox.GestureRecognizers.Add(gridBoxTapGestureRecognizer);
            }

            foreach (var gridBox in NHWDTB)
            {
                var col = Grid.GetColumn(gridBox);
                if (col == 0)
                {
                    continue;
                }
                var row = Grid.GetRow(gridBox);

                var gridBoxTapGestureRecognizer = new TapGestureRecognizer();
                gridBoxTapGestureRecognizer.Tapped += async (s, e) =>
                {
                    int date = col - 1;
                    await Navigation.PushModalAsync(new AddTimetableSchedulePage(nightHours[row], date));
                };
                gridBox.GestureRecognizers.Add(gridBoxTapGestureRecognizer);
            }

            foreach (var gridBox in NHWETB)
            {
                var col = Grid.GetColumn(gridBox);
                if (col == 0)
                {
                    continue;
                }
                var row = Grid.GetRow(gridBox);

                var gridBoxTapGestureRecognizer = new TapGestureRecognizer();
                gridBoxTapGestureRecognizer.Tapped += async (s, e) =>
                {
                    int date = col + 4;
                    await Navigation.PushModalAsync(new AddTimetableSchedulePage(nightHours[row], date));
                };
                gridBox.GestureRecognizers.Add(gridBoxTapGestureRecognizer);
            }

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var monSchedules = await _database.GetAllScheduleByWeekdate(0);
            var tueSchedules = await _database.GetAllScheduleByWeekdate(1);
            var wedSchedules = await _database.GetAllScheduleByWeekdate(2);
            var thuSchedules = await _database.GetAllScheduleByWeekdate(3);
            var friSchedules = await _database.GetAllScheduleByWeekdate(4);
            var satSchedules = await _database.GetAllScheduleByWeekdate(5);
            var sunSchedules = await _database.GetAllScheduleByWeekdate(6);

            List<ScheduleTimetable>[] WeekdaySchedules = new List<ScheduleTimetable>[]
            {
                monSchedules,tueSchedules,wedSchedules,thuSchedules,friSchedules,satSchedules,sunSchedules
            };
            List<ScheduleTimetable>[] WeekendSchedules = new List<ScheduleTimetable>[]
            {
                satSchedules,sunSchedules
            };


            foreach (var schedules in WeekdaySchedules)
            {
                SetWeekdaySchedule(schedules);
            }


            foreach (var schedules in WeekendSchedules)
            {

                SetWeekendSchedule(schedules);
            }

        }

        private void SetWeekdaySchedule(List<ScheduleTimetable> schedules)
        {
            foreach (var schedule in schedules)
            {
                //시작시간이 주간이다.
                if (!IsNight(schedule.StartHour))
                {
                    DayHourWeekdayTableBody.Children.Add(SetScheduleStack_day(schedule));
                }

                else
                {
                    NightHourWeekdayTableBody.Children.Add(SetScheduleStack_night(schedule));
                }
            }
        }

        private void SetWeekendSchedule(List<ScheduleTimetable> schedules)
        {
            foreach (var schedule in schedules)
            {
                //시작시간이 주간이다.
                if (!IsNight(schedule.StartHour))
                {
                    DayHourWeekendTableBody.Children.Add(SetScheduleStack_day(schedule));
                }

                else
                {
                    NightHourWeekendTableBody.Children.Add(SetScheduleStack_night(schedule));
                }
            }
        }


        private StackLayout SetScheduleStack_day(ScheduleTimetable schedule)
        {
            //마감시간이 저녁시간이라면
            if (IsNight(schedule.EndHour))
            {
                schedule.EndHour = 19;
            }

            var startHourRowIDX = dayHours.IndexOf(schedule.StartHour);
            var endHourRowIDX = dayHours.IndexOf(schedule.EndHour);
            int scheduleColIDX = 0;

            //주말이면
            if (IsWeekend(schedule.WeekDate))
            {
                scheduleColIDX= schedule.WeekDate -4;
            }
            //평일이면
            else
            {
                scheduleColIDX = schedule.WeekDate + 1;
            }
            var span = endHourRowIDX - startHourRowIDX;

            var displaySchedule = new StackLayout
            {
                BackgroundColor = Color.FromHex(schedule.SelectedColor),
                StyleId = schedule.ID.ToString()
            };

            displaySchedule.Children.Add(SetBlankBox_TopinScheduleStack(schedule.StartMinute));
            displaySchedule.Children.Add(SetTitleinScheduleStack(schedule.ScheduleTitle));
            displaySchedule.Children.Add(SetBlackBox_BottominScheduleStack(schedule.EndMinute));

            Grid.SetRow(displaySchedule, startHourRowIDX);
            Grid.SetColumn(displaySchedule, scheduleColIDX);
            Grid.SetRowSpan(displaySchedule, span);

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (s, e) =>
            {
                await Navigation.PushModalAsync(new AddTimetableSchedulePage(schedule));
            };

            displaySchedule.GestureRecognizers.Add(tapGestureRecognizer);

            return displaySchedule;
        }

        private StackLayout SetScheduleStack_night(ScheduleTimetable schedule)
        {
            //마감시간이 저녁시간이라면
            if (IsNight(schedule.EndHour))
            {
                schedule.EndHour = 19;
            }

            var startHourRowIDX = nightHours.IndexOf(schedule.StartHour);
            var endHourRowIDX = nightHours.IndexOf(schedule.EndHour);
            int scheduleColIDX = 0;

            //주말이면
            if (IsWeekend(schedule.WeekDate))
            {
                scheduleColIDX = schedule.WeekDate - 4;
            }
            //평일이면
            else
            {
                scheduleColIDX = schedule.WeekDate + 1;
            }
            var span = endHourRowIDX - startHourRowIDX;

            var displaySchedule = new StackLayout
            {
                BackgroundColor = Color.FromHex(schedule.SelectedColor),
                StyleId = schedule.ID.ToString()
            };

            displaySchedule.Children.Add(SetBlankBox_TopinScheduleStack(schedule.StartMinute));
            displaySchedule.Children.Add(SetTitleinScheduleStack(schedule.ScheduleTitle));
            displaySchedule.Children.Add(SetBlackBox_BottominScheduleStack(schedule.EndMinute));

            Grid.SetRow(displaySchedule, startHourRowIDX);
            Grid.SetColumn(displaySchedule, scheduleColIDX);
            Grid.SetRowSpan(displaySchedule, span);

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (s, e) =>
            {

                var temp = await DisplayActionSheet("스케쥴 관리", "취소", "삭제", "수정");
                await DisplayAlert("", temp, "ok");
                //bool answer=await DisplayAlert("클릭", "클릭했다", "취소","확인");
                //if (!answer)
                //{
                //    await DisplayAlert("", "취소버튼눌렀음", "ok");
                //}
                //테스트용이었다.

            };

            displaySchedule.GestureRecognizers.Add(tapGestureRecognizer);

            return displaySchedule;
        }

        private Label SetTitleinScheduleStack(string title)
        {
            var Title = new Label
            {
                Text = title,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            return Title;
        }

        private BoxView SetBlankBox_TopinScheduleStack(int startMinute)
        {
            var box_top = new BoxView
            {
                BackgroundColor = Color.White,
                HeightRequest = GetStartMinuteHeightRequest(startMinute)
            };
            return box_top;
        }
        
        private BoxView SetBlackBox_BottominScheduleStack(int endMinute)
        {
            var box_bottom = new BoxView
            {
                BackgroundColor = Color.White,
                HeightRequest = GetEndMinuteHeightRequest(endMinute)
            };
            return box_bottom;
        }

        private double GetStartMinuteHeightRequest(int minute)
        {
            double heightRequest= 20 * ((minute / 6) * 0.4);
            return heightRequest;
        }

        private double GetEndMinuteHeightRequest(int minute)
        {
            double heightRequest = 80-(20 * ((minute / 6) * 0.4));
            return heightRequest;
        }

        private bool IsNight(int hour)
        {
            if (hour < 20 && hour > 07)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsWeekend(int weekdate)
        {
            if (weekdate > 5)
            {
                return true;
            }
            else return false;
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

