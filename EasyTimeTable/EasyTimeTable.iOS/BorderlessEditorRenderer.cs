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



[assembly: ExportRenderer(typeof(BorderlessEditor), typeof(BorderlessEditorRenderer))]
namespace EasyTimeTable.iOS
{
    public class BorderlessEditorRenderer:EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Layer.BorderWidth = 0;
            }
        }
    }
}