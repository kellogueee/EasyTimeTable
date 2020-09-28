using EasyTimeTable.Constant;
using EasyTimeTable.DataAccessLayer;
using EasyTimeTable.DataAccessLayer.SqliteEntity;
using EasyTimeTable.DBCodeTestFolder;
using FFImageLoading.Forms;
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
using Xamarin.Forms.Xaml;

namespace EasyTimeTable
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {

        private static string[] tableHeadNameArray = new string[] { "시간", "월", "화", "수", "목", "금", "토", "일" };
        private static string DisplayedTimetableHead = "DisplayedTimetableHead";
        private static string DisplayedTimetableBody = "DisplayedTimetableBody";
        private int[] weekday = new int[] { 0, 1, 2, 3, 4, 5 };
        private int[] weekend = new int[] { 0, 6, 7 };
        private int[] DefaultDayHours = new int[] { 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };
        private int[] DefaultNightHours = new int[] { 20, 21, 22, 23, 0, 1, 2, 3, 4, 5, 6, 7 };
        private static double RowHeight = 50;

        private readonly IDatabase<IterativeSchedule> _database;
        private readonly IDatabaseServiceTest _databaseServiceTest;

        //여기가 Appearing보다 먼저 일어남
        public MainPage()
        {
            InitializeComponent();

            _database = new DatabaseService().SQLiteDatabase;
            //_databaseServiceTest = new SQLiteDatabaseTest().SQLiteDatabaseService;
            
        }

        //MainPage보다 늦게일어남
        protected override void OnAppearing()
        {
            base.OnAppearing();
            InitializeBasicTable();

            RemoveCurrentTable();
            View[] tempArray = new View[TableBodyGrid.Children.Count];

            TableBodyGrid.Children.CopyTo(tempArray, 0);

            foreach (var item in tempArray)
            {
                if (item is StackLayout)
                {
                    TableBodyGrid.Children.Remove(item);
                }
            }
            //DB data붙이기
            InitializeScheduleInTabletable();

            //보여질 테이블 정하기.

            //평일 낮
            if (Day.IsVisible && Weekday.IsVisible)
            {
                GenerateTable(DefaultDayHours, weekday);
            }
            
            //평일 밤
            else if (Night.IsVisible && Weekday.IsVisible)
            {
                GenerateTable(DefaultNightHours, weekday);
            }
            
            //주말 낮
            else if (Day.IsVisible && Weekend.IsVisible)
            {
                GenerateTable(DefaultDayHours, weekend);
            }
            
            //주말 밤
            else if (Night.IsVisible && Weekend.IsVisible)
            {
                GenerateTable(DefaultNightHours, weekend);
            }

        }


        #region GenesisTable
        private void InitializeBasicTable()
        {
            int gridCol = 8;
            int gridRow = 24;

            //Head
            for (var col = 0; col < gridCol; col++)
            {
                var box = new BoxView { StyleId = "0," + col.ToString() };
                Grid.SetColumn(box, col);
                TableHeadGrid.Children.Add(box);

                var date = new Label
                {
                    Text = tableHeadNameArray[col],
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    StyleId = "0," + col.ToString()
                };

                //토요일
                if (col == 6)
                {
                    date.TextColor = Color.Blue;
                }
                //일요일
                else if (col == 7)
                {
                    date.TextColor = Color.Red;
                }
                Grid.SetColumn(date, col);
                TableHeadGrid.Children.Add(date);
            }


            //Body
            for (var row = 0; row < gridRow; row++)
            {
                for (var col = 0; col < gridCol; col++)
                {
                    var box = new BoxView { StyleId = row.ToString() + "," + col.ToString() };
                    Grid.SetColumn(box, col);
                    Grid.SetRow(box, row);
                    TableBodyGrid.Children.Add(box);

                    if (col == 0)
                    {
                        var hour = new Label
                        {
                            Text = string.Format("{0:00}", row),
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            StyleId = row.ToString() + "," + col.ToString()
                        };
                        Grid.SetColumn(hour, col);
                        Grid.SetRow(hour, row);
                        TableBodyGrid.Children.Add(hour);
                    }
                }
            }


            TableBodyGrid.Children.GroupBy(x => Grid.GetRow(x));

            foreach (var eachRow in TableBodyGrid.Children.GroupBy(x => Grid.GetRow(x)))
            {
                foreach (var item in eachRow)
                {
                    var col = GetColumnOfItem(item);
                    if (col > 0)
                    {
                        TapGestureRecognizer tap = new TapGestureRecognizer();
                        tap.Tapped += async (s, e) =>
                        {
                            int hour = GetRowofItem(item);
                            int date = col;
                            await Navigation.PushModalAsync(new AddTimetableSchedulePage(hour, date));
                        };
                        item.GestureRecognizers.Add(tap);
                    }

                }
            }
        }

        private int GetColumnOfItem(View item)
        {
            var id = GetSplitedRowCol(item.StyleId);

            return int.Parse(id[1]);
        }

        private int GetRowofItem(View item)
        {
            var id = GetSplitedRowCol(item.StyleId);

            return int.Parse(id[0]);
        }

        private string[] GetSplitedRowCol(string id)
        {
            return id.Split(',');
        }
        #endregion

        #region DB작업
        private void InitializeScheduleInTabletable()
        {

            var schedules = _database.GetAllScheduleAsync().Result;



            foreach (var item in schedules)
            {
                //+1을 하는 이유는 DB에 저장한 Weekdate는 월요일부터 일요일까지 차례로 0부터 6까지 숫자가 부여.
                //그러나 운용하고있는 리스트는 {"시간", "월", ... , "일"}이기 때문에 인덱스를 위하여 +1을함.

                //Column이다.
                int date = item.WeekDate + 1;
                //Row다.
                int startHour = item.StartHour;
                //span 계산
                var stack = GenerateScheduleStack(item);
                Grid.SetRow(stack, startHour);
                Grid.SetColumn(stack, date);
                Grid.SetRowSpan(stack, GetSpan(item.StartHour, item.EndHour));
                stack.StyleId = startHour.ToString() + "," + date.ToString();


                TapGestureRecognizer tap = new TapGestureRecognizer();
                tap.Tapped += async (s, e) =>
                {
                    await Navigation.PushModalAsync(new AddTimetableSchedulePage(item));
                };
                stack.GestureRecognizers.Add(tap);

                TableBodyGrid.Children.Add(stack);



                int endHour = item.EndHour;

                //혹시 주간시작 야간마감 또는 야간시작 주간마감인 경우를 걸러보자.
                if (HasDifferentEndHourInArray(startHour, endHour))
                {
                    StackLayout partialStack = new StackLayout();
                    Grid.SetColumn(partialStack, date);
                    //주간에서 야간으로 끝남
                    if (DefaultNightHours.IndexOf(endHour) > -1)
                    {
                        partialStack = GeneratePartialScheduleStack(item);
                        Grid.SetRow(partialStack, DefaultNightHours[0]);
                        Grid.SetRowSpan(stack, GetSpan(DefaultNightHours[0], item.EndHour));
                        partialStack.StyleId = DefaultNightHours[0].ToString() + "," + date.ToString();
                    }

                    //야간에서 주간으로
                    else if (DefaultDayHours.IndexOf(endHour) > -1)
                    {
                        partialStack = GeneratePartialScheduleStack(item);
                        Grid.SetRow(partialStack, DefaultDayHours[0]);
                        Grid.SetRowSpan(stack, GetSpan(DefaultDayHours[0], item.EndHour));
                        partialStack.StyleId = DefaultDayHours[0].ToString() + "," + date.ToString();
                    }


                    TapGestureRecognizer partial_tap = new TapGestureRecognizer();
                    partial_tap.Tapped += async (s, e) =>
                    {
                        await Navigation.PushModalAsync(new AddTimetableSchedulePage(item));
                    };
                    partialStack.GestureRecognizers.Add(tap);

                    TableBodyGrid.Children.Add(partialStack);
                }

            }

        }

        private bool HasDifferentEndHourInArray(int startHour, int endHour)
        {
            if (DefaultDayHours.IndexOf(startHour) > -1)
            {
                return DefaultDayHours.IndexOf(endHour) == -1;
            }
            else if (DefaultNightHours.IndexOf(startHour) > -1)
            {
                return DefaultNightHours.IndexOf(endHour) == -1;
            }
            return false;
        }

        private StackLayout GenerateScheduleStack(IterativeSchedule schedule)
        {

            int startMinute = schedule.StartMinute;
            int endMinute = schedule.EndMinute;

            var stack = new StackLayout
            {
                BackgroundColor = Color.FromHex(schedule.SelectedColor),
                Margin = new Thickness(1)
            };

            var topBox = new BoxView
            {
                BackgroundColor = Color.White,
                HeightRequest = GetTopBoxHeightRequest(startMinute)
            };


            var title = new Label
            {
                Text = schedule.ScheduleTitle,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };


            var endBox = new BoxView
            {
                BackgroundColor = Color.White,
                HeightRequest = GetBottomBoxHeightRequest(endMinute)
            };


            stack.Children.Add(topBox);
            stack.Children.Add(title);
            stack.Children.Add(endBox);

            return stack;
        }




        private StackLayout GeneratePartialScheduleStack(IterativeSchedule schedule)
        {
            var stack = new StackLayout { BackgroundColor = Color.FromHex(schedule.SelectedColor) };

            int endMinute = schedule.EndMinute;
            var title = new Label
            {
                Text = schedule.ScheduleTitle,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };


            var endBox = new BoxView
            {
                BackgroundColor = Color.White,
                HeightRequest = GetBottomBoxHeightRequest(endMinute)
            };


            stack.Children.Add(title);
            stack.Children.Add(endBox);


            return stack;

        }

        private int GetSpan(int startHour, int endHour)
        {
            int span = endHour - startHour;
            span++;
            if (span < 0)
            {
                span += 24;
            }
            return span;
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

        #endregion

        #region WillDisplayTable
        private void GenerateTable(int[] hours, int[] dates)
        {

            //Head
            var head = TableHeadGrid.Children.Where(x => dates.Contains(Grid.GetColumn(x))).OrderBy(x => Grid.GetColumn(x)).GroupBy(x => Grid.GetColumn(x)).ToList();
            GenerateTableHead(head);

            //Body
            var body = TableBodyGrid.Children.Where(x => hours.Contains(Grid.GetRow(x)) && dates.Contains(Grid.GetColumn(x))).OrderBy(x => Grid.GetColumn(x)).GroupBy(x => Grid.GetColumn(x)).ToList();
            GenerateTableBody(body, hours);
        }


        private void GenerateTableHead(List<IGrouping<int, View>> SelectedDates)
        {
            var tableHead = new Grid
            {
                ColumnSpacing = 1,
                RowSpacing = 1,
                StyleId = DisplayedTimetableHead
            };
            int colIDX = 0;
            foreach (var GroupedView in SelectedDates)
            {
                if (GroupedView.Key == 0)
                {
                    tableHead.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });
                    foreach (var item in GroupedView)
                    {
                        Grid.SetColumn(item, colIDX);
                        tableHead.Children.Add(item);
                    }
                    colIDX++;
                }
                else
                {
                    tableHead.ColumnDefinitions.Add(new ColumnDefinition());
                    foreach (var item in GroupedView)
                    {
                        Grid.SetColumn(item, colIDX);
                        tableHead.Children.Add(item);
                    }
                    colIDX++;
                }
            }
            TableHeadStack.Children.Add(tableHead);
        }

        //int[] dates는 키 역할을 한다.
        private void GenerateTableBody(List<IGrouping<int, View>> SelectedHours, int[] hours)
        {
            var tableBody = new Grid
            {
                ColumnSpacing = 1,
                RowSpacing = 1,
                StyleId = DisplayedTimetableBody
            };

            for (var i = 0; i < hours.Length; i++)
            {
                tableBody.RowDefinitions.Add(new RowDefinition { Height = new GridLength(RowHeight) });
            }

            int colIDX = 0;
            foreach (var GroupedView in SelectedHours)
            {
                if (GroupedView.Key == 0)
                {
                    tableBody.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });
                    foreach (var item in GroupedView)
                    {
                        Grid.SetColumn(item, colIDX);
                        Grid.SetRow(item, hours.IndexOf(Grid.GetRow(item)));
                        tableBody.Children.Add(item);

                    }
                    colIDX++;
                }
                else
                {
                    tableBody.ColumnDefinitions.Add(new ColumnDefinition());
                    foreach (var item in GroupedView)
                    {
                        Grid.SetColumn(item, colIDX);
                        Grid.SetRow(item, hours.IndexOf(Grid.GetRow(item)));
                        tableBody.Children.Add(item);
                    }
                    colIDX++;
                }
            }
            TableBodyStack.Children.Add(tableBody);
        }


        private void RemoveCurrentTable()
        {
            var displayedHead = (Grid)TableHeadStack.Children.Where(x => x.StyleId == DisplayedTimetableHead).FirstOrDefault();
            if (displayedHead == null)
            {
                return;
            }

            View[] CopiedHead = new View[displayedHead.Children.Count];
            displayedHead.Children.CopyTo(CopiedHead, 0);

            foreach (var item in CopiedHead)
            {
                var row = GetRowofItem(item);
                var col = GetColumnOfItem(item);
                Grid.SetRow(item, row);
                Grid.SetColumn(item, col);
                TableHeadGrid.Children.Add(item);
            }

            TableHeadStack.Children.Remove(displayedHead);



            var displayedBody = (Grid)TableBodyStack.Children.Where(x => x.StyleId == DisplayedTimetableBody).FirstOrDefault();

            View[] CopiedBody = new View[displayedBody.Children.Count];
            displayedBody.Children.CopyTo(CopiedBody, 0);

            foreach (var item in CopiedBody)
            {
                var row = GetRowofItem(item);
                var col = GetColumnOfItem(item);
                Grid.SetRow(item, row);
                Grid.SetColumn(item, col);
                TableBodyGrid.Children.Add(item);
            }

            TableBodyStack.Children.Remove(displayedBody);
        }
        #endregion

        #region RelatedWithNavigationBarButton
        private async void OnMenuPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MenuItemPage());
        }


        private void OnChangeDayToNightButtonClicked(object sender, EventArgs e)
        {
            var imgButton = (CachedImage)sender;
            imgButton.IsVisible = false;
            Night.IsVisible = true;

            RemoveCurrentTable();

            if (Weekday.IsVisible)
            {
                GenerateTable(DefaultNightHours, weekday);
            }
            else
            {
                GenerateTable(DefaultNightHours, weekend);
            }

        }

        private void OnChangeNightToDayButtonClicked(object sender, EventArgs e)
        {
            var imgButton = (CachedImage)sender;
            imgButton.IsVisible = false;
            Day.IsVisible = true;
            RemoveCurrentTable();

            if (Weekday.IsVisible)
            {
                GenerateTable(DefaultDayHours, weekday);
            }
            else
            {
                GenerateTable(DefaultDayHours, weekend);
            }
        }

        private void OnChangeWeekdaysToWeekendButtonClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            button.IsVisible = false;
            Weekend.IsVisible = true;
            RemoveCurrentTable();

            if (Day.IsVisible)
            {
                GenerateTable(DefaultDayHours, weekend);
            }
            else
            {
                GenerateTable(DefaultNightHours, weekend);
            }

        }
        private void OnChangeWeekendToWeekdaysButtonClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            button.IsVisible = false;
            Weekday.IsVisible = true;
            RemoveCurrentTable();

            if (Day.IsVisible)
            {
                GenerateTable(DefaultDayHours, weekday);
            }
            else
            {
                GenerateTable(DefaultNightHours, weekday);
            }
        }



        #endregion

    }
}

