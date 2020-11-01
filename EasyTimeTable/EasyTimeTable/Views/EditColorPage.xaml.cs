using EasyTimeTable.DatabaseLayer;
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
    public partial class EditColorPage : ContentPage
    {
        private readonly SQLiteDatabase _database;

        public EditColorPage()
        {
            InitializeComponent();
            _database = new DatabaseService().SQLiteDatabase;
        }



    }
}