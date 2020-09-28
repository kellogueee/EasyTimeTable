using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SQLite;


namespace EasyTimeTable.DBCodeTestFolder
{
    public class SQLiteDatabaseTest
    {
        SQLiteDatabaseServiceTest databaseService;
        public SQLiteDatabaseServiceTest SQLiteDatabaseService
        {
            get
            {
                if (databaseService == null)
                {
                    databaseService = new SQLiteDatabaseServiceTest(Path
                        .Combine(Environment
                        .GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "table.db3"));
                }
                return databaseService;
            }
        }

       

    }
}
