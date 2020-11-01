using EasyTimeTable.DatabaseLayer;
using EasyTimeTable.DatabaseLayer.Entity;
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
        private readonly SQLiteDatabase _database;
        public FlagSettingsViewModel Settings { get; private set; }

        public App()
        {
            Device.SetFlags(new[] { "Shapes_Experimental", "Brush_Experimental" , "RadioButton_Experimental" });
            InitializeComponent();
            _database = new DatabaseService().SQLiteDatabase;

            var colors = _database.GetAllColorsAsync().Result;

            if (colors.Count == 0 || colors is null)
            {
                var addColor1 = new AddedColor
                {
                    ColorHex = "#ff837f"
                };
                _database.AddColorAsync(addColor1).Wait();

                var addColor2 = new AddedColor
                {
                    ColorHex = "#89a5ea"
                };
                _database.AddColorAsync(addColor2).Wait();

                var addColor3 = new AddedColor
                {
                    ColorHex = "#a5ea89"
                };
                _database.AddColorAsync(addColor3).Wait();

                var addColor4 = new AddedColor
                {
                    ColorHex = "#ffcb6b"
                };
                _database.AddColorAsync(addColor4).Wait();

                var addColor5 = new AddedColor
                {
                    ColorHex = "#cbde8c"
                };
                _database.AddColorAsync(addColor5).Wait();

                var addColor6 = new AddedColor
                {
                    ColorHex = "#e96ec2"
                };
                _database.AddColorAsync(addColor6).Wait();

                var addColor7 = new AddedColor
                {
                    ColorHex = "#5dc2c4"
                };
                _database.AddColorAsync(addColor7).Wait();
            }


            //Current.Properties.Clear();

            Settings = new FlagSettingsViewModel(Current.Properties);


            //if (!Settings.IsSkipCarousel)
            //{
            //    MainPage = new FirstInformationCarouselPage();
            //}
            //else
            //{
            //    MainPage = new EasyTimetablePage();
            //}
            MainPage = new EasyTimetablePage();
            //MainPage = new AddShcedulePage();




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
