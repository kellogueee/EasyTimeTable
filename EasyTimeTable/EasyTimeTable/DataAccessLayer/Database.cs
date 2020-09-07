using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyTimeTable.Constants;

namespace EasyTimeTable.DataAccessLayer
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;


        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ScheduleTimetable>().Wait();
        }

        public Task<List<ScheduleTimetable>> GetAllSchedule()
        {
            return _database.Table<ScheduleTimetable>().ToListAsync();
        }

        public Task<List<ScheduleTimetable>> GetAllScheduleByWeekdate(int date)
        {
            return _database.Table<ScheduleTimetable>().Where(x=>x.WeekDate==date).ToListAsync();
        }

        public Task AddSchedule(ScheduleTimetable schedule)
        {
            return _database.InsertAsync(schedule);
        }

        public Task UpdateSchedule(ScheduleTimetable schedule)
        {
            return _database.UpdateAsync(schedule);
        }

        public Task DeleteSchedule(ScheduleTimetable schedule)
        {
            return _database.DeleteAsync(schedule);
        }

        public Task DeleteAllSchedule()
        {
            return _database.DeleteAllAsync<ScheduleTimetable>();
        }
    }
}
