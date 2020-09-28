using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyTimeTable.Constant;
using EasyTimeTable.DataAccessLayer.SqliteEntity;

namespace EasyTimeTable.DataAccessLayer
{
    public class SQLiteDatabase:IDatabase<IterativeSchedule>
    {
        readonly SQLiteAsyncConnection _database;

        public SQLiteDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<IterativeSchedule>().Wait();
        }


        public Task<List<IterativeSchedule>> GetAllScheduleAsync()
        {
            return _database.Table<IterativeSchedule>().ToListAsync();
        }

        public Task<List<IterativeSchedule>> GetAllScheduleByWeekdate(int date)
        {
            return _database.Table<IterativeSchedule>().Where(x=>x.WeekDate==date).ToListAsync();
        }

        public Task AddSchedule(IterativeSchedule schedule)
        {
            return _database.InsertAsync(schedule);
        }

        public Task UpdateSchedule(IterativeSchedule schedule)
        {
            return _database.UpdateAsync(schedule);
        }

        public Task DeleteSchedule(IterativeSchedule schedule)
        {
            return _database.DeleteAsync(schedule);
        }



        public Task DeleteAllSchedule()
        {
            return _database.DeleteAllAsync<IterativeSchedule>();
        }
    }
}
