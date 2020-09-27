using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyTimeTable
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarPage : ContentPage
    {
        private static string[] weekDates = new string[] { "일", "월", "화", "수", "목", "금", "토" };
        private static int[] Days31 = new int[] { 1, 3, 5, 7, 8, 10, 12 };
        private static int[] Days30 = new int[] { 4, 6, 9, 11 };

        public CalendarPage()
        {
            InitializeComponent();
            int year = DateTime.Today.Year;
            int month = DateTime.Today.Month;
            int day = DateTime.Today.Day;
            InitializeCalendarCurrentMonth(year,month,day);

        }

        private void InitializeCalendarCurrentMonth(int year,int month,int day)
        {
            int numofDays = NumofdaysInMonth(year,month);
            int numIDX = 0;
            int startDate = GetDayOfWeekOnTheMonthOfTheYear(year, month, 1);
            int[] days = new int[numofDays];
            for(var i=0;i< days.Length; i++)
            {
                days[i] = ((startDate+i)%7);
            }


            for (var row = 1; row <= 5; row++)
            {
                for (var col = 0; col < 7; col++)
                {
                    var box = new BoxView
                    {
                        BackgroundColor = Color.White,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        Margin = new Thickness()
                    };

                    Grid.SetRow(box, row);
                    Grid.SetColumn(box, col);
                    Calender.Children.Add(box);

                    if (numIDX < numofDays)
                    {
                        if (days[numIDX] == col)
                        {
                            var label = new Label
                            {
                                VerticalOptions = LayoutOptions.Start,
                                HorizontalOptions = LayoutOptions.CenterAndExpand,
                                Text = (numIDX+1).ToString(),
                                Padding=new Thickness(0,2,0,0)
                            };
                            if (col == 6)
                            {
                                label.TextColor = Color.Blue;
                            }
                            else if (col == 0)
                            {
                                label.TextColor = Color.Red;
                            }


                            //오늘날짜는 백그라운드 색깔주기
                            if (numIDX + 1 == day&&month== DateTime.Today.Month)
                            {
                                box.BackgroundColor = Color.FromHex("#d7eaff");
                            }

                            Grid.SetRow(label, row);
                            Grid.SetColumn(label, days[numIDX]);
                            Calender.Children.Add(label);
                            numIDX++;
                        }
                    }
                }
            }
        }

        private void InitializeCalendar(int year, int month)
        {
            int numofDays = NumofdaysInMonth(year, month);
            int numIDX = 0;
            int startDate = GetDayOfWeekOnTheMonthOfTheYear(year, month, 1);
            int[] days = new int[numofDays];
            for (var i = 0; i < days.Length; i++)
            {
                days[i] = ((startDate + i) % 7);
            }


            for (var row = 1; row <= 5; row++)
            {
                for (var col = 0; col < 7; col++)
                {
                    var box = new BoxView
                    {
                        BackgroundColor = Color.White,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        Margin = new Thickness()
                    };

                    Grid.SetRow(box, row);
                    Grid.SetColumn(box, col);
                    Calender.Children.Add(box);

                    if (numIDX < numofDays)
                    {
                        if (days[numIDX] == col)
                        {
                            var label = new Label
                            {
                                VerticalOptions = LayoutOptions.Start,
                                HorizontalOptions = LayoutOptions.CenterAndExpand,
                                Text = (numIDX + 1).ToString()
                            };
                            if (col == 6)
                            {
                                label.TextColor = Color.Blue;
                            }
                            else if (col == 0)
                            {
                                label.TextColor = Color.Red;
                            }

                            Grid.SetRow(label, row);
                            Grid.SetColumn(label, days[numIDX]);
                            Calender.Children.Add(label);
                            numIDX++;
                        }
                    }
                }
            }
        }


        private int NumofdaysInMonth(int year,int month)
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
        
        private int GetNumofdayFebruary(int year)
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


        private int GetDayOfWeekOnTheMonthOfTheYear(int year,int month,int day)
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

        private int GetGaussianInteger(double val)
        {
            int result=(int)(Math.Truncate(val));
            if (result < 0)
            {
                result--;
            }

            return result;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}