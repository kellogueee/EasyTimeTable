using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTimeTable.DBCodeTestFolder
{
    public class CanceledIterativeScheduleTest
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }


        [ForeignKey(typeof(IterativeScheduleTest))]
        public int IterativeScheduleID { get; set; }

        [ManyToOne]
        public IterativeScheduleTest IterativeScheduleTest { get; set; }
    }
}
