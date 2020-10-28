using EasyTimeTable.DatabaseLayer;
using EasyTimeTable.ViewModels;
using EasyTimeTable.Views;
using System;
using System.IO;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyTimeTable
{
    public partial class App : Application
    {
        public FlagSettingsViewModel Settings { get; private set; }

        public App()
        {
            Device.SetFlags(new[] { "Shapes_Experimental", "Brush_Experimental" });
            InitializeComponent();
            Current.Properties.Clear();

            Settings = new FlagSettingsViewModel(Current.Properties);

            
            if (!Settings.IsSkipCarousel)
            {
                MainPage = new FirstInformationCarouselPage();
            }
            else
            {
                MainPage = new EasyTimetablePage();
            }
            //MainPage = new EasyTimetablePage();


        }


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            Settings.SaveState(Current.Properties);
        }

        protected override void OnResume()
        {
        }
    }
}
