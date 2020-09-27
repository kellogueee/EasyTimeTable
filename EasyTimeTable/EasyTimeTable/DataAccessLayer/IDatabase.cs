using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyTimeTable.DataAccessLayer
{
    public interface IDatabase<T>
    {
        public Task<List<T>> GetAllScheduleAsync();
        public Task<List<T>> GetAllScheduleByWeekdate(int date);
        public Task AddSchedule(T schedule);
        public Task UpdateSchedule(T schedule);
        public Task DeleteSchedule(T schedule);
        public Task DeleteAllSchedule();
    }
}
