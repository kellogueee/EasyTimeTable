using EasyTimeTable.DataAccessLayer;
using EasyTimeTable.TestPage;
using System;
using System.IO;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyTimeTable
{
    public partial class App : Application
    {
        public static string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FirstIndicator.txt");

        public App()
        {
            Device.SetFlags(new[] { "Shapes_Experimental", "Brush_Experimental" });
            InitializeComponent();

            //if (File.Exists(fileName))
            //{
            //    var FirstOrNot = File.ReadAllText(fileName).Trim();

            //    //시작인디케이터 표시
            //    if (FirstOrNot == "true")
            //    {
            //        MainPage= new FirstIndicatorPage();

            //    }
            //    //아니면 그냥
            //    else
            //    {
            //        MainPage = new NavigationPage(new DayAndWeekdayPage());
            //    }
            //}
            //else
            //{
            //    MainPage = new FirstIndicatorPage();
            //}

            MainPage = new FirstIndicatorPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
