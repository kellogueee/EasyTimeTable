using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.Internals;
using EasyTimeTable.Resx;


namespace EasyTimeTable.Constants
{
    public class DateService
    {
        /// <summary>
        /// sunday가 0, monday부터 차례대로 1
        /// </summary>
        private static string[] weekdays = new string[] { "sunday", "monday","tuesday","wednesday","thursday","friday","saturday" };
        private static string[] dayofweek = new string[] { AppResources.Sunday , AppResources.Monday, AppResources.Tuesday, AppResources.Wednesday, AppResources.Thursday, AppResources.Friday, AppResources.Saturday};


        public static int GetTodayDateIndex()
        {
            var today = DateTime.Today.DayOfWeek.ToString().Split('.').LastOrDefault();
            var idx = weekdays.IndexOf(today.ToLower());
            return idx;
        }

        public static string GetTodayDayofWeek(int idx)
        {
            return dayofweek[idx];
        }
    }
}
