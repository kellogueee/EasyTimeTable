using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTimeTable
{

    public class TimetablePageMasterMenuItem
    {
        public TimetablePageMasterMenuItem()
        {
            TargetType = typeof(TimetablePageMasterMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}