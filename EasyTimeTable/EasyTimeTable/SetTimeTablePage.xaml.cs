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
    public partial class SetTimeTablePage : ContentPage
    {

        public SetTimeTablePage(string[] idx)
        {
            InitializeComponent();

            //List<string> firstRow = new List<string>();
            //for(var i = 0; i < 6; i++)
            //{
            //    firstRow.Add(string.Format("{0:00}", i));
            //}

            //List<string> secondRow= new List<string>();
            //for (var i = 0; i < 6; i++)
            //{
            //    secondRow.Add(string.Format("{0:00}", i+6));
            //}

            ColorCollectionView.BindingContext = new SelectedColorViewModel();
            DateSelectCollectionView.BindingContext = new SelectDatesViewModel();

            StartTimeFirstRow.BindingContext = new SelectTimesViewModel();
            EndTimeFirstRow.BindingContext = new SelectTimesViewModel();

            StartAMPM.BindingContext = new AMPMViewModel();
            EndAMPM.BindingContext = new AMPMViewModel();

            //for (var i = 0; i < 2; i++)
            //{
            //    for(var j = 0; j < 6; j++)
            //    {
            //        var box = new BoxView()
            //        {
            //            BackgroundColor = Color.White,
            //            Margin = new Thickness(0.1),
            //        };
            //        var text = new Label()
            //        {
            //            VerticalOptions = LayoutOptions.CenterAndExpand,
            //            HorizontalOptions = LayoutOptions.CenterAndExpand
            //        };
            //        if (i == 0)
            //        {
            //            int times = 0 + j;
            //            text.Text = string.Format("{0:00}", times);
            //        }
            //        else
            //        {
            //            int times = 6 + j;
            //            text.Text = string.Format("{0:00}", times);
            //        }


            //        selectTimeTableStart.Children.Add(box, j, i);
            //        selectTimeTableStart.Children.Add(text, j, i);
            //    }
            //}

            //for (var i = 0; i < 2; i++)
            //{
            //    for (var j = 0; j < 6; j++)
            //    {
            //        var box = new BoxView()
            //        {
            //            BackgroundColor = Color.White,
            //            Margin = new Thickness(0.1),
            //        };
            //        var text = new Label()
            //        {
            //            VerticalOptions = LayoutOptions.CenterAndExpand,
            //            HorizontalOptions = LayoutOptions.CenterAndExpand
            //        };
            //        if (i == 0)
            //        {
            //            int times = 0 + j;
            //            text.Text = string.Format("{0:00}", times);
            //        }
            //        else
            //        {
            //            int times = 6 + j;
            //            text.Text = string.Format("{0:00}", times);
            //        }

            //        selectTimeTableEnd.Children.Add(box, j, i);
            //        selectTimeTableEnd.Children.Add(text, j, i);
            //    }
            //}

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



            ObservableCollection<Times> itemsource_EndAMPM = new ObservableCollection<AMPM>();
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

       
    }
}