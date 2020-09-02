using EasyTimeTable.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace EasyTimeTable.ViewModels
{
    public class SelectTimesViewModel
    {
        private readonly IList<Times> source;

        public ObservableCollection<Times> TimesList { get; private set; }

        public SelectTimesViewModel()
        {
            source = new List<Times>();
            CreateTimesCollection();
        }

        void CreateTimesCollection()
        {
            for(var i = 0; i < 12; i++)
            {
                source.Add(new Times
                {
                    DisplayTime = string.Format("{0:00}", i)
                });
            }

            TimesList = new ObservableCollection<Times>(source);
        }

    }
}
