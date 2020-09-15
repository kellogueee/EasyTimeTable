using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTimeTable.ViewModels
{
    public class StartMinuteViewModel
    {
        public int StartMinute { get; set; }
        public StartMinuteViewModel()
        {
            
        }
        public StartMinuteViewModel(int startMinute)
        {
            StartMinute = startMinute;
        }
    }
}
