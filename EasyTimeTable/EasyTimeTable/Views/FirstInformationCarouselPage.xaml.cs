using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyTimeTable.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstInformationCarouselPage : ContentPage
    {
        public FirstInformationCarouselPage()
        {
            InitializeComponent();
        }


        private void OnStartButtonClicked(object sender, EventArgs e)
        {
            skip.IsToggled = true;
            Application.Current.MainPage = new EasyTimetablePage();
        }
    }
}