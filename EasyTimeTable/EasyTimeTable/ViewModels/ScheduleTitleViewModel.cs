using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTimeTable.ViewModels
{
    public class ScheduleTitleViewModel
    {
        public string ScheduleTitle { get; set; }

        public ScheduleTitleViewModel()
        {
           
        }

        public ScheduleTitleViewModel(string title)
        {
            ScheduleTitle = title;
        }
    }
}
