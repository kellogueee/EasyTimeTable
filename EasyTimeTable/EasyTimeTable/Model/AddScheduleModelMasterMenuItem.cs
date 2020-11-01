using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTimeTable.Model
{

    public class AddScheduleModelMasterMenuItem
    {
        public AddScheduleModelMasterMenuItem()
        {
            TargetType = typeof(AddScheduleModelMasterMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}