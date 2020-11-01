using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTimeTable.DatabaseLayer.Entity
{
    public class AddedColor
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string ColorHex { get; set; }
    }
}
