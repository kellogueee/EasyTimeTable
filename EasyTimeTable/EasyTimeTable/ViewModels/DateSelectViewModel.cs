using EasyTimeTable.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace EasyTimeTable.ViewModels
{
    public class DateSelectViewModel:BaseViewModel
    {
        private readonly IList<WeekdatesModel> source;

        WeekdatesModel preSelectedDate;

        public WeekdatesModel PreSelectedDate
        {
            get
            {
                return preSelectedDate;
            }
            set
            {
                if (preSelectedDate != value)
                {
                    preSelectedDate = value;
                }
            }
        }

        public ObservableCollection<WeekdatesModel> SetofWeekdates { get; private set; }

        public DateSelectViewModel()
        {
            source = new List<WeekdatesModel>();
            CreateSetofWeekdates();
            Random rnd = new Random();
            preSelectedDate = SetofWeekdates.Skip(rnd.Next(0, 6)).FirstOrDefault();
        }

        private void CreateSetofWeekdates()
        {
            source.Add(new WeekdatesModel
            {
                DateName = "월"
            });
            source.Add(new WeekdatesModel
            {
                DateName = "화",
            });
            source.Add(new WeekdatesModel
            {
                DateName = "수"
            });
            source.Add(new WeekdatesModel
            {
                DateName = "목"
            });
            source.Add(new WeekdatesModel
            {
                DateName = "금"
            });
            source.Add(new WeekdatesModel
            {
                DateName = "토"
            });
            source.Add(new WeekdatesModel
            {
                DateName = "일"
            });

            SetofWeekdates = new ObservableCollection<WeekdatesModel>(source);

        }
    }
}
