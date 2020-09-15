﻿using EasyTimeTable.ViewModels;
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
    public partial class ColorChagneTest : ContentPage
    {
        public ColorChagneTest()
        {
            InitializeComponent();
            ColorSelectCollectionView.BindingContext = new ColorSelectViewModel();
        }
    }
}