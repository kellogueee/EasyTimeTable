using System;
using Xamarin.Forms;
namespace EasyTimeTable.TestPage
{
    public class LabelTriggerTest:TriggerAction<Label>
    {
        public LabelTriggerTest()
        {
        }

        protected override void Invoke(Label sender)
        {
            sender.IsVisible = false;
        }
    }
}
