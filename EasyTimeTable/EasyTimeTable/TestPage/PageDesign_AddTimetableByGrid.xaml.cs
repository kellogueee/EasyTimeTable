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

            //var t = new EventTrigger();
            //t.Event = "Toggled";
            //t.Actions.Add(new LabelTriggerTest());
            //var temp = new Label();
            //DataTrigger test = new DataTrigger(typeof(Label));
            //test.Binding = DayNightSwitch.IsToggled;
            var trigger1 = new DataTrigger(typeof(Label));
            //trigger1.BindingContext = DayNightSwitch;
            Binding b = new Binding();
            b.Source = DayNightSwitch;
            b.Path = "IsToggled";
            trigger1.Binding = b;
            trigger1.Value = true;
            trigger1.Setters.Add(new Setter { Property = Label.IsVisibleProperty, Value = false });
            //temp.Triggers.Add(test);
            //temp.IsVisible = false;
            //DayNightSwitch.Triggers.Add(t);
            temp.Triggers.Add(trigger1);
            //temp.Triggers.Add(t);
            //testStack.Children.Add(temp);

        }
    }
}