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

    //리펙토링 시작.
    //1. 변수 이름 정리해서 다시 싹다 바꾸기.
    //2. 중복되는 메서드들 정리해서 새로운 함수로 만들기.
    //3. 데이터 통합 후 데이터베이스 인터페이스 만들기.

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SetTimeTablePage : ContentPage
    {

        private readonly ISetTimeTablePageViewModel _setTimeTable;



        public SetTimeTablePage(string[] idx)
        {
            InitializeComponent();

            //현재 이 페이지에 선택을 해야하는 오브젝트 총 8개.
            //1. 색깔
            //2. 요일
            //3. 시작 오전오후
            //4. 시작 시
            //5. 시작 분
            //6. 종료 오전오후
            //7. 종료 시
            //8. 종료 분
            //선택의 의미가 있는 오브젝트는 모두 접두어 Select를 붙여서 통일
            //접미어는 해당 오브젝트 구현 오브젝트.

            //SelectColorCollectionView
            //SelectDateCollectionView

            //SelectStartAMPMCollectionView
            //SelectStartHourCollectionView
            //SelectStartMinuteSlider

            //SelectEndAMPMCollectionView
            //SelectEndHourCollectionView
            //SelectEndMinuteSlider


            //각 오브젝트별로 공통적으로 사용되는 메서드가 있으므로 인터페이스를 상속받도록 하자.
            //최종적으로 데이터를 모두 조합해야하므로 합쳐놓은 인터페이스와 클래스를 만들도록 하자.

            ColorCollectionView.BindingContext = new SelectedColorViewModel();
            DateSelectCollectionView.BindingContext = new SelectDatesViewModel();

            StartTimeFirstRow.BindingContext = new SelectTimesViewModel();
            EndTimeFirstRow.BindingContext = new SelectTimesViewModel();

            StartAMPM.BindingContext = new AMPMViewModel();
            EndAMPM.BindingContext = new AMPMViewModel();





        }

        private void OnStartTimeFirstRowChanged(object sender, SelectionChangedEventArgs e)
        {
            ObservableCollection<Times> itemsource = new ObservableCollection<Times>();
            foreach (var item in StartTimeFirstRow.ItemsSource)
            {
                var temp = (Times)item;
                temp.RectangleBackgroundDefaultColor = Color.White;
                itemsource.Add(temp);

            }
            var selectedTime = (Times)(e.CurrentSelection.FirstOrDefault());
            itemsource.Where(x => x.DisplayTime == selectedTime.DisplayTime).FirstOrDefault().RectangleBackgroundDefaultColor = currentColor.BackgroundColor;
            StartTimeFirstRow.ItemsSource = itemsource;
        }

        private void OnEndTimeFirstRowChanged(object sender, SelectionChangedEventArgs e)
        {
            ObservableCollection<Times> itemsource = new ObservableCollection<Times>();
            foreach (var item in EndTimeFirstRow.ItemsSource)
            {
                var temp = (Times)item;
                temp.RectangleBackgroundDefaultColor = Color.White;
                itemsource.Add(temp);

            }
            var selectedTime = (Times)(e.CurrentSelection.FirstOrDefault());
            itemsource.Where(x => x.DisplayTime == selectedTime.DisplayTime).FirstOrDefault().RectangleBackgroundDefaultColor = currentColor.BackgroundColor;
            EndTimeFirstRow.ItemsSource = itemsource;
        }

        void OnSelectionDateChanged(object sender, SelectionChangedEventArgs e)
        {

            ObservableCollection<WeekDate> itemsource=new ObservableCollection<WeekDate>();
            foreach (var item in DateSelectCollectionView.ItemsSource)
            {
                var temp = (WeekDate)item;
                temp.RectangleBackgroundDefaultColor = Color.White;
                itemsource.Add(temp);

            }
            var selecedDate = (WeekDate)(e.CurrentSelection.FirstOrDefault());
            itemsource.Where(x => x.DateName == selecedDate.DateName).FirstOrDefault().RectangleBackgroundDefaultColor = currentColor.BackgroundColor;

            DateSelectCollectionView.ItemsSource = itemsource;
        }

        void OnColorSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedColor = (ColorSelect)(e.CurrentSelection.FirstOrDefault());
            currentColor.BackgroundColor = selectedColor.SelectionColor;

            ObservableCollection<WeekDate> itemsource_WeekDate = new ObservableCollection<WeekDate>();

            foreach (var item in DateSelectCollectionView.ItemsSource)
            {
                var temp = (WeekDate)item;
                if (temp.RectangleBackgroundDefaultColor != Color.White)
                {
                    temp.RectangleBackgroundDefaultColor = currentColor.BackgroundColor;
                }
                itemsource_WeekDate.Add(temp);
            }

            DateSelectCollectionView.ItemsSource = itemsource_WeekDate;


            ObservableCollection<AMPM> itemsource_StartAMPM = new ObservableCollection<AMPM>();
            foreach (var item in StartAMPM.ItemsSource)
            {
                var temp = (AMPM)item;
                if (temp.RectangleBackgroundDefaultColor != Color.White)
                {
                    temp.RectangleBackgroundDefaultColor = currentColor.BackgroundColor;
                }
                itemsource_StartAMPM.Add(temp);
            }

            StartAMPM.ItemsSource = itemsource_StartAMPM;


            ObservableCollection<AMPM> itemsource_EndAMPM = new ObservableCollection<AMPM>();
            foreach (var item in EndAMPM.ItemsSource)
            {
                var temp = (AMPM)item;
                if (temp.RectangleBackgroundDefaultColor != Color.White)
                {
                    temp.RectangleBackgroundDefaultColor = currentColor.BackgroundColor;
                }
                itemsource_EndAMPM.Add(temp);
            }


            EndAMPM.ItemsSource = itemsource_EndAMPM;


            ObservableCollection<Times> itemsource_StartTime = new ObservableCollection<Times>();
            foreach (var item in StartTimeFirstRow.ItemsSource)
            {
                var temp = (Times)item;
                if (temp.RectangleBackgroundDefaultColor != Color.White)
                {
                    temp.RectangleBackgroundDefaultColor = currentColor.BackgroundColor;
                }
                itemsource_StartTime.Add(temp);

            }

            StartTimeFirstRow.ItemsSource = itemsource_StartTime;



            ObservableCollection<Times> itemsource_EndTime = new ObservableCollection<Times>();
            foreach (var item in EndTimeFirstRow.ItemsSource)
            {
                var temp = (Times)item;
                if (temp.RectangleBackgroundDefaultColor != Color.White)
                {
                    temp.RectangleBackgroundDefaultColor = currentColor.BackgroundColor;
                }
                itemsource_EndTime.Add(temp);

            }

            EndTimeFirstRow.ItemsSource = itemsource_EndTime;

        }

        void OnStartAMPMSelected(object sender, SelectionChangedEventArgs e)
        {
            ObservableCollection<AMPM> itemsource = new ObservableCollection<AMPM>();


            foreach (var item in StartAMPM.ItemsSource)
            {
                var temp = (AMPM)item;
                temp.RectangleBackgroundDefaultColor = Color.White;
                itemsource.Add(temp);
            }

            var selectedAMPM = (AMPM)(e.CurrentSelection.FirstOrDefault());
            itemsource.Where(x => x.AMorPM == selectedAMPM.AMorPM).FirstOrDefault().RectangleBackgroundDefaultColor = currentColor.BackgroundColor;

            StartAMPM.ItemsSource = itemsource;


            ObservableCollection<Times> itemsource_Times = new ObservableCollection<Times>();

            if (selectedAMPM.AMorPM == "오전")
            {
                for (var i = 0; i < 12; i++)
                {
                    itemsource_Times.Add(new Times
                    {
                        DisplayTime = string.Format("{0:00}", i)
                    });
                }
            }
            else
            {
                for (var i = 12; i < 24; i++)
                {
                    itemsource_Times.Add(new Times
                    {
                        DisplayTime = string.Format("{0:00}", i)
                    });
                }
            }

            StartTimeFirstRow.ItemsSource = itemsource_Times;

        }

        void OnEndAMPMSelected(object sender, SelectionChangedEventArgs e)
        {
            ObservableCollection<AMPM> itemsource = new ObservableCollection<AMPM>();
            foreach (var item in EndAMPM.ItemsSource)
            {
                var temp = (AMPM)item;
                temp.RectangleBackgroundDefaultColor = Color.White;
                itemsource.Add(temp);
            }

            var selectedAMPM = (AMPM)(e.CurrentSelection.FirstOrDefault());
            itemsource.Where(x => x.AMorPM == selectedAMPM.AMorPM).FirstOrDefault().RectangleBackgroundDefaultColor = currentColor.BackgroundColor;

            EndAMPM.ItemsSource = itemsource;


            ObservableCollection<Times> itemsource_Times = new ObservableCollection<Times>();

            if (selectedAMPM.AMorPM == "오전")
            {
                for (var i = 0; i < 12; i++)
                {
                    itemsource_Times.Add(new Times
                    {
                        DisplayTime = string.Format("{0:00}", i)
                    });
                }
            }
            else
            {
                for (var i = 12; i < 24; i++)
                {
                    itemsource_Times.Add(new Times
                    {
                        DisplayTime = string.Format("{0:00}", i)
                    });
                }
            }

            EndTimeFirstRow.ItemsSource = itemsource_Times;
        }


        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

    }
}