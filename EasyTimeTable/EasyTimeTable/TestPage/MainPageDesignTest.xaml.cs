using EasyTimeTable.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EasyTimeTable.TestPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageDesignTest : ContentPage
    {
        //private static bool currentDisplayIsNight = false;
        //private static bool currentDisplayIsWeekend = false;
        private static string[] DayNight = new string[] { "오전", "오후" };
        private static string[] WeekDayEnd = new string[] { "주간", "주말" };

        //이거는 먼 훗날 변경이 될 수도 있는 숫자이기 때문에 따로 빼놓는다.
        private static int TableBodyRowCount = 12;

        public MainPageDesignTest()
        {
            InitializeComponent();
            GenerateWeekdayGrid();
            GenerateWeekendGrid();
        
        }


        private void GenerateWeekdayGrid()
        {
            WeekdayGridHead();
            WeekdayGridBody();
        }
        private void GenerateWeekendGrid()
        {
            WeekendGridHead();
            WeekendGridBody();
        }

        private Grid WeekdayGridHead()
        {
            var tableHead = new Grid
            {

            };
            return tableHead;
        }

        private Grid WeekendGridHead()
        {
            var tableHead = new Grid
            {

            };
            return tableHead;
        }

        private Grid WeekdayGridBody()
        {
            var tableBody = new Grid
            {

            };
            return tableBody;
        }
        private Grid WeekendGridBody()
        {
            var tableBody = new Grid
            {

            };
            return tableBody;
        }

        private void ChangeDisplayedHour(bool IsNight)
        {

        }
        private void ChangeDisplayedTableBody(bool IsWeekend)
        {

        }

        private void OnNightDayChangeClicked(object sender, EventArgs e)
        {
            var daynightItem = (ToolbarItem)sender;
            var IsNight = currentDisplayIsNight(daynightItem.Text);
            ChangeDisplayedHour(IsNight);
        }

        private void OnWeekendDayChangeClicked(object sender, EventArgs e)
        {
            var weekendItem = (ToolbarItem)sender;
            var IsWeekend = currentDisplayIsWeekend(weekendItem.Text);
            ChangeDisplayedTableBody(IsWeekend);
        }

        private bool currentDisplayIsNight(string displayedText)
        {
            bool IsNight=false;
            if (DayNight.IndexOf(displayedText) > 1)
            {
                IsNight = true;
                return IsNight;
            }
            return IsNight;
        }

        private bool currentDisplayIsWeekend(string displayedText)
        {
            bool IsWeekend = false;
            if (WeekDayEnd.IndexOf(displayedText) > 1)
            {
                IsWeekend = true;
                return IsWeekend;
            }
            return IsWeekend;
        }
    }
}