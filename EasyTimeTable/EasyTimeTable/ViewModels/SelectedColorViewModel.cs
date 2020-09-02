using EasyTimeTable.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace EasyTimeTable.ViewModels
{
    public class SelectedColorViewModel:BaseViewModel
    {


        private readonly IList<ColorSelect> source;

        public ObservableCollection<ColorSelect> ColorsToSelect { get; private set; }

        public SelectedColorViewModel()
        {
            source = new List<ColorSelect>();
            CreateColorsCollection();
        }

        void CreateColorsCollection()
        {
            source.Add(new ColorSelect
            {
                SelectionColor = Color.FromHex("#ff837f")
            });
            source.Add(new ColorSelect
            {
                SelectionColor = Color.FromHex("#89a5ea")
            });
            source.Add(new ColorSelect
            {
                SelectionColor = Color.FromHex("#a5ea89")
            });
            source.Add(new ColorSelect
            {
                SelectionColor = Color.FromHex("#ffcb6b")
            });
            source.Add(new ColorSelect
            {
                SelectionColor = Color.FromHex("#e96ec2")
            });
            source.Add(new ColorSelect
            {
                SelectionColor = Color.FromHex("#5dc2c4")
            });
            source.Add(new ColorSelect
            {
                SelectionColor = Color.FromHex("#cbde8c")
            });

            ColorsToSelect = new ObservableCollection<ColorSelect>(source);
        }
    }
}
