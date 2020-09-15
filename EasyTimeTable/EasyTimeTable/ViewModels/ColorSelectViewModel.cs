using EasyTimeTable.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace EasyTimeTable.ViewModels
{
    public class ColorSelectViewModel:BaseViewModel
    {
        private readonly IList<ColorModel> source;

        public ObservableCollection<ColorModel> SetofColors { get; private set; }

        ColorModel preSelectedColor;
        public ColorModel PreSelectedColor
        {
            get
            {
                return preSelectedColor;
            }
            set
            {
                if (preSelectedColor != value)
                {
                    preSelectedColor = value;
                }
            }
        }

        public ColorSelectViewModel()
        {
            source = new List<ColorModel>();
            CreateSetofColors();
            Random rnd = new Random();
            preSelectedColor = SetofColors.Skip(rnd.Next(0,6)).FirstOrDefault();
        }


        public ColorSelectViewModel(string color)
        {
            source = new List<ColorModel>();
            CreateSetofColors();
            preSelectedColor = SetofColors.Where(x => x.ColortoSelect == Color.FromHex(color)).FirstOrDefault();
        }

        void CreateSetofColors()
        {
            source.Add(new ColorModel
            {
                ColortoSelect = Color.FromHex("#ff837f")
            });
            source.Add(new ColorModel
            {
                ColortoSelect = Color.FromHex("#89a5ea")
            });
            source.Add(new ColorModel
            {
                ColortoSelect = Color.FromHex("#a5ea89")
            });
            source.Add(new ColorModel
            {
                ColortoSelect = Color.FromHex("#ffcb6b")
            });
            source.Add(new ColorModel
            {
                ColortoSelect = Color.FromHex("#e96ec2")
            });
            source.Add(new ColorModel
            {
                ColortoSelect = Color.FromHex("#5dc2c4")
            });
            source.Add(new ColorModel
            {
                ColortoSelect = Color.FromHex("#cbde8c")
            });

            SetofColors = new ObservableCollection<ColorModel>(source);
        }
        
    }
}
