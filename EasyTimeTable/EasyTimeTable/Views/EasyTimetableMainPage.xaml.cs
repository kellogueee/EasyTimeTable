using EasyTimeTable.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyTimeTable.DatabaseLayer;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EasyTimeTable.DatabaseLayer.Entity;

namespace EasyTimeTable.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EasyTimetableMainPage : ContentPage
    {
        private int today = 0;
        private string dayofweek;

        private readonly SQLiteDatabase _database;


        /// <summary>
        /// 최초 표시된 스케쥴들. 만약 다시 화면이 나타났을때 남아있는 ID는 삭제된 ID다.
        /// </summary>
        List<int> tempIDList;


        public EasyTimetableMainPage()
        {
            InitializeComponent();
            tempIDList = new List<int>();
            today = DateService.GetTodayDateIndex();
            dayofweek = DateService.GetTodayDayofWeek(today);
            todayTitle.Text = dayofweek + " (" + DateTime.Today.Month + "."  + DateTime.Today.Day + ")";

            _database = new DatabaseService().SQLiteDatabase;

            
        }


        async void InsertScheduleFrameInStack()
        {
            var monSchedule = await _database.GetAllScheduleAsyncByWeekdate(1);
            CreateFrameForEachDay(monSchedule, 1);

            var tueSchedule = await _database.GetAllScheduleAsyncByWeekdate(2);
            CreateFrameForEachDay(tueSchedule, 2);

            var wedSchedule = await _database.GetAllScheduleAsyncByWeekdate(3);
            CreateFrameForEachDay(wedSchedule, 3);

            var thuSchedule = await _database.GetAllScheduleAsyncByWeekdate(4);
            CreateFrameForEachDay(thuSchedule, 4);

            var friSchedule = await _database.GetAllScheduleAsyncByWeekdate(5);
            CreateFrameForEachDay(friSchedule, 5);

            var satSchedule = await _database.GetAllScheduleAsyncByWeekdate(6);
            CreateFrameForEachDay(satSchedule, 6);

            var sunSchedule = await _database.GetAllScheduleAsyncByWeekdate(0);
            CreateFrameForEachDay(sunSchedule, 7);


        }
       


        void CreateFrameForEachDay(List<Schedule> schedule,int date)
        {
        

            if (schedule.Count==0||schedule is null)
            {
                return;
            }

            foreach (var item in schedule)
            {

                var absolute = new AbsoluteLayout
                {
                    ClassId=item.ID.ToString()
                };

                Grid.SetColumn(absolute, date - 1);
                Grid.SetRow(absolute, item.ScheduleStartHour);
                
                
                //앱솔루트안에 들어갈 내용
                var frame = new Frame
                {
                    BackgroundColor = Color.FromHex(item.ScheduleColor),
                    ClassId=item.ID.ToString()
                };


                var endMinute = item.ScheduleEndMinute;
                var startMinute = item.ScheduleStartMinute;


                int totalMinute = (item.ScheduleEndHour - item.ScheduleStartHour) * 60 + (item.ScheduleEndMinute - item.ScheduleStartMinute);

                var span = (totalMinute / 60)+1;

                Grid.SetRowSpan(absolute, span);

                AbsoluteLayout.SetLayoutBounds(frame, new Rectangle(0, item.ScheduleStartMinute, 1, totalMinute));

                AbsoluteLayout.SetLayoutFlags(frame, AbsoluteLayoutFlags.WidthProportional);


                var frameStack = new StackLayout();

                frame.Content = frameStack;

                var title = new Label
                {
                    Text = item.ScheduleTitle,
                    FontAttributes = FontAttributes.Bold,
                };
                var content = new Label
                {
                    Text = item.ScheduleContents,
                    VerticalOptions=LayoutOptions.CenterAndExpand
                };

                frameStack.Children.Add(title);
                frameStack.Children.Add(content);

                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += OnScheduleFrameTapped;
                frame.GestureRecognizers.Add(tapGestureRecognizer);

                absolute.Children.Add(frame);

                IntersectionGrid.Children.Add(absolute);
            }
        }


        private async void OnScheduleFrameTapped(object sender, EventArgs e)
        {
            var frame = (Frame)sender;
            var schedule = await _database.GetScheduleById(int.Parse(frame.ClassId));
            await Navigation.PushModalAsync(new AddShcedulePage(schedule));

        }




        private async void OnCellTapped(object sender, EventArgs e)
        {
            var stack = (StackLayout)sender;

            var row = Grid.GetRow(stack);
            var col = Grid.GetColumn(stack);

            var hour = row;
            var date = (col + 1) % 7;

            await Navigation.PushModalAsync(new AddShcedulePage(hour, date));
        }


        private async void OnHorizontalScroll(object sender, ScrolledEventArgs e)
        {
            var currenty = intersection.ScrollY;
            await intersection.ScrollToAsync(e.ScrollX, currenty, false);
        }

        private async void OnVerticalScroll(object sender, ScrolledEventArgs e)
        {
            var currentx = intersection.ScrollX;
            await intersection.ScrollToAsync(currentx, e.ScrollY, false);
        }

        private async void OnIntersectionScroll(object sender, ScrolledEventArgs e)
        {
            var curretx = horizontalScroll.ScrollX;
            var currety = verticalScroll.ScrollY;

            //x방향 스크롤 변화
            if (e.ScrollX != curretx)
            {
                await horizontalScroll.ScrollToAsync(e.ScrollX, currety, false);
                await intersection.ScrollToAsync(e.ScrollX, currety, false);
            }
            if (e.ScrollY != currety)
            {
                await verticalScroll.ScrollToAsync(curretx, e.ScrollY, false);
                await intersection.ScrollToAsync(curretx, e.ScrollY, false);
            }
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Navigation.NavigationStack[0] is FirstInformationCarouselPage)
            {
                Navigation.RemovePage(Navigation.NavigationStack[0]);
            }
            var count = IntersectionGrid.Children.Count;
            for (var i = 168; i < count; i++)
            {
                IntersectionGrid.Children.Remove(IntersectionGrid.Children.LastOrDefault());
            }
            InsertScheduleFrameInStack();
        }

        //시작할때도 뜸
        protected override async void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            var remaindWidth = width - 59;

            if (width < 450)
            {
                HorizontalGrid.ColumnDefinitions.Clear();
                HorizontalGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = remaindWidth / 5 });
                HorizontalGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = remaindWidth / 5 });
                HorizontalGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = remaindWidth / 5 });
                HorizontalGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = remaindWidth / 5 });
                HorizontalGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = remaindWidth / 5 });
                HorizontalGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = remaindWidth / 5 });
                HorizontalGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = remaindWidth / 5 });

                IntersectionGrid.ColumnDefinitions.Clear();
                IntersectionGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = remaindWidth / 5 });
                IntersectionGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = remaindWidth / 5 });
                IntersectionGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = remaindWidth / 5 });
                IntersectionGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = remaindWidth / 5 });
                IntersectionGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = remaindWidth / 5 });
                IntersectionGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = remaindWidth / 5 });
                IntersectionGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = remaindWidth / 5 });

                //조금 딜레이를 줘야하네.
                await Task.Delay(50);

                switch (today)
                {
                    case 1:
                        await horizontalScroll.ScrollToAsync(Monday, ScrollToPosition.Start, false);
                        break;
                    case 2:
                        await horizontalScroll.ScrollToAsync(Tuesday, ScrollToPosition.Start, false);
                        break;
                    case 3:
                        await horizontalScroll.ScrollToAsync(Wednesday, ScrollToPosition.Start, false);
                        break;
                    case 4:
                        await horizontalScroll.ScrollToAsync(Thursday, ScrollToPosition.Start, false);
                        break;
                    case 5:
                        await horizontalScroll.ScrollToAsync(Friday, ScrollToPosition.Start, false);
                        break;
                    case 6:
                        await horizontalScroll.ScrollToAsync(Saturday, ScrollToPosition.Start, false);
                        break;
                    case 0:
                        await horizontalScroll.ScrollToAsync(Sunday, ScrollToPosition.Start, false);
                        break;
                }
               
            }


            else
            {
                HorizontalGrid.ColumnDefinitions.Clear();
                HorizontalGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = remaindWidth / 7 });
                HorizontalGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = remaindWidth / 7 });
                HorizontalGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = remaindWidth / 7 });
                HorizontalGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = remaindWidth / 7 });
                HorizontalGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = remaindWidth / 7 });
                HorizontalGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = remaindWidth / 7 });
                HorizontalGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = remaindWidth / 7 });
                IntersectionGrid.ColumnDefinitions.Clear();
                IntersectionGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = remaindWidth / 7 });
                IntersectionGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = remaindWidth / 7 });
                IntersectionGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = remaindWidth / 7 });
                IntersectionGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = remaindWidth / 7 });
                IntersectionGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = remaindWidth / 7 });
                IntersectionGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = remaindWidth / 7 });
                IntersectionGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = remaindWidth / 7 });

            }

            var currentHour = DateTime.Now.Hour;
            await Task.Delay(50);
            await verticalScroll.ScrollToAsync(0, (60 * currentHour)+5, false);



        }

        private async void OnDayScheduleTapped(object sender, EventArgs e)
        {
            switch (today)
            {
                case 1:
                    await horizontalScroll.ScrollToAsync(Monday, ScrollToPosition.Start, false);
                    break;
                case 2:
                    await horizontalScroll.ScrollToAsync(Tuesday, ScrollToPosition.Start, false);
                    break;
                case 3:
                    await horizontalScroll.ScrollToAsync(Wednesday, ScrollToPosition.Start, false);
                    break;
                case 4:
                    await horizontalScroll.ScrollToAsync(Thursday, ScrollToPosition.Start, false);
                    break;
                case 5:
                    await horizontalScroll.ScrollToAsync(Friday, ScrollToPosition.Start, false);
                    break;
                case 6:
                    await horizontalScroll.ScrollToAsync(Saturday, ScrollToPosition.Start, false);
                    break;
                case 0:
                    await horizontalScroll.ScrollToAsync(Sunday, ScrollToPosition.Start, false);
                    break;
            }
            await verticalScroll.ScrollToAsync(0, 485, false);
        }


        private async void OnNightScheduleTapped(object sender, EventArgs e)
        {
            switch (today)
            {
                case 1:
                    await horizontalScroll.ScrollToAsync(Monday, ScrollToPosition.Start, false);
                    break;
                case 2:
                    await horizontalScroll.ScrollToAsync(Tuesday, ScrollToPosition.Start, false);
                    break;
                case 3:
                    await horizontalScroll.ScrollToAsync(Wednesday, ScrollToPosition.Start, false);
                    break;
                case 4:
                    await horizontalScroll.ScrollToAsync(Thursday, ScrollToPosition.Start, false);
                    break;
                case 5:
                    await horizontalScroll.ScrollToAsync(Friday, ScrollToPosition.Start, false);
                    break;
                case 6:
                    await horizontalScroll.ScrollToAsync(Saturday, ScrollToPosition.Start, false);
                    break;
                case 0:
                    await horizontalScroll.ScrollToAsync(Sunday, ScrollToPosition.Start, false);
                    break;
            }
            await verticalScroll.ScrollToAsync(0,60*20+5, false);
           
        }

    }
}