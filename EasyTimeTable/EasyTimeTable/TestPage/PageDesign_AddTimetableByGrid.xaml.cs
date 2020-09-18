using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyTimeTable.TestPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageDesign_AddTimetableByGrid : ContentPage
    {
        public PageDesign_AddTimetableByGrid()
        {
            InitializeComponent();
            
            var trigger1 = new DataTrigger(typeof(Label));
            Binding b = new Binding
            {
                Source = DayNightSwitch,
                Path = "IsToggled"
            };
            trigger1.Binding = b;
            trigger1.Value = true;
            trigger1.Setters.Add(new Setter { Property = Label.IsVisibleProperty, Value = false });
            temp.Triggers.Add(trigger1);
            int row = 0;
            int col = 0;
            CreateHourBoxStack(row, col);
        }

        private StackLayout CreateHourBoxStack(int row, int col)
        {
            var test = (Style)Resources.Where(x => x.Key == "HourBox").FirstOrDefault().Value;
            var boxStack = new StackLayout
            {
                //Style = (Style)Resources.Where(x => x.Key == "HourBox").FirstOrDefault().Value
                
            };
            return boxStack;
        }
    }
}