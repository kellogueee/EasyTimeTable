using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EasyTimeTable.iOS;
using EasyTimeTable.ResourceRenderer;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;



[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]

namespace EasyTimeTable.iOS
{
    public class BorderlessEntryRenderer:EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Layer.BorderWidth = 0;
            }
        }
    }
}