using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTimeTable.ViewModels
{
    public class EndMinuteViewModel
    {
        public int EndMinute { get; set; }

        public EndMinuteViewModel()
        {
            
        }

        public EndMinuteViewModel(int endMinute)
        {
            EndMinute = endMinute;
        }
    }
}
