using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EasyTimeTable.ViewModels
{
    public class AddScheduleViewModel : BaseViewModel
    {
        Color selectedCurrentBackgroudColor = Color.LightGray;

        private IList<string> yearSource;
        private IList<string> monthSource;
        private IList<string> daySource;
        private IList<string> hourSource;
        private IList<string> minuteSource;

        string selectedYear;
        string selectedMonth;
        string selectedDay;
        string selectedStartHour;
        string selectedStartMinute;
        string selectedEndHour;
        string selectedEndMinute;
        string scheduleTitle;
        string scheduleContents;


        public ObservableCollection<string> Years { get; set; }
        public ObservableCollection<string> Months { get; set; }
        public ObservableCollection<string> Days { get; set; }
        public ObservableCollection<string> StartHours { get; set; }
        public ObservableCollection<string> StartMinutes { get; set; }
        public ObservableCollection<string> EndHours { get; set; }
        public ObservableCollection<string> EndMinutes { get; set; }



        public string ScheduleTitle
        {
            get
            {
                return scheduleTitle;
            }
            set
            {
                SetProperty(ref scheduleTitle, value);
            }
        }

        public string ScheduleContents
        {
            get
            {
                return scheduleContents;
            }
            set
            {
                SetProperty(ref scheduleContents, value);
            }
        }

        public string SelectedYear
        {
            get
            {
                return selectedYear;
            }
            set
            {
                SetProperty(ref selectedYear, value);
            }
        }

        public string SelectedMonth
        {
            get
            {
                return selectedMonth;
            }
            set
            {
                SetProperty(ref selectedMonth, value);
            }
        }

        public string SelectedDay
        {
            get
            {
                return selectedDay;
            }
            set
            {
                SetProperty(ref selectedDay, value);
            }
        }

        public string SelectedStartHour
        {
            get
            {
                return selectedStartHour;
            }
            set
            {
                SetProperty(ref selectedStartHour, value);
            }
        }

        public string SelectedStartMinute
        {
            get
            {
                return selectedStartMinute;
            }
            set
            {
                SetProperty(ref selectedStartMinute, value);
            }
        }

        public string SelectedEndHour
        {
            get
            {
                return selectedEndHour;
            }
            set
            {
                SetProperty(ref selectedEndHour, value);
            }
        }

        public string SelectedEndMinute
        {
            get
            {
                return selectedEndMinute;
            }
            set
            {
                SetProperty(ref selectedEndMinute, value);
            }
        }

        public AddScheduleViewModel()
        {

            yearSource = new List<string>();
            monthSource = new List<string>();
            daySource = new List<string>();
            hourSource = new List<string>();
            minuteSource = new List<string>();
            CreateYear();
            CreateMonth();
            CreateDay();
            CreateHour();
            CreateMinute();
        }

        void CreateYear()
        {
            var year = DateTime.Today.Year;
            for (var i = 0; i < 10; i++)
            {
                yearSource.Add(year.ToString());
                year++;
            }
            Years = new ObservableCollection<string>(yearSource);

        }

        void CreateMonth()
        {
            for (var month = 1; month < 13; month++)
            {
                monthSource.Add(month.ToString());
            }
            Months = new ObservableCollection<string>(monthSource);
        }

        void CreateDay()
        {
            for (var day = 1; day < 32; day++)
            {
                daySource.Add(day.ToString());
            }
            Days = new ObservableCollection<string>(daySource);
        }

        void CreateHour()
        {
            for (var hour = 0; hour < 24; hour++)
            {
                hourSource.Add(String.Format("{0:00}", hour));
            }
            StartHours = new ObservableCollection<string>(hourSource);
            EndHours = new ObservableCollection<string>(hourSource);
        }

        void CreateMinute()
        {
            for (var minute = 0; minute < 60; minute++)
            {
                minuteSource.Add(String.Format("{0:00}", minute));
            }

            StartMinutes = new ObservableCollection<string>(minuteSource);
            EndMinutes = new ObservableCollection<string>(minuteSource);
        }


        public Color SelectedCurrentBackgroundColor
        {
            set
            {
                SetProperty(ref selectedCurrentBackgroudColor, value);

            }
            get
            {
                return selectedCurrentBackgroudColor;
            }
        }


        bool SetProperty<T>(ref T storage,T value,[CallerMemberName]string propertyName = null)
        {
            if (object.Equals(storage, value))
            {
                return false;
            }
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }



    }
}
