using EasyTimeTable.DatabaseLayer;
using EasyTimeTable.DatabaseLayer.Entity;
using EasyTimeTable.ResourceRenderer;
using EasyTimeTable.Resx;
using EasyTimeTable.ViewModels;
using FFImageLoading.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyTimeTable.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddShcedulePage : ContentPage
    {

        private readonly SQLiteDatabase _database;
        CachedImage checkedFF;
        int countOfDay = 0;
        List<int> dayofWeeks;
        int radioDay = 0;

        Schedule updateSchedule;


        private  AddScheduleViewModel AddViewModel { get; set; }

        public AddShcedulePage()
        {
            InitializeComponent();

            dayofWeeks = new List<int>();

            _database = new DatabaseService().SQLiteDatabase;

            var colors = _database.GetAllColorsAsync().Result;

            var colIDX = 0;

            foreach (var item in colors)
            {
                ColorGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 50 });

                var frame = new Frame
                {
                    BackgroundColor = Color.FromHex(item.ColorHex),
                    CornerRadius=8
                };

                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += OnColorFrameTapped;
                frame.GestureRecognizers.Add(tapGestureRecognizer);
                Grid.SetColumn(frame, colIDX);
                ColorGrid.Children.Add(frame);
                colIDX++;
            }


            AddViewModel = new AddScheduleViewModel();
            BindingContext = AddViewModel;
        }

        public AddShcedulePage(int hour,int date):this()
        {
            AddViewModel.SelectedStartHour = string.Format("{0:00}", hour);
            switch (date)
            {
                case 1:
                    monCheck.IsChecked = true;
                    break;
                case 2:
                    tueCheck.IsChecked = true;
                    break;
                case 3:
                    wedCheck.IsChecked = true;
                    break;
                case 4:
                    thuCheck.IsChecked = true;
                    break;
                case 5:
                    friCheck.IsChecked = true;
                    break;
                case 6:
                    satCheck.IsChecked = true;
                    break;
                case 0:
                    sunCheck.IsChecked = true;
                    break;
            }
        }

        public AddShcedulePage(Schedule schedule):this()
        {
            DayofWeekStackForAdd.IsVisible = false;
            DayofWeekStackForUpdate.IsVisible = true;
            saveButton.IsVisible = false;
            updateButton.IsVisible = true;
            removeButton.IsVisible = true;
            updateSchedule = schedule;


            switch (schedule.ScheuldeWeekdate)
            {
                case 1:
                    monRadio.IsChecked = true;
                    break;
                case 2:
                    tueRadio.IsChecked = true;
                    break;
                case 3:
                    wedRadio.IsChecked = true;
                    break;
                case 4:
                    thuRadio.IsChecked = true;
                    break;
                case 5:
                    friRadio.IsChecked = true;
                    break;
                case 6:
                    satRadio.IsChecked = true;
                    break;
                case 0:
                    sunRadio.IsChecked = true;
                    break;
            }
            AddViewModel.SelectedCurrentBackgroundColor = Color.FromHex(schedule.ScheduleColor);
            AddViewModel.ScheduleTitle = schedule.ScheduleTitle;
            AddViewModel.ScheduleContents = schedule.ScheduleContents;
            IterativeCheckbox.IsChecked = schedule.IsIterative;
            AddViewModel.SelectedYear = schedule.Year is null ? null:schedule.Year.ToString();
            AddViewModel.SelectedMonth = schedule.Month is null ? null : schedule.Month.ToString();
            AddViewModel.SelectedDay = schedule.Day is null ? null : schedule.Day.ToString();
            AddViewModel.SelectedStartHour = string.Format("{0:00}", schedule.ScheduleStartHour);
            AddViewModel.SelectedStartMinute = string.Format("{0:00}", schedule.ScheduleStartMinute);
            AddViewModel.SelectedEndHour = string.Format("{0:00}", schedule.ScheduleEndHour);
            AddViewModel.SelectedEndMinute = string.Format("{0:00}", schedule.ScheduleEndMinute);
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ColorGrid.ColumnDefinitions.Clear();
            ColorGrid.Children.Clear();
            
            var colors =await _database.GetAllColorsAsync();
            var colIDX = 0;
            
            foreach (var item in colors)
            {
                ColorGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 50 });
                var frame = new Frame
                {
                    BackgroundColor = Color.FromHex(item.ColorHex),
                    CornerRadius = 8
                };
                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += OnColorFrameTapped;
                frame.GestureRecognizers.Add(tapGestureRecognizer);
                Grid.SetColumn(frame, colIDX);
                Grid.SetRow(frame, 0);
                ColorGrid.Children.Add(frame);
                colIDX++;
            }

            checkedFF = new CachedImage
            {
                Source = "Check256.png",
                IsVisible = true,
                Aspect = Aspect.Fill,
                Margin = new Thickness(10)
            };

            if(updateSchedule is null)
            {
                var rand = new Random();
                var idx = rand.Next(0, 6);
                var color = colors[idx].ColorHex;
                AddViewModel.SelectedCurrentBackgroundColor =Color.FromHex(color);
                Grid.SetColumn(checkedFF, idx);
                Grid.SetRow(checkedFF, 0);
                ColorGrid.Children.Add(checkedFF);

            }
            else
            {
                var idx = colors.IndexOf(colors.Where(x => Color.FromHex(x.ColorHex) == Color.FromHex(updateSchedule.ScheduleColor)).FirstOrDefault());
                AddViewModel.SelectedCurrentBackgroundColor = Color.FromHex(updateSchedule.ScheduleColor);
                Grid.SetColumn(checkedFF, idx);
                Grid.SetRow(checkedFF, 0);
                ColorGrid.Children.Add(checkedFF);
            }

        }


        private async void OnColorEditButtonClicked(object sender, EventArgs e)
        {
            //개발후에
            //await Navigation.PushModalAsync(new EditColorPage());
            await DisplayAlert("", AppResources.Prepearing, AppResources.Ok);
        }

        private void OnColorFrameTapped(object sender, EventArgs e)
        {
            var frame = (Frame)sender;
            var row = Grid.GetRow(frame);
            var col = Grid.GetColumn(frame);
            AddViewModel.SelectedCurrentBackgroundColor = frame.BackgroundColor;

            Grid.SetRow(checkedFF, row);
            Grid.SetColumn(checkedFF, col);
            checkedFF.IsVisible = true;

        }


        private void OnComboBoxFrameTapped(object sender, EventArgs e)
        {
            var img = (CachedImage)sender;
            var parent = (Grid)img.Parent;

            var picker = (BorderlessPicker)parent.Children[0];
            picker.Focus();
        }


        private void OnDayofWeekCheckboxChecked(object sender, EventArgs e)
        {
            var check = (CheckBox)sender;
            var id = int.Parse(check.ClassId);
            if (check.IsChecked)
            {
                countOfDay++;
                dayofWeeks.Add(id);
            }
            else
            {
                countOfDay--;
                dayofWeeks.Remove(id);
            }
            if(countOfDay>1)
            {
                datePicker.IsVisible = false;
            }
            else
            {
                datePicker.IsVisible = true;
            }
        }

        private void OnDayofWeekRadioButtonChecked(object sender, EventArgs e)
        {
            var radio = (RadioButton)sender;
            radioDay = int.Parse(radio.ClassId);
        }


        private int IsValidate()
        {
            if (countOfDay == 0)
            {
                return 1;
            }
            if (IterativeCheckbox.IsChecked)
            {
                if(AddViewModel.SelectedYear is null)
                {
                    return 2;
                }
                if(AddViewModel.SelectedMonth is null)
                {
                    return 3;
                }
                if(AddViewModel.SelectedDay is null)
                {
                    return 4;
                }
            }
            if(AddViewModel.SelectedStartHour is null)
            {
                return 5;
            }
            if(AddViewModel.SelectedStartMinute is null)
            {
                return 6;
            }
            if (AddViewModel.SelectedEndHour is null)
            {
                return 7;
            }
            if(AddViewModel.SelectedEndMinute is null)
            {
                return 8;
            }
            if (int.Parse(AddViewModel.SelectedStartHour) >int.Parse( AddViewModel.SelectedEndHour))
            {
                return 9;
            }
            return 0;
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {

            switch (IsValidate())
            {
                case 1:
                    await DisplayAlert("저장실패", "요일을 선택해 주세요.", "확인");
                    return;
                case 2:
                    await DisplayAlert("저장실패", "저장할 연도를 선택해 주세요.", "확인");
                    return;
                case 3:
                    await DisplayAlert("저장실패", "저장할 월을 선택해 주세요.", "확인");
                    return;
                case 4:
                    await DisplayAlert("저장실패", "저장할 일을 선택해 주세요.", "확인");
                    return;
                case 5:
                    await DisplayAlert("저장실패", "시작 시간을 선택해 주세요.", "확인");
                    return;
                case 6:
                    await DisplayAlert("저장실패", "시작 분을 선택해 주세요.", "확인");
                    return;
                case 7:
                    await DisplayAlert("저장실패", "종료 시간을 선택해 주세요.", "확인");
                    return;
                case 8:
                    await DisplayAlert("저장실패", "종료 분을 선택해 주세요.", "확인");
                    return;
                case 9:
                    await DisplayAlert("저장실패", "종료 시간이 더 빠릅니다.", "확인");
                    return;
            }


            //제목이 없으면
            if(title.Text is null)
            {
                title.Text = AppResources.Notitle;
            }
            //내용이 없으면
            if (contents.Text is null)
            {
                contents.Text = AppResources.Nocontent;
            }

            int? year=null;
            int? month=null;
            int? day=null;

            if(!(AddViewModel.SelectedYear is null))
            {
                year = int.Parse(AddViewModel.SelectedYear);
            }
            if(!(AddViewModel.SelectedMonth is null))
            {
                month = int.Parse(AddViewModel.SelectedMonth);
            }
            if(!(AddViewModel.SelectedDay is null))
            {
                day = int.Parse(AddViewModel.SelectedDay);
            }
            

            foreach (var item in dayofWeeks)
            {
                var schedule = new Schedule
                {
                    ScheduleColor = AddViewModel.SelectedCurrentBackgroundColor.ToHex(),
                    ScheduleTitle=title.Text,
                    ScheduleContents=contents.Text,
                    IsIterative=IterativeCheckbox.IsChecked,
                    Year=year,
                    Month=month,
                    Day=day,
                    ScheuldeWeekdate=item,
                    ScheduleStartHour=int.Parse(AddViewModel.SelectedStartHour),
                    ScheduleStartMinute=int.Parse(AddViewModel.SelectedStartMinute),
                    ScheduleEndHour=int.Parse(AddViewModel.SelectedEndHour),
                    ScheduleEndMinute=int.Parse(AddViewModel.SelectedEndMinute)
                };

                await _database.AddScheduleAsync(schedule);
                await Navigation.PopModalAsync(false);
            }

        }

        private async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void OnUpdateButtonClicked(object sender, EventArgs e)
        {

            switch (IsValidate())
            {
                case 1:
                    await DisplayAlert("저장실패", "요일을 선택해 주세요.", "확인");
                    return;
                case 2:
                    await DisplayAlert("저장실패", "저장할 연도를 선택해 주세요.", "확인");
                    return;
                case 3:
                    await DisplayAlert("저장실패", "저장할 월을 선택해 주세요.", "확인");
                    return;
                case 4:
                    await DisplayAlert("저장실패", "저장할 일을 선택해 주세요.", "확인");
                    return;
                case 5:
                    await DisplayAlert("저장실패", "시작 시간을 선택해 주세요.", "확인");
                    return;
                case 6:
                    await DisplayAlert("저장실패", "시작 분을 선택해 주세요.", "확인");
                    return;
                case 7:
                    await DisplayAlert("저장실패", "종료 시간을 선택해 주세요.", "확인");
                    return;
                case 8:
                    await DisplayAlert("저장실패", "종료 분을 선택해 주세요.", "확인");
                    return;
                case 9:
                    await DisplayAlert("저장실패", "종료 시간이 더 빠릅니다.", "확인");
                    return;
            }



            updateSchedule.ScheduleColor = AddViewModel.SelectedCurrentBackgroundColor.ToHex();
            updateSchedule.ScheduleTitle = AddViewModel.ScheduleTitle;
            updateSchedule.ScheduleContents = AddViewModel.ScheduleContents;
            updateSchedule.IsIterative = IterativeCheckbox.IsChecked;


            int? year = null;
            int? month = null;
            int? day = null;

            if (!(AddViewModel.SelectedYear is null))
            {
                year = int.Parse(AddViewModel.SelectedYear);
            }
            if (!(AddViewModel.SelectedMonth is null))
            {
                month = int.Parse(AddViewModel.SelectedMonth);
            }
            if (!(AddViewModel.SelectedDay is null))
            {
                day = int.Parse(AddViewModel.SelectedDay);
            }


            updateSchedule.Year = year;
            updateSchedule.Month = month;
            updateSchedule.Day = day;
            updateSchedule.ScheuldeWeekdate = radioDay;
            updateSchedule.ScheduleStartHour = int.Parse(AddViewModel.SelectedStartHour);
            updateSchedule.ScheduleStartMinute = int.Parse(AddViewModel.SelectedStartMinute);
            updateSchedule.ScheduleEndHour = int.Parse(AddViewModel.SelectedEndHour);
            updateSchedule.ScheduleEndMinute = int.Parse(AddViewModel.SelectedEndMinute);

            await _database.UpdateScheduleAsync(updateSchedule);
            await Navigation.PopModalAsync(false);

        }

        private async void OnRemoveButtonClicked(object sender, EventArgs e)
        {
            await _database.DeleteScheduleAsync(updateSchedule);
            await Navigation.PopModalAsync(false);
        }

    }
}