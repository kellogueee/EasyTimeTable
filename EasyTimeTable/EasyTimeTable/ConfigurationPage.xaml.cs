using EasyTimeTable.DataAccessLayer;
using EasyTimeTable.DataAccessLayer.SqliteEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyTimeTable
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfigurationPage : ContentPage
    {
        private readonly IDatabase<IterativeSchedule> _database;
        public ConfigurationPage()
        {
            InitializeComponent();
            _database = new DatabaseService().SQLiteDatabase;
        }
       
        private async void OnAllScheduleDeleteClicked(object sender, EventArgs e)
        {
            var willDelete=await DisplayAlert("삭제", "모든 일정을 삭제 하시겠습니까?", "네", "아니오");
            if (willDelete)
            {
                await _database.DeleteAllSchedule();
            }
        }
    }
}