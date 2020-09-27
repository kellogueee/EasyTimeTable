using System;
using System.Collections.Generic;
using System.Text;
using SQLite;


namespace EasyTimeTable.DataAccessLayer.SqliteEntity
{
    public class ScheduleTimetable
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
        public bool IsIterativeSchedule { get; set; } = true;

    }
}
