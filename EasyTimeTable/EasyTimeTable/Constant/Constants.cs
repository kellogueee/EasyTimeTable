using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyTimeTable.Constant
{
    public class Constants
    {
        public static string[] colorArray  = new string[] { "#ff837f", "#89a5ea", "#a5ea89", "#ffcb6b", "#e96ec2", "#5dc2c4", "#cbde8c" };
        public static string[] dateNameArray  = new string[] { "월", "화", "수", "목", "금", "토", "일" };
        public static string[] DayNight = new string[] { "오전", "오후" };
        public static string[] WeekDayEnd = new string[] { "주간", "주말" };


        public static string[] dayofWeek = new string[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };

        private static int[] Days31 = new int[] { 1, 3, 5, 7, 8, 10, 12 };
        private static int[] Days30 = new int[] { 4, 6, 9, 11 };

        public static int NumofdaysInMonth(int year, int month)
        {
            if (Days31.Contains(month))
            {
                return 31;
            }
            else if (Days30.Contains(month))
            {
                return 30;
            }

            return GetNumofdayFebruary(year);
        }

        public static int GetNumofdayFebruary(int year)
        {

            if (year % 4 == 0)
            {
                if (year % 400 == 0)
                {
                    return 29;
                }
            }
            return 28;
        }


        public static int GetDayOfWeekOnTheMonthOfTheYear(int year, int month, int day)
        {
            int centries = year / 100;
            int notCentriesYear = year % 100;
            month -= 2;
            if (month - 2 <= 0)
            {
                month += 12;
                notCentriesYear--;
            }

            int date = (day + GetGaussianInteger((2.6) * month - 0.2) - (2 * centries) + notCentriesYear + (centries / 4) + (notCentriesYear / 4)) % 7;

            return date;
        }

        private static int GetGaussianInteger(double val)
        {
            int result = (int)(Math.Truncate(val));
            if (result < 0)
            {
                result--;
            }

            return result;
        }

    }
}
