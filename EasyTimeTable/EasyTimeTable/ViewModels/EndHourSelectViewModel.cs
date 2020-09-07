using EasyTimeTable.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace EasyTimeTable.ViewModels
{
    public class EndHourSelectViewModel:BaseViewModel
    {
        private readonly IList<DisplayHourModel> source;

        public ObservableCollection<DisplayHourModel> SetofEndHour { get; private set; }

        DisplayHourModel preSelectedEndHour;

        public DisplayHourModel PreSelectedEndHour
        {
            get
            {
                return preSelectedEndHour;
            }

            set
            {
                if (preSelectedEndHour != value)
                {
                    preSelectedEndHour = value;
                }
            }
        }

        public EndHourSelectViewModel()
        {
            source = new List<DisplayHourModel>();
            CreateSetofEndTime();
            Random rnd = new Random();
            preSelectedEndHour = SetofEndHour.Skip(rnd.Next(0, 6)).FirstOrDefault();
           
        }


        void CreateSetofEndTime()
        {
            for (var i = 0; i < 24; i++)
            {
                source.Add(new DisplayHourModel
                {
                    DisplayHour = string.Format("{0:00}", i)
                });
            }

            SetofEndHour = new ObservableCollection<DisplayHourModel>(source);
        }
    }
}
