using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasyTimeTable.DatabaseLayer
{
    public class DatabaseService
    {
        SQLiteDatabase database;

        public SQLiteDatabase SQLiteDatabase
        {
            get
            {
                if (database == null)
                {
                    database=new SQLiteDatabase(Path
                        .Combine(Environment
                        .GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "timetable.db3"));
                }
                return database;
            }
        }
    }
}
