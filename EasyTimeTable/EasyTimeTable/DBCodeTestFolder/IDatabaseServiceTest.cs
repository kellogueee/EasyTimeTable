using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyTimeTable.DBCodeTestFolder
{
    public interface IDatabaseServiceTest
    {
        public Task<List<IterativeScheduleTest>> GetAllIterativeSchedules();
        public Task<List<SingleScheduleTest>> GetAllSingleSchedules();
        public Task<List<CanceledIterativeScheduleTest>> GetCanceledIterativeSchedules(int scheduleID);
        public Task AddSingleSchedule(SingleScheduleTest single);
        public Task AddIterativeSchedule(IterativeScheduleTest iterativeSchedule);
        public Task AddCanceledIterativeSchedule(CanceledIterativeScheduleTest canceledIterativeSchedule);

    }
}
