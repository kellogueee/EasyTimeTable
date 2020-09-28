using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTimeTable.DBCodeTestFolder
{
    public class SQLiteDatabaseServiceTest:IDatabaseServiceTest
    {
        readonly SQLiteAsyncConnection _database;

        public SQLiteDatabaseServiceTest(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
        }

        public Task<List<IterativeScheduleTest>> GetAllIterativeSchedules()
        {
            return _database.Table<IterativeScheduleTest>().ToListAsync();
        }

        public Task<List<SingleScheduleTest>> GetAllSingleSchedules()
        {
            return _database.Table<SingleScheduleTest>().ToListAsync();
        }

        public async Task<List<CanceledIterativeScheduleTest>> GetCanceledIterativeSchedules(int scheduleID)
        {
            var CanceledIteratives =await _database.Table<CanceledIterativeScheduleTest>().ToListAsync();
            return CanceledIteratives.Where(x => x.ID == scheduleID).ToList();
        }


        public Task AddSingleSchedule(SingleScheduleTest single)
        {
            return _database.InsertAsync(single);
        }
        public Task AddIterativeSchedule(IterativeScheduleTest iterativeSchedule)
        {
            return _database.InsertAsync(iterativeSchedule);
        }
        public Task AddCanceledIterativeSchedule(CanceledIterativeScheduleTest canceledIterativeSchedule)
        {
            return _database.InsertAsync(canceledIterativeSchedule);
        }


        public Task UpdateSingleSchedule(SingleScheduleTest single)
        {
            return _database.UpdateAsync(single);
        }
        public Task UpdateIterativeSchedule(IterativeScheduleTest iterativeSchedule)
        {
            return _database.UpdateAsync(iterativeSchedule);
        }
        public Task UpdateCanceledIterativeSchedule(CanceledIterativeScheduleTest canceledIterativeSchedule)
        {
            return _database.UpdateAsync(canceledIterativeSchedule);
        }

        public Task DeleteSingleSchedule(SingleScheduleTest single)
        {
            return _database.DeleteAsync(single);
        }
        public Task DeleteIterativeSchedule(IterativeScheduleTest iterativeSchedule)
        {
            return _database.DeleteAsync(iterativeSchedule);
        }
        public Task DeleteCanceledIterativeSchedule(CanceledIterativeScheduleTest canceledIterativeSchedule)
        {
            return _database.DeleteAsync(canceledIterativeSchedule);
        }

        public async void DeleteAllSchedule()
        {
            await _database.DeleteAllAsync<SingleScheduleTest>();
            await _database.DeleteAllAsync<IterativeScheduleTest>();
            await _database.DeleteAllAsync<CanceledIterativeScheduleTest>();
        }


    }
}
