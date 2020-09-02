using EasyTimeTable.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EasyTimeTable.ViewModels
{
    public class SelectDatesViewModel:BaseViewModel
    {
        private readonly IList<WeekDate> source;
        WeekDate selectedDate;

        public ObservableCollection<WeekDate> WeekDates { get; private set; }

        public WeekDate SelectedDate
        {
            get 
            { 
                return selectedDate; 
            }
            set
            {
                if (selectedDate != value)
                {
                    selectedDate = value;
                }
            }
        }

        public string SelectedDateMessage { get; private set; }

        public ICommand DateSelectionChangedCommand => new Command(DateSelectionChanged);

        public SelectDatesViewModel()
        {
            source = new List<WeekDate>();
            CreateWeekDateCollection();

            //selectedDate = WeekDates.Skip(3).FirstOrDefault();
            //DateSelectionChanged();
        }

        void CreateWeekDateCollection()
        {
            source.Add(new WeekDate
            {
                DateName="월"
            });
            source.Add(new WeekDate
            {
                DateName = "화",
            });
            source.Add(new WeekDate
            {
                DateName = "수"
            });
            source.Add(new WeekDate
            {
                DateName = "목"
            });
            source.Add(new WeekDate
            {
                DateName = "금"
            });
            source.Add(new WeekDate
            {
                DateName = "토"
            });
            source.Add(new WeekDate
            {
                DateName = "일"
            });

            WeekDates = new ObservableCollection<WeekDate>(source);

        }

        void DateSelectionChanged()
        {
            SelectedDateMessage = $"Selection {selectedDate.DateName}";
            OnPropertyChanged("SelectedDateMessage");
        }




    }
}
