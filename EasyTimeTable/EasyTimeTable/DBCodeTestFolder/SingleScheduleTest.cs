using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTimeTable.DBCodeTestFolder
{
    public class SingleScheduleTest
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string SelectedColor { get; set; }
        public string ScheduleTitle { get; set; }
        public int WeekDate { get; set; }
        public int StartHour { get; set; }
        public int StartMinute { get; set; }
        public int EndHour { get; set; }
        public int EndMinute { get; set; }
        public bool IsIterativeSchedule { get; set; } = false;
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
    }
}
