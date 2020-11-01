using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyTimeTable.DatabaseLayer.Entity;
using SQLite;


namespace EasyTimeTable.DatabaseLayer
{
    //Xamarin은 SQLite말고 SQL Server 같은 RDBMS를 쓰려면 Web을 통해서 사용하는 것이 강력하게 추천되나보다. 따라서 데이터베이스 액세스 레이어를 따로 운용하는 것은 
    //개발 시간 낭비이다. 나중에 웹개발 할때 다시 도전해보자.

    public class SQLiteDatabase
    {
        readonly SQLiteAsyncConnection _database;
        public SQLiteDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Schedule>().Wait();
            _database.CreateTableAsync<AddedColor>().Wait();
        }

        public Task<List<AddedColor>> GetAllColorsAsync()
        {
            return _database.Table<AddedColor>().ToListAsync();
        }

        public Task AddColorAsync(AddedColor addedColor)
        {
            return _database.InsertAsync(addedColor);
        }

        public Task<List<Schedule>> GetAllScheduleAsync()
        {
            return _database.Table<Schedule>().ToListAsync();
        }

        public Task<List<Schedule>> GetAllScheduleAsyncByWeekdate(int date)
        {
            return _database.Table<Schedule>().Where(x => x.ScheuldeWeekdate == date).ToListAsync();
        }

        public Task<Schedule> GetScheduleById(int id)
        {
            return _database.Table<Schedule>().Where(x => x.ID == id).FirstOrDefaultAsync();
        }

        public Task AddScheduleAsync(Schedule schedule)
        {
            return _database.InsertAsync(schedule);
        }

        public Task UpdateScheduleAsync(Schedule schedule)
        {
            return _database.UpdateAsync(schedule);
        }

        public Task DeleteScheduleAsync(Schedule schedule)
        {
            return _database.DeleteAsync(schedule);
        }

        public Task DeleteAllScheduleAsync()
        {
            return _database.DeleteAllAsync<Schedule>();
        }

    }
}
