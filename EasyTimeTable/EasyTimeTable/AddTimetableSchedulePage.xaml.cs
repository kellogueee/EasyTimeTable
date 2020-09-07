using EasyTimeTable.DataAccessLayer;
using EasyTimeTable.Models;
using EasyTimeTable.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        static int startHourScrollFirstItem=0;
        static int endHourScrollFirstItem=0;

        public AddTimetableSchedulePage()
        {
            InitializeComponent();
            ColorSelectCollectionView.BindingContext = new ColorSelectViewModel();
            DateSelectCollectionView.BindingContext = new DateSelectViewModel();
            ScheduleTitle.BindingContext = new ScheduleTitleViewModel();
            StartHourSelctCollectionView.BindingContext = new StartHourSelectViewModel();
            StartMinuteSilder.BindingContext = new StartMinuteViewModel();
            EndHourSelctCollectionView.BindingContext = new EndHourSelectViewModel();
            EndMinuteSlider.BindingContext = new EndMinuteViewModel();
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
            var CollectionList = StartHourSelctCollectionView.ItemsSource;
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
            StartHourSelctCollectionView.ScrollTo(startHourScrollFirstItem,animate:false);
        }

        private void OnStartHourSelectCollectionViewScrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            startHourScrollFirstItem=e.FirstVisibleItemIndex;
        }

        private void OnEndHourSelectCollectionViewScrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            endHourScrollFirstItem = e.FirstVisibleItemIndex;
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
            EndHourSelctCollectionView.ScrollTo(endHourScrollFirstItem, animate: false);
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
                return;
            }
            
            var endMin = (int)EndMinuteSlider.Value;

            try
            {
                await App.Database.AddSchedule(new ScheduleTimetable
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
            catch(Exception ex)
            {
                await DisplayAlert("저장 실패", "저장에 실패했습니다. 개발자에게 문의하세요.", "OK");
                return;
            }

            await Navigation.PopToRootAsync();

        }

        private int GetWeekdateValue(WeekdatesModel date)
        {
            var vm = new DateSelectViewModel();

            var temp = vm.SetofWeekdates.Select(x => x.DateName).ToList();

            return temp.IndexOf(date.DateName);
        }

    }
}