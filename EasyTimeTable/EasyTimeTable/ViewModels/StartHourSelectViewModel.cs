using EasyTimeTable.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace EasyTimeTable.ViewModels
{
    public class StartHourSelectViewModel:BaseViewModel
    {
        private readonly IList<DisplayHourModel> source;

        public ObservableCollection<DisplayHourModel> SetofStartHour { get; private set; }

        DisplayHourModel preSelectedStartHour;

        public DisplayHourModel PreSelectedStartHour
        {
            get
            {
                return preSelectedStartHour;
            }

            set
            {
                if (preSelectedStartHour != value)
                {
                    preSelectedStartHour = value;
                }
            }
        }

        public StartHourSelectViewModel()
        {
            source = new List<DisplayHourModel>();
            CreateSetofStartTime();
        }

        public StartHourSelectViewModel(int hour)
        {
            source = new List<DisplayHourModel>();
            CreateSetofStartTime();
            preSelectedStartHour = SetofStartHour[hour];
        }


        void CreateSetofStartTime()
        {
            for(var i = 0; i < 24; i++)
            {
                source.Add(new DisplayHourModel
                {
                    DisplayHour = string.Format("{0:00}", i)
                });
            }

            SetofStartHour = new ObservableCollection<DisplayHourModel>(source);
        }

    }
}
