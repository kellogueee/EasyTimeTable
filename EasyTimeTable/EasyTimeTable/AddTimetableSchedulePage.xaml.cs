using EasyTimeTable.DataAccessLayer;
using EasyTimeTable.Models;
using EasyTimeTable.ViewModels;
using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EasyTimeTable
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTimetableSchedulePage : ContentPage
    {


        private static string[] colorArray = new string[] { "#ff837f", "#89a5ea", "#a5ea89", "#ffcb6b", "#e96ec2", "#5dc2c4", "#cbde8c" };
        private static string[] dateNameArray = new string[] {"시간", "월", "화", "수", "목", "금", "토", "일" };
        //private static readonly int[] dayHours = new int[] { 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };
        //private static readonly int[] nightHours = new int[] { 20, 21, 22, 23, 0, 1, 2, 3, 4, 5, 6, 7 };
        private static Color currentSelectedItemBackgroundColor = Color.LightGray;


        //그냥 추가 눌렀을때
        public AddTimetableSchedulePage()
        {
            InitializeComponent();

            CreateColorBoxs(ColorBoxGridList);
            CreateDateBoxStacks(DateBoxGridList);
            CreateHourBoxStacks(StartHourBoxGrid, DayNightSwitch_Start);
            CreateHourBoxStacks(EndHourBoxGrid, DayNightSwitch_End);

        }

        //시간표 빈칸 눌렀을 때
        public AddTimetableSchedulePage(int hour, int date)
        {
            InitializeComponent();

            //기본세팅
            CreateColorBoxs(ColorBoxGridList);
            CreateDateBoxStacks(DateBoxGridList);
            CreateHourBoxStacks(StartHourBoxGrid, DayNightSwitch_Start);
            CreateHourBoxStacks(EndHourBoxGrid, DayNightSwitch_End);

            //추가세팅
            SetDayNightSwitchToggled(hour, DayNightSwitch_Start, StartHourBoxGrid);
            SetDateBoxGridList(date);
        }

        //시간표 스케줄 눌렀을 때
        public AddTimetableSchedulePage(ScheduleTimetable schedule)
        {
            InitializeComponent();

            //기본세팅
            CreateColorBoxs(ColorBoxGridList);
            CreateDateBoxStacks(DateBoxGridList);
            CreateHourBoxStacks(StartHourBoxGrid, DayNightSwitch_Start);
            CreateHourBoxStacks(EndHourBoxGrid, DayNightSwitch_End);



        }

        private void SetDateBoxGridList(int date)
        {
            foreach (var item in DateBoxGridList.Children)
            {
                var parent = (StackLayout)item;
                var child = (StackLayout)parent.Children.FirstOrDefault();
                var childchild = (Label)child.Children.FirstOrDefault();
                if (dateNameArray.IndexOf(childchild.Text) == date)
                {
                    child.BackgroundColor = currentSelectedItemBackgroundColor;
                    break;
                }
            }
        }


        private void SetDayNightSwitchToggled(int hour, Switch DayOrNight, Grid HourBoxGrid)
        {
            //주간표다.
            if (hour > 7 && hour < 20)
            {
                DayOrNight.IsToggled = false;
                SetHourGridBox(HourBoxGrid, hour);
            }

            //야간표다.
            else
            {
                DayOrNight.IsToggled = true;
                SetHourGridBox(HourBoxGrid, hour);
            }
        }

        private void SetHourGridBox(Grid StartOrEnd, int hour)
        {
            foreach (var item in StartOrEnd.Children)
            {
                var stack = (StackLayout)item;
                var label = (Label)stack.Children.FirstOrDefault();

                if (int.Parse(label.Text) == hour)
                {
                    stack.BackgroundColor = currentSelectedItemBackgroundColor;
                    break;
                }
            }
        }


        #region 색깔 설정 박스 부분
        private void CreateColorBoxs(Grid ColorBoxGridList)
        {
            int GridRowCount = ColorBoxGridList.RowDefinitions.Count;
            int GridColCount = ColorBoxGridList.ColumnDefinitions.Count;

            for (var row = 0; row < GridRowCount; row++)
            {
                for (var col = 0; col < GridColCount; col++)
                {
                    var box = CreateColorBox(row, col);
                    Grid.SetRow(box, row);
                    Grid.SetColumn(box, col);
                    ColorBoxGridList.Children.Add(box);
                }
            }
        }

        private BoxView CreateColorBox(int row, int col)
        {
            var box = new BoxView()
            {
                BackgroundColor = Color.FromHex(colorArray[col])
            };

            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += OnColorTapGestureRecognizerTapped;
            box.GestureRecognizers.Add(tap);

            return box;
        }

        private void OnColorTapGestureRecognizerTapped(object sender, EventArgs e)
        {
            var colorBox = (BoxView)sender;
            currentSelectedItemBackgroundColor = colorBox.BackgroundColor;
            ChangeSelectedItemsBackgroundColor(currentSelectedItemBackgroundColor);
        }

        private void ChangeSelectedItemsBackgroundColor(Color selectedBackgroundColor)
        {
            ChangeSelecedDateItemBackgroundColor(selectedBackgroundColor);
            ChangeSelectedStartHourItemBackgroundColor(selectedBackgroundColor);
            ChangeSelectedEndHourItemBackgroundColor(selectedBackgroundColor);
        }

        private void ChangeSelecedDateItemBackgroundColor(Color selectedBackgroundColor)
        {
            foreach (var DateBoxStack in DateBoxGridList.Children)
            {
                var parent = (StackLayout)DateBoxStack;
                var childStack = (StackLayout)parent.Children.FirstOrDefault();
                if (childStack.BackgroundColor != Color.White)
                {
                    childStack.BackgroundColor = selectedBackgroundColor;
                    break;
                }
            }
        }

        private void ChangeSelectedStartHourItemBackgroundColor(Color selectedBackgroundColor)
        {
            foreach (var item in StartHourBoxGrid.Children)
            {
                var stack = (StackLayout)item;
                if (stack.BackgroundColor != Color.White)
                {
                    stack.BackgroundColor = selectedBackgroundColor;
                    break;
                }
            }
        }

        private void ChangeSelectedEndHourItemBackgroundColor(Color selectedBackgroundColor)
        {
            foreach (var item in EndHourBoxGrid.Children)
            {
                var stack = (StackLayout)item;
                if (stack.BackgroundColor != Color.White)
                {
                    stack.BackgroundColor = selectedBackgroundColor;
                    break;
                }

            }
        }
        #endregion

        #region 요일 설정 박스 부분

        private void CreateDateBoxStacks(Grid DateBoxGridList)
        {
            int GridRowCount = DateBoxGridList.RowDefinitions.Count;
            int GridColCount = DateBoxGridList.ColumnDefinitions.Count;

            for (var row = 0; row < GridRowCount; row++)
            {
                for (var col = 0; col < GridColCount; col++)
                {
                    var stack = CreateDateOutterStack(col);
                    Grid.SetRow(stack, row);
                    Grid.SetColumn(stack, col);
                    DateBoxGridList.Children.Add(stack);
                }
            }
        }

        private StackLayout CreateDateOutterStack(int col)
        {
            var outterStack = new StackLayout
            {
                BackgroundColor = Color.LightGray
            };

            var innerStack = CreateDateInnerStack(col);
            outterStack.Children.Add(innerStack);

            return outterStack;
        }

        private StackLayout CreateDateInnerStack(int col)
        {
            var innerStack = new StackLayout
            {
                Style = (Style)Resources.Where(x => x.Key == "DateInnerStack").FirstOrDefault().Value
            };

            var dateName = CreateDateNameLabel(col);
            innerStack.Children.Add(dateName);

            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += OnDateTapGestureRecognizerTapped;
            innerStack.GestureRecognizers.Add(tap);

            return innerStack;
        }

        private Label CreateDateNameLabel(int col)
        {
            var dateName = new Label
            {
                Style = (Style)Resources.Where(x => x.Key == "DateName").FirstOrDefault().Value,
                Text = dateNameArray[col+1]
            };

            return dateName;
        }

        private void OnDateTapGestureRecognizerTapped(object sender, EventArgs e)
        {
            var middleStack = (StackLayout)sender;
            DateBoxGridClear();
            middleStack.BackgroundColor = currentSelectedItemBackgroundColor;
        }

        private void DateBoxGridClear()
        {
            foreach (var item in DateBoxGridList.Children)
            {
                var parent = (StackLayout)item;
                var child = (StackLayout)parent.Children.FirstOrDefault();
                child.BackgroundColor = Color.White;
            }
        }

        #endregion

        #region 시간 설정 박스 부분
        private void CreateHourBoxStacks(Grid hourGrid, Switch dayNightSwitch)
        {
            int GridRowCount = hourGrid.RowDefinitions.Count;
            int GridColCount = hourGrid.ColumnDefinitions.Count;

            for (var row = 0; row < GridRowCount; row++)
            {
                for (var col = 0; col < GridColCount; col++)
                {
                    var stack = CreateHourBoxStack(row, col, dayNightSwitch);
                    Grid.SetRow(stack, row);
                    Grid.SetColumn(stack, col);
                    hourGrid.Children.Add(stack);

                }
            }
        }

        private StackLayout CreateHourBoxStack(int row, int col, Switch dayNightSwitch)
        {
            var boxStack = new StackLayout
            {
                Style = (Style)Resources.Where(x => x.Key == "HourBox").FirstOrDefault().Value
            };

            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += OnHourTapGestureRecognizerTapped;
            boxStack.GestureRecognizers.Add(tap);

            boxStack.Children.Add(CreateHourLabel(row, col, dayNightSwitch));

            return boxStack;
        }

        private Label CreateHourLabel(int row, int col, Switch dayNightSwitch)
        {
            var DayNightTrigger = new DataTrigger(typeof(Label));
            Binding b = new Binding
            {
                Source = dayNightSwitch,
                Path = "IsToggled"
            };
            DayNightTrigger.Binding = b;
            DayNightTrigger.Value = true;

            var label = new Label
            {
                Style = (Style)Resources.Where(x => x.Key == "Hours").FirstOrDefault().Value
            };

            if (row == 0)
            {
                var hour = col + 8;
                label.Text = string.Format("{0:00}", hour);
                if (col < 4)
                {
                    hour += 12;
                    DayNightTrigger.Setters.Add(new Setter
                    {
                        Property = Label.TextProperty,
                        Value = string.Format("{0:00}", hour)
                    });
                }
                else
                {
                    hour -= 12;
                    DayNightTrigger.Setters.Add(new Setter
                    {
                        Property = Label.TextProperty,
                        Value = string.Format("{0:00}", hour)
                    });
                }


            }
            else if (row == 1)
            {
                var hour = col + 14;
                label.Text = string.Format("{0:00}", hour);
                DayNightTrigger.Setters.Add(new Setter
                {
                    Property = Label.TextProperty,
                    Value = string.Format("{0:00}", hour - 12)
                });
            }
            label.Triggers.Add(DayNightTrigger);

            return label;
        }

        private void HourBoxBackgroundColorClear(Grid DayOrNight)
        {
            foreach (var item in DayOrNight.Children)
            {
                var stack = (StackLayout)item;
                stack.BackgroundColor = Color.White;
            }
        }

        private void OnHourTapGestureRecognizerTapped(object sender, EventArgs e)
        {
            var stack = (StackLayout)sender;
            var grid = (Grid)stack.Parent;
            var gridName = grid.Id.ToString();

            if (gridName == "StartHourBoxGrid")
            {

            }
            else if (gridName == "EndHourBoxGrid")
            {

            }

            HourBoxBackgroundColorClear(grid);
            stack.BackgroundColor = currentSelectedItemBackgroundColor;
        }

        private void OnStartSwitchToggled(object sender, EventArgs e)
        {
            HourBoxBackgroundColorClear(StartHourBoxGrid);
        }

        private void OnEndSwitchToggled(object sender, EventArgs e)
        {
            HourBoxBackgroundColorClear(EndHourBoxGrid);
        }
        #endregion
    }
}