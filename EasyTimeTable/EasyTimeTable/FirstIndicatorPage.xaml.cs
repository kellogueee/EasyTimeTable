using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml;
using System.Runtime.Serialization;
using EasyTimeTable.ViewModels;

namespace EasyTimeTable
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstIndicatorPage : ContentPage
    {
        public FirstIndicatorPage()
        {
            InitializeComponent();
            BindingContext = new IntroductionViewModel();
        }

        private void OnStartButtonClicked(object sender, EventArgs e)
        {
            File.WriteAllText(App.fileName, "false");
            App.Current.MainPage = new NavigationPage(new DayAndWeekdayPage());
        }
    }
}