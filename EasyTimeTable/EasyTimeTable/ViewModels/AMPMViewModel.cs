using EasyTimeTable.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace EasyTimeTable.ViewModels
{
    public class AMPMViewModel
    {
        private readonly IList<AMPM> source;

        public ObservableCollection<AMPM> AMPMsource { get; private set; }


        public AMPMViewModel()
        {
            source = new List<AMPM>();
            CreateAMPMCollection();
        }

        void CreateAMPMCollection()
        {
            source.Add(new AMPM
            {
                AMorPM = "오전"
            });
            source.Add(new AMPM
            {
                AMorPM = "오후"
            });
            AMPMsource = new ObservableCollection<AMPM>(source);
        }
    }
}
