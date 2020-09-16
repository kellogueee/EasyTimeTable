using EasyTimeTable.DataAccessLayer;
using EasyTimeTable.TestPage;
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

        public App()
        {
            Device.SetFlags(new[] { "Shapes_Experimental", "Brush_Experimental" });

            InitializeComponent();

            //MainPage = new NavigationPage(new MainPage());
            //MainPage = new ColorChagneTest();
            MainPage = new PageDesign_AddTimetableByGrid();
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
