using System;
using System.Collections.Generic;
using EasyTimeTable.DataAccessLayer;
using EasyTimeTable.DataAccessLayer.SqliteEntity;



using Xamarin.Forms;

namespace EasyTimeTable.TestPage
{
    public partial class MyPage : ContentPage
    {

        private readonly IDatabase<IterativeSchedule> _database;

        public MyPage()
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
                            Text = string.Format("{0:00}", col + 8),
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            HorizontalOptions = LayoutOptions.CenterAndExpand
                        };
                        stack.Children.Add(label);
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



            foreach (var item in monList)
            {
                var col = item.WeekDate;
                var row = item.StartHour - 8 + 1;

                var span = item.EndHour - item.StartHour;

                var stack = new StackLayout
                {
                    BackgroundColor = Color.FromHex(item.SelectedColor)
                };

                var title = new Label
                {
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = item.ScheduleTitle
                };

                stack.Children.Add(title);

                Grid.SetRow(stack, row);
                Grid.SetColumn(stack, col);
                Grid.SetRowSpan(stack, span);

                DayAndWeekdayBody.Children.Add(stack);
            }

            foreach (var item in tueList)
            {
                var col = item.WeekDate;
                var row = item.StartHour - 8 + 1;

                var span = item.EndHour - item.StartHour;

                var stack = new StackLayout
                {
                    BackgroundColor = Color.FromHex(item.SelectedColor)
                };

                var title = new Label
                {
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = item.ScheduleTitle
                };

                stack.Children.Add(title);

                Grid.SetRow(stack, row);
                Grid.SetColumn(stack, col);
                Grid.SetRowSpan(stack, span);

                DayAndWeekdayBody.Children.Add(stack);
            }

            foreach (var item in wedList)
            {
                var col = item.WeekDate;
                var row = item.StartHour - 8 + 1;

                var span = item.EndHour - item.StartHour;

                var stack = new StackLayout
                {
                    BackgroundColor = Color.FromHex(item.SelectedColor)
                };

                var title = new Label
                {
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = item.ScheduleTitle
                };

                stack.Children.Add(title);

                Grid.SetRow(stack, row);
                Grid.SetColumn(stack, col);
                Grid.SetRowSpan(stack, span);

                DayAndWeekdayBody.Children.Add(stack);
            }


            foreach (var item in thuList)
            {
                var col = item.WeekDate;
                var row = item.StartHour - 8 + 1;

                var span = item.EndHour - item.StartHour;

                var stack = new StackLayout
                {
                    BackgroundColor = Color.FromHex(item.SelectedColor)
                };

                var title = new Label
                {
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = item.ScheduleTitle
                };

                stack.Children.Add(title);

                Grid.SetRow(stack, row);
                Grid.SetColumn(stack, col);
                Grid.SetRowSpan(stack, span);

                DayAndWeekdayBody.Children.Add(stack);
            }

            foreach (var item in friList)
            {
                var col = item.WeekDate;
                var row = item.StartHour - 8 + 1;

                var span = item.EndHour - item.StartHour;

                var stack = new StackLayout
                {
                    BackgroundColor = Color.FromHex(item.SelectedColor)
                };

                var title = new Label
                {
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = item.ScheduleTitle
                };

                stack.Children.Add(title);

                Grid.SetRow(stack, row);
                Grid.SetColumn(stack, col);
                Grid.SetRowSpan(stack, span);

                DayAndWeekdayBody.Children.Add(stack);
            }




        }



        private  async void OnAddButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddTimetableSchedulePage());
        }


    }

}
