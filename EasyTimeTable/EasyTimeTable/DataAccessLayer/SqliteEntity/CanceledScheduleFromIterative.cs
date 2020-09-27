using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace EasyTimeTable.DataAccessLayer.SqliteEntity
{
    public class CanceledScheduleFromIterative
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
    }
}
