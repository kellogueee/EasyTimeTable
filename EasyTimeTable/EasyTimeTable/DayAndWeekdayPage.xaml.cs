using EasyTimeTable.Constant;
using EasyTimeTable.DataAccessLayer;
using EasyTimeTable.DataAccessLayer.SqliteEntity;
using FFImageLoading.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EasyTimeTable
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DayAndWeekdayPage : ContentPage
    {
        private readonly IDatabase<IterativeSchedule> _database;
        private static double RowHeight = 60;
        public DayAndWeekdayPage()
        {
            InitializeComponent();
            _database = new DatabaseService().SQLiteDatabase;

            for (var row = 0; row < 12; row++)
            {
                for (var col = 0; col < 6; col++)
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
                        int date = col;

                        TapGestureRecognizer tap = new TapGestureRecognizer();
                        tap.Tapped += async (s, e) =>
                        {
                            await Navigation.PushModalAsync(new AddTimetableSchedulePage(hour, date));
                        };
                        stack.GestureRecognizers.Add(tap);
                    }

                    Grid.SetRow(stack, row);
                    Grid.SetColumn(stack, col);
                    DayAndWeekdayBody.Children.Add(stack);
                }
            }


        }




        protected async override void OnAppearing()
        {
            base.OnAppearing();
            

            //1이 월요일로 맞출것
            var monList = await _database.GetAllScheduleByWeekdate(1);
            var tueList = await _database.GetAllScheduleByWeekdate(2);
            var wedList = await _database.GetAllScheduleByWeekdate(3);
            var thuList = await _database.GetAllScheduleByWeekdate(4);
            var friList = await _database.GetAllScheduleByWeekdate(5);

            var AllWeekdayList = await _database.GetAllScheduleAsync();
            AllWeekdayList = AllWeekdayList.Where(x => x.WeekDate > 0 && x.WeekDate < 6&&x.StartHour>7&&x.StartHour<20).ToList();
            //ClassID는 DB ID임
            var items = DayAndWeekdayBody.Children.Where(x => x.StyleId == "scheduleStack");

            

            //현재 페이지에 스케줄스택이 이미 나와있음
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
                        DayAndWeekdayBody.Children.Remove(item);
                    }

                }

                var UpdatedSchedule = AllWeekdayList.Where(x => !filteredSchedule.Select(x => int.Parse(x.ClassId)).Contains(x.ID)).ToList();
                foreach (var item in UpdatedSchedule)
                {
                    var col = item.WeekDate;
                    var row = item.StartHour - 8;

                    var span = item.EndHour - item.StartHour;
                    span++;
                    var stack = SetScheduleStack(item);

                    Grid.SetRow(stack, row);
                    Grid.SetColumn(stack, col);
                    Grid.SetRowSpan(stack, span);

                    DayAndWeekdayBody.Children.Add(stack);
                }

            }

            //처음 실행시 DB에서 스택 불러옴
            else
            {
                foreach (var item in monList.Where(x => x.StartHour < 20 && x.StartHour > 7))
                {
                    var col = item.WeekDate;
                    var row = item.StartHour - 8;

                    var span = item.EndHour - item.StartHour;
                    span++;
                    var stack = SetScheduleStack(item);

                    Grid.SetRow(stack, row);
                    Grid.SetColumn(stack, col);
                    Grid.SetRowSpan(stack, span);

                    DayAndWeekdayBody.Children.Add(stack);
                }

                foreach (var item in tueList.Where(x => x.StartHour < 20 && x.StartHour > 7))
                {
                    var col = item.WeekDate;
                    var row = item.StartHour - 8;

                    var span = item.EndHour - item.StartHour;
                    span++;
                    var stack = SetScheduleStack(item);

                    Grid.SetRow(stack, row);
                    Grid.SetColumn(stack, col);
                    Grid.SetRowSpan(stack, span);

                    DayAndWeekdayBody.Children.Add(stack);
                }

                foreach (var item in wedList.Where(x => x.StartHour < 20 && x.StartHour > 7))
                {
                    var col = item.WeekDate;
                    var row = item.StartHour - 8;

                    var span = item.EndHour - item.StartHour;
                    span++;
                    var stack = SetScheduleStack(item);

                    Grid.SetRow(stack, row);
                    Grid.SetColumn(stack, col);
                    Grid.SetRowSpan(stack, span);

                    DayAndWeekdayBody.Children.Add(stack);
                }

                foreach (var item in thuList.Where(x => x.StartHour < 20 && x.StartHour > 7))
                {
                    var col = item.WeekDate;
                    var row = item.StartHour - 8;

                    var span = item.EndHour - item.StartHour;
                    span++;
                    var stack = SetScheduleStack(item);

                    Grid.SetRow(stack, row);
                    Grid.SetColumn(stack, col);
                    Grid.SetRowSpan(stack, span);

                    DayAndWeekdayBody.Children.Add(stack);
                }

                foreach (var item in friList.Where(x => x.StartHour < 20 && x.StartHour > 7))
                {
                    var col = item.WeekDate;
                    var row = item.StartHour - 8;

                    var span = item.EndHour - item.StartHour;
                    span++;
                    var stack = SetScheduleStack(item);

                    Grid.SetRow(stack, row);
                    Grid.SetColumn(stack, col);
                    Grid.SetRowSpan(stack, span);

                    DayAndWeekdayBody.Children.Add(stack);
                }
            }
        }
        private StackLayout SetScheduleStack(IterativeSchedule schedule)
        {
            var stack = new StackLayout
            {
                BackgroundColor=Color.FromHex(schedule.SelectedColor),
                Margin=new Thickness(1),
                StyleId="scheduleStack",
                ClassId=schedule.ID.ToString()
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

            stack.GestureRecognizers.Add(tap);
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
            Application.Current.MainPage = new NavigationPage(new NightAndWeekdayPage());
        }

        private void OnChangeWeekdaysToWeekendButtonClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new DayAndWeekendPage());
        }
        
        #endregion
    }
}