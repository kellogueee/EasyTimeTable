using EasyTimeTable.DataAccessLayer;
using EasyTimeTable.DataAccessLayer.SqliteEntity;
using EasyTimeTable.Models;
using EasyTimeTable.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Markup;
using Xamarin.Forms.Xaml;

namespace EasyTimeTable
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTimetableSchedulePage : ContentPage
    {


        private static string[] colorArray = new string[] { "#ff837f", "#89a5ea", "#a5ea89", "#ffcb6b", "#e96ec2", "#5dc2c4", "#cbde8c" };
        private static string[] dateNameArray = new string[] {"일","월","화","수","목","금","토" };
        private List<int> selecteDates = new List<int>();
        
        private Color currentSelectedItemBackgroundColor = Color.LightGray;
        

        private IterativeSchedule selectedSchedule;
        private readonly IDatabase<IterativeSchedule> _database;

        //그냥 추가 눌렀을때
        public AddTimetableSchedulePage()
        {
            InitializeComponent();
            var rnd = new Random();
            rnd.Next(0, 6);

            currentSelectedItemBackgroundColor = Color.FromHex(colorArray[rnd.Next(0, 6)]);
            _database = new DatabaseService().SQLiteDatabase;
            CreateColorBoxs(ColorBoxGridList);
            CreateDateBoxStacks(DateBoxGridList);
            CreateHourBoxStacks(StartHourBoxGrid, DayNightSwitch_Start);
            CreateHourBoxStacks(EndHourBoxGrid, DayNightSwitch_End);

            selectedSchedule = new IterativeSchedule();

            

        }

        //시간표 빈칸 눌렀을 때
        public AddTimetableSchedulePage(int hour, int date)
        {
            InitializeComponent();
            var rnd = new Random();
            rnd.Next(0, 6);
            currentSelectedItemBackgroundColor = Color.FromHex(colorArray[rnd.Next(0, 6)]);
            _database = new DatabaseService().SQLiteDatabase;
            //기본세팅
            CreateColorBoxs(ColorBoxGridList);
            CreateDateBoxStacks(DateBoxGridList);
            CreateHourBoxStacks(StartHourBoxGrid, DayNightSwitch_Start);
            CreateHourBoxStacks(EndHourBoxGrid, DayNightSwitch_End);

            selecteDates.Add(date);

            //추가세팅
            SetDayNightSwitchToggled(hour, DayNightSwitch_Start, StartHourBoxGrid);
            SetDateBoxGridList(date);

            selectedSchedule = new IterativeSchedule();
            
        }

        //시간표 스케줄 눌렀을 때
        public AddTimetableSchedulePage(IterativeSchedule schedule)
        {
            InitializeComponent();

            selectedSchedule = schedule;
            _database = new DatabaseService().SQLiteDatabase;

            //기본세팅
            CreateColorBoxs(ColorBoxGridList);
            CreateDateBoxStacks(DateBoxGridList);
            CreateHourBoxStacks(StartHourBoxGrid, DayNightSwitch_Start);
            CreateHourBoxStacks(EndHourBoxGrid, DayNightSwitch_End);
            deleteButton.IsVisible = true;
            saveButton.IsVisible = false;
            saveButton2.IsVisible = true;
            selecteDates.Add(schedule.WeekDate);


            SetSelectedColor(schedule.SelectedColor);
            SetScheduleTitle(schedule.ScheduleTitle);
            SetDateBoxGridList(schedule.WeekDate);
            SetDayNightSwitchToggled(schedule.StartHour, DayNightSwitch_Start, StartHourBoxGrid);
            StartMinute.Value = schedule.StartMinute;
            SetDayNightSwitchToggled(schedule.EndHour, DayNightSwitch_End, EndHourBoxGrid);
            EndMinute.Value = schedule.EndMinute;

        }

        private void SetSelectedColor(string colorHex)
        {
            currentSelectedItemBackgroundColor = Color.FromHex(colorHex);
        }

        private void SetScheduleTitle(string title)
        {
            ScheduleTitle.Text = title;
        }

        private void SetDateBoxGridList(int date)
        {
            foreach (var item in DateBoxGridList.Children)
            {
                var parent = (StackLayout)item;
                var child = (StackLayout)parent.Children.FirstOrDefault();
                var childchild = (Label)child.Children.FirstOrDefault();
                var id = int.Parse(childchild.StyleId);
                if (id == date)
                {
                    child.BackgroundColor = currentSelectedItemBackgroundColor;
                    break;
                }
            }
        }

        //이 메서드도 현재 고정값임. 수정해야함
        //DayHourArray와 NightHourArray를 만들어서 contain여부를 가지고 if분기를 만들예정
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

                    if(StartOrEnd.StyleId== "StartHourBoxGrid")
                    {
                        startHourLabel.Text = ((Label)stack.Children.FirstOrDefault()).Text;

                    }
                    else if (StartOrEnd.StyleId == "EndHourBoxGrid")
                    {
                        endHourLabel.Text = ((Label)stack.Children.FirstOrDefault()).Text;
                    }

                    break;
                }
            }
        }


        /// <summary>
        /// 저장할때만 사용한다.
        /// </summary>
        /// <returns></returns>
        private List<IterativeSchedule> GetListCurrentSettedSchedule()
        {
            var schedules = new List<IterativeSchedule>();

            foreach (var item in selecteDates)
            {
                var insert = new IterativeSchedule
                {
                    SelectedColor = currentSelectedItemBackgroundColor.ToHex(),
                    WeekDate = item,
                    StartHour = int.Parse(startHourLabel.Text),
                    StartMinute = (int)StartMinute.Value,
                    EndHour = int.Parse(endHourLabel.Text),
                    EndMinute = (int)EndMinute.Value
                };

                if (ScheduleTitle.Text == null)
                {
                    insert.ScheduleTitle = "제목없음";
                }
                else
                {
                    insert.ScheduleTitle = ScheduleTitle.Text;
                }

                schedules.Add(insert);

            }

            return schedules;
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
            };
            dateName.Text = dateNameArray[(col + 1) % 7];
            dateName.StyleId = ((col + 1) % 7).ToString();
            return dateName;
        }

        private void OnDateTapGestureRecognizerTapped(object sender, EventArgs e)
        {
            var innerStack = (StackLayout)sender;
            var date = ((Label)innerStack.Children.FirstOrDefault()).StyleId;
            if (selecteDates.Contains(int.Parse(date)))
            {
                selecteDates.Remove(int.Parse(date));
                innerStack.BackgroundColor = Color.White;
            }
            else
            {
                selecteDates.Add(int.Parse(date));
                innerStack.BackgroundColor = currentSelectedItemBackgroundColor;
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



        //지금 이 메서드에는 오전 오후 시간대가 정해져있는데 훗날 사용자 편의 시간대 설정을 위해서는 메서드 수정을 해야한다.
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



            //수정이 되어야할 부분
            //지금은 오전: 8~19시 야간: 20~7시 고정값
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
            var gridName = grid.StyleId.ToString();

            if (gridName == "StartHourBoxGrid")
            {
                startHourLabel.Text = ((Label)stack.Children.FirstOrDefault()).Text;
            }
            else if (gridName == "EndHourBoxGrid")
            {
                endHourLabel.Text = ((Label)stack.Children.FirstOrDefault()).Text;
            }

            HourBoxBackgroundColorClear(grid);
            stack.BackgroundColor = currentSelectedItemBackgroundColor;
            
        }

        private void OnStartSwitchToggled(object sender, EventArgs e)
        {
            HourBoxBackgroundColorClear(StartHourBoxGrid);
            startHourLabel.Text = "";
        }

        private void OnEndSwitchToggled(object sender, EventArgs e)
        {
            HourBoxBackgroundColorClear(EndHourBoxGrid);
            endHourLabel.Text = "";
        }
        #endregion

        #region 버튼이벤트
        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var willDelete = await DisplayAlert("스케쥴 삭제", "해당 스케쥴을 삭제 하시겠습니까?", "네", "아니오");
            if (willDelete)
            {
                await _database.DeleteSchedule(selectedSchedule);
                await Navigation.PopModalAsync();
            }
        }
        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {

            if(selecteDates.Count==0|| selecteDates == null)
            {
                await DisplayAlert("저장실패", "요일을 선택하세요.", "Ok");
                return;
            }

            try
            {
                int.Parse(startHourLabel.Text);
            }
            catch
            {
                await DisplayAlert("저장실패", "시작시간을 설정하세요.", "Ok");
                return;
            }

            try
            {
                int.Parse(endHourLabel.Text);
            }
            catch
            {
                await DisplayAlert("저장실패", "종료시간을 설정하세요.", "Ok");
                return;
            }

            if (CheckLogicBetweenStartAndEnd())
            {
                await DisplayAlert("저장실패", "종료시간이 시작시간보다 빠를 수 없습니다.", "Ok");
                return;
            };
            var sches = GetListCurrentSettedSchedule();
            foreach (var item in sches)
            {
                await _database.AddSchedule(item);
            }


            await Navigation.PopModalAsync();
        }
        private async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        private async void OnUpdateButtonClicked(object sender, EventArgs e)
        {
            if (selecteDates.Count == 0 || selecteDates == null)
            {
                await DisplayAlert("저장실패", "요일을 선택하세요.", "Ok");
                return;
            }

            try
            {
                int.Parse(startHourLabel.Text);
            }
            catch
            {
                await DisplayAlert("저장실패", "시작시간을 설정하세요.", "Ok");
                return;
            }

            try
            {
                int.Parse(endHourLabel.Text);
            }
            catch
            {
                await DisplayAlert("저장실패", "종료시간을 설정하세요.", "Ok");
                return;
            }

            if (CheckLogicBetweenStartAndEnd())
            {
                await DisplayAlert("저장실패", "종료시간이 시작시간보다 빠를 수 없습니다.", "Ok");
                return;
            };
            await _database.DeleteSchedule(selectedSchedule);
            var sches = GetListCurrentSettedSchedule();
            foreach (var item in sches)
            {
                await _database.AddSchedule(item);
            }
            await Navigation.PopModalAsync();
        }



        private bool CheckLogicBetweenStartAndEnd()
        {
            int start = int.Parse(startHourLabel.Text);
            int end = int.Parse(endHourLabel.Text);

            //주간이다
            if (start > 7 && start < 20)
            {
                if (end - start < 0)
                {
                    return true;
                }
                return false;
            }

            //야간
            else
            {
                start -= 20;
                end -= 20;
                if (start < 0)
                {
                    start += 24;
                }
                if (end < 0)
                {
                    end += 24;
                }
                if (end - start < 0)
                {
                    return true;
                }
                return false;
            }
        }

        #endregion
    }
}