using EasyTimeTable.DataAccessLayer;
using EasyTimeTable.Models;
using EasyTimeTable.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EasyTimeTable
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTimetableSchedulePage : ContentPage
    {


        private readonly IDatabase<ScheduleTimetable> _database;

        static int startHourScrollCenter = 0;
        static int endHourScrollCenter = 0;

        private readonly ColorSelectViewModel _colorvm;
        private readonly DateSelectViewModel _datevm;
        private readonly ScheduleTitleViewModel _titlevm;
        private readonly StartHourSelectViewModel _startHvm;
        private readonly StartMinuteViewModel _startMvm;
        private readonly EndHourSelectViewModel _endHvm;
        private readonly EndMinuteViewModel _endMvm;

        public AddTimetableSchedulePage()
        {
            InitializeComponent();
            _database = new DatabaseService().SQLiteDatabase;

            _colorvm = new ColorSelectViewModel();
            _titlevm = new ScheduleTitleViewModel();
            _datevm = new DateSelectViewModel();
            _startHvm = new StartHourSelectViewModel();
            _startMvm = new StartMinuteViewModel();
            _endHvm = new EndHourSelectViewModel();
            _endMvm = new EndMinuteViewModel();

            ColorSelectCollectionView.BindingContext = _colorvm;
            ScheduleTitle.BindingContext = _titlevm;
            DateSelectCollectionView.BindingContext = _datevm;
            StartHourSelctCollectionView.BindingContext = _startHvm;
            StartMinuteSilder.BindingContext = _startMvm;
            EndHourSelctCollectionView.BindingContext = _endHvm;
            EndMinuteSlider.BindingContext = _endMvm;

        }
        public AddTimetableSchedulePage(int starthour, int date)
        {
            InitializeComponent();
            _database = new DatabaseService().SQLiteDatabase;

            _colorvm = new ColorSelectViewModel();
            _titlevm = new ScheduleTitleViewModel();
            _datevm = new DateSelectViewModel(date);
            _startHvm = new StartHourSelectViewModel(starthour);
            _startMvm = new StartMinuteViewModel();
            _endHvm = new EndHourSelectViewModel();
            _endMvm = new EndMinuteViewModel();

            ColorSelectCollectionView.BindingContext = _colorvm;
            ScheduleTitle.BindingContext = _titlevm;
            DateSelectCollectionView.BindingContext = _datevm;
            StartHourSelctCollectionView.BindingContext = _startHvm;
            StartMinuteSilder.BindingContext = _startMvm;
            EndHourSelctCollectionView.BindingContext = _endHvm;
            EndMinuteSlider.BindingContext = _endMvm;

        }

        public AddTimetableSchedulePage(ScheduleTimetable schedule)
        {
            InitializeComponent();
            _database = new DatabaseService().SQLiteDatabase;

            _colorvm = new ColorSelectViewModel(schedule.SelectedColor);
            _titlevm = new ScheduleTitleViewModel(schedule.ScheduleTitle);
            _datevm = new DateSelectViewModel(schedule.WeekDate);
            _startHvm = new StartHourSelectViewModel(schedule.StartHour);
            _startMvm = new StartMinuteViewModel(schedule.StartMinute);
            _endHvm = new EndHourSelectViewModel(schedule.EndHour);
            _endMvm = new EndMinuteViewModel(schedule.EndMinute);

            ColorSelectCollectionView.BindingContext = _colorvm;
            ScheduleTitle.BindingContext = _titlevm;
            DateSelectCollectionView.BindingContext = _datevm;
            StartHourSelctCollectionView.BindingContext = _startHvm;
            StartMinuteSilder.BindingContext = _startMvm;
            EndHourSelctCollectionView.BindingContext = _endHvm;
            EndMinuteSlider.BindingContext = _endMvm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
        }

        private void OnColorSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var color = (ColorModel)(e.CurrentSelection.FirstOrDefault());
            currentSelectedColor.BackgroundColor = color.ColortoSelect;
            try
            {
                ChangeCurrentSelectedColor(color.ColortoSelect);

            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        } 
        private void ChangeCurrentSelectedColor(Color ColortoChange)
        {

            //DateCollectionChange
            var DateList= DateSelectCollectionView.ItemsSource;
            foreach (var temp in DateList)
            {
                var item = (WeekdatesModel)temp;
                if (item.RectangleBackgroundDefaultColor != Color.White)
                {
                    item.RectangleBackgroundDefaultColor = ColortoChange;
                }
            }
            DateSelectCollectionView.ItemsSource = null;
            DateSelectCollectionView.ItemsSource = DateList;


            //StartHourCollectionChange
            var StartHourList = StartHourSelctCollectionView.ItemsSource;
            foreach (var temp in StartHourList)
            {
                var item = (DisplayHourModel)temp;
                if (item.RectangleBackgroundDefaultColor != Color.White)
                {
                    item.RectangleBackgroundDefaultColor = ColortoChange;
                }
            }
            StartHourSelctCollectionView.ItemsSource = null;
            StartHourSelctCollectionView.ItemsSource = StartHourList;
            StartHourSelctCollectionView.ScrollTo(startHourScrollCenter, position: ScrollToPosition.Center, animate: false);


            //EndHourCollectionChange
            var EndHourList = EndHourSelctCollectionView.ItemsSource;
            foreach (var temp in EndHourList)
            {
                var item = (DisplayHourModel)temp;
                if (item.RectangleBackgroundDefaultColor != Color.White)
                {
                    item.RectangleBackgroundDefaultColor = ColortoChange;
                }
            }
            EndHourSelctCollectionView.ItemsSource = null;
            EndHourSelctCollectionView.ItemsSource = EndHourList;
            EndHourSelctCollectionView.ScrollTo(endHourScrollCenter, position: ScrollToPosition.Center, animate: false);

        }
        
        private void OnWeekdateSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var CollectionList = DateSelectCollectionView.ItemsSource;
            var selectedItem = (WeekdatesModel)(e.CurrentSelection.FirstOrDefault());

            foreach (var temp in CollectionList)
            {
                var item = (WeekdatesModel)temp;
                if (item.DateName == selectedItem.DateName)
                {
                    item.RectangleBackgroundDefaultColor = currentSelectedColor.BackgroundColor;
                }
                else
                {
                    item.RectangleBackgroundDefaultColor = Color.White;
                }
            }

            DateSelectCollectionView.ItemsSource = null;
            DateSelectCollectionView.ItemsSource = CollectionList;
        }

        private void OnStartHourSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //try  catch 문으로 작성해야합니데이.
            var CollectionList = _startHvm.SetofStartHour;
            var selectedItem = (DisplayHourModel)(e.CurrentSelection.FirstOrDefault());

            foreach (var temp in CollectionList)
            {
                var item = (DisplayHourModel)temp;
                if (item.DisplayHour == selectedItem.DisplayHour)
                {
                    item.RectangleBackgroundDefaultColor = currentSelectedColor.BackgroundColor;
                }
                else
                {
                    item.RectangleBackgroundDefaultColor = Color.White;
                }
            }

            StartHourSelctCollectionView.ItemsSource = null;
            StartHourSelctCollectionView.ItemsSource = CollectionList;
            StartHourSelctCollectionView.ScrollTo(startHourScrollCenter, position: ScrollToPosition.Center, animate: false);
        }

        private void OnStartHourSelectCollectionViewScrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            startHourScrollCenter=e.CenterItemIndex;
        }

        private void OnEndHourSelectCollectionViewScrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            endHourScrollCenter = e.CenterItemIndex;
        }

        private void OnEndHourSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var CollectionList = EndHourSelctCollectionView.ItemsSource;
            var selectedItem = (DisplayHourModel)(e.CurrentSelection.FirstOrDefault());

            foreach (var temp in CollectionList)
            {
                var item = (DisplayHourModel)temp;
                if (item.DisplayHour == selectedItem.DisplayHour)
                {
                    item.RectangleBackgroundDefaultColor = currentSelectedColor.BackgroundColor;
                }
                else
                {
                    item.RectangleBackgroundDefaultColor = Color.White;
                }
            }

            EndHourSelctCollectionView.ItemsSource = null;
            EndHourSelctCollectionView.ItemsSource = CollectionList;
            EndHourSelctCollectionView.ScrollTo(endHourScrollCenter, position: ScrollToPosition.Center, animate: false);
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var color = ((ColorModel)ColorSelectCollectionView.SelectedItem).ColortoSelect.ToHex();

            WeekdatesModel date;
            try
            {
                date = ((WeekdatesModel)DateSelectCollectionView.SelectedItem);
            }
            catch(Exception ex)
            {
                await DisplayAlert("요일 선택", "요일을 선택해주세요.", "OK");
                Debug.WriteLine(ex.Message);
                return;
            }

            var title = ScheduleTitle.Text;

            int startHour=0;
            try
            {
                startHour =int.Parse(((DisplayHourModel)StartHourSelctCollectionView.SelectedItem).DisplayHour);
            }
            catch(Exception ex)
            {
                await DisplayAlert("시간 선택", "시간을 선택해주세요.", "OK");
                Debug.WriteLine(ex.Message);
                return;
            }
            
            var startMin = (int)StartMinuteSilder.Value;

            int endHour = 0;
            try
            {
                endHour = int.Parse(((DisplayHourModel)EndHourSelctCollectionView.SelectedItem).DisplayHour);
            }
            catch(Exception ex)
            {
                await DisplayAlert("시간 선택", "시간을 선택해주세요.", "OK");
                Debug.WriteLine(ex.Message);
                return;
            }
            
            var endMin = (int)EndMinuteSlider.Value;

            try
            {
                await _database.AddSchedule(new ScheduleTimetable
                {
                    SelectedColor = color,
                    WeekDate = GetWeekdateValue(date),
                    ScheduleTitle = title,
                    StartHour = startHour,
                    StartMinute = startMin,
                    EndHour = endHour,
                    EndMinute = endMin
                });
            }
            catch (Exception ex)
            {
                await DisplayAlert("저장 실패", "저장에 실패했습니다. 개발자에게 문의하세요.", "OK");
                Debug.WriteLine(ex.Message);
                return;
            }

            await Navigation.PopModalAsync();
           

        }

        private int GetWeekdateValue(WeekdatesModel date)
        {
            var vm = new DateSelectViewModel();

            var temp = vm.SetofWeekdates.Select(x => x.DateName).ToList();

            return temp.IndexOf(date.DateName);
        }

    }
}