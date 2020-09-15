using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasyTimeTable.DataAccessLayer
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
                    database = new SQLiteDatabase(Path
                        .Combine(Environment
                        .GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "table.db3"));
                }
                return database;
            }
        }
    }
}
