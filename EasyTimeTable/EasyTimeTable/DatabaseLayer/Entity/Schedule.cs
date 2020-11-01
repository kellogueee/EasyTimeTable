using System;
using System.Collections.Generic;
using System.Text;
using SQLite;


namespace EasyTimeTable.DatabaseLayer.Entity
{
    public class Schedule
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public bool IsIterative { get; set; } 
        public string ScheduleColor { get; set; }
        public string ScheduleTitle { get; set; }
        public string ScheduleContents { get; set; }
        public int ScheuldeWeekdate { get; set; }
        public int ScheduleStartHour { get; set; }
        public int ScheduleStartMinute { get; set; }
        public int ScheduleEndHour { get; set; }
        public int ScheduleEndMinute { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? Day { get; set; }


    }
}
