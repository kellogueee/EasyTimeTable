using EasyTimeTable.DataAccessLayer;
using EasyTimeTable.DataAccessLayer.SqliteEntity;
using FFImageLoading.Forms;
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
    public partial class DayAndWeekendPage : ContentPage
    {
        private readonly IDatabase<IterativeSchedule> _database;
        private static double RowHeight = 60;

        public DayAndWeekendPage()
        {
            InitializeComponent();
            _database = new DatabaseService().SQLiteDatabase;

            for(var row = 0; row < 12; row++)
            {
                for(var col = 0; col < 3; col++)
                {
                    var stack = new StackLayout
                    {
                        BackgroundColor = Color.White
                    };
                    if (col == 0)
                    {
                        var label = new Label
                        {
                            Text = string.Format("{0:00}", row + 8),
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            HorizontalOptions = LayoutOptions.CenterAndExpand
                        };
                        stack.Children.Add(label);
                    }

                    else
                    {
                        int hour = row + 8;
                        int date = (col+5)%7;

                        TapGestureRecognizer tap = new TapGestureRecognizer();
                        tap.Tapped += async (s, e) =>
                        {
                            await Navigation.PushModalAsync(new AddTimetableSchedulePage(hour, date));
                        };
                        stack.GestureRecognizers.Add(tap);
                    }

                    Grid.SetRow(stack, row);
                    Grid.SetColumn(stack, col);
                    DayAndWeekendBody.Children.Add(stack);
                }
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //1이 월요일로 맞출것
            //0이 일요일
            var satList = await _database.GetAllScheduleByWeekdate(6);
            var sunList = await _database.GetAllScheduleByWeekdate(0);

            var AllWeekdayList = await _database.GetAllScheduleAsync();
            AllWeekdayList = AllWeekdayList.Where(x => !(x.WeekDate > 0 && x.WeekDate < 6) && x.StartHour > 7 && x.StartHour < 20).ToList();
            //ClassID는 DB ID임
            var items = DayAndWeekendBody.Children.Where(x => x.StyleId == "scheduleStack");

            if (items.Count() > 0)
            {
                var removalSchedule = items.Where(x => !AllWeekdayList.Select(x => x.ID).Contains(int.Parse(x.ClassId)));
                var filteredSchedule = items.Where(x => !removalSchedule.Select(x => x.ClassId).Contains(x.ClassId));

                //나와있는 스택 중에 사라진 스택이 있다면
                if (removalSchedule.Count() > 0)
                {
                    List<View> temp = new List<View>();

                    //복사를 해야한다.
                    foreach (var item in removalSchedule)
                    {
                        temp.Add(item);
                    }
                    //복사한 리스트를 호출
                    foreach (var item in temp)
                    {
                        DayAndWeekendBody.Children.Remove(item);
                    }

                }

                var UpdatedSchedule = AllWeekdayList.Where(x => !filteredSchedule.Select(x => int.Parse(x.ClassId)).Contains(x.ID)).ToList();
                foreach (var item in UpdatedSchedule)
                {
                    int col = 0;
                    if (item.WeekDate == 6)
                    {
                        col = 1;
                    }
                    else if (item.WeekDate == 0)
                    {
                        col = 2;
                    }
                    var row = item.StartHour - 8;

                    var span = item.EndHour - item.StartHour;
                    span++;
                    var stack = SetScheduleStack(item);

                    Grid.SetRow(stack, row);
                    Grid.SetColumn(stack, col);
                    Grid.SetRowSpan(stack, span);

                    DayAndWeekendBody.Children.Add(stack);
                }


            }

            else
            {
                foreach (var item in satList.Where(x => (x.StartHour < 20 && x.StartHour > 7)))
                {
                    var col = 1;
                    var row = item.StartHour - 8;

                    var span = item.EndHour - item.StartHour;
                    span++;
                    var stack = SetScheduleStack(item);

                    Grid.SetRow(stack, row);
                    Grid.SetColumn(stack, col);
                    Grid.SetRowSpan(stack, span);

                    DayAndWeekendBody.Children.Add(stack);
                }

                foreach (var item in sunList.Where(x => (x.StartHour < 20 && x.StartHour > 7)))
                {
                    var col = 2;
                    var row = item.StartHour - 8;

                    var span = item.EndHour - item.StartHour;
                    span++;
                    var stack = SetScheduleStack(item);

                    Grid.SetRow(stack, row);
                    Grid.SetColumn(stack, col);
                    Grid.SetRowSpan(stack, span);

                    DayAndWeekendBody.Children.Add(stack);
                }
            }

            

            

        }
        private StackLayout SetScheduleStack(IterativeSchedule schedule)
        {
            var stack = new StackLayout
            {
                BackgroundColor = Color.FromHex(schedule.SelectedColor),
                Margin = new Thickness(1),
                StyleId = "scheduleStack",
                ClassId = schedule.ID.ToString()
            };

            var title = new Label
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Text = schedule.ScheduleTitle
            };

            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += async (s, e) =>
            {
                await Navigation.PushModalAsync(new AddTimetableSchedulePage(schedule));
            };
            stack.GestureRecognizers.Add(tap);

            var topBox = new BoxView
            {
                BackgroundColor = Color.White,
                HeightRequest = GetTopBoxHeightRequest(schedule.StartMinute)
            };
            var endBox = new BoxView
            {
                BackgroundColor = Color.White,
                HeightRequest = GetBottomBoxHeightRequest(schedule.EndMinute)
            };

            stack.Children.Add(topBox);
            stack.Children.Add(title);
            stack.Children.Add(endBox);

            return stack;
        }
        private double GetTopBoxHeightRequest(int startMinute)
        {
            //60은 60분을 의미함
            double height = (RowHeight / 60) * startMinute;
            return height;
        }

        private double GetBottomBoxHeightRequest(int endMinute)
        {
            double height = (RowHeight / 60) * (59 - endMinute);
            return height;
        }


        #region RelatedWithNavigationBarButton
        private async void OnMenuPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MenuItemPage());
        }


        private void OnChangeDayToNightButtonClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new NightAndWeekendPage());
        }
        private void OnChangeWeekendToWeekdaysButtonClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new DayAndWeekdayPage());
        }
        #endregion
    }
}