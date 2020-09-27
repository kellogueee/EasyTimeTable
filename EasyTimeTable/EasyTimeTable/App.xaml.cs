using EasyTimeTable.DataAccessLayer;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyTimeTable
{
    public partial class App : Application
    {

        //static Database database;

        //public static Database Database
        //{
        //    get
        //    {
        //        if (database == null)
        //        {
        //            database = new Database(Path
        //                .Combine(Environment
        //                .GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "table.db3"));
        //        }

        //        return database;
        //    }

        //}
        public static int languague = 0;


        public App()
        {
            Device.SetFlags(new[] { "Shapes_Experimental", "Brush_Experimental" });

            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
            //MainPage = new ColorChagneTest();
            //MainPage = new PageDesign_AddTimetableByGrid();
            //MainPage = new PageDesignTest(20,0);
            //MainPage = new NavigationPage(new MainPageDesignTest());
            //MainPage = new NavigationPage(new MainPageDesignTest2());
            //MainPage = new DailySchedulePage();
            //MainPage = new NavigationPage(new CalendarPage());
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
