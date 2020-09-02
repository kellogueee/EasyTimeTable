using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Markup;

namespace EasyTimeTable
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        private static string[] tableHead = { "시간", "월", "화", "수", "목", "금" };
        private static string[] timeArray = { "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24" };
        public MainPage()
        {

            InitializeComponent();

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += OnTapGestureRecognizerTapped;

            


            var rowCount = TableBody.RowDefinitions.Count;
            var colCount = TableBody.ColumnDefinitions.Count;


            for (var k = 0; k < colCount; k++)
            {
                var boxView = new BoxView()
                {
                    Margin = new Thickness(0.1),
                    BackgroundColor = Color.White,

                };
                var text = new Label()
                {
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = tableHead[k]
                };

                TableHead.Children.Add(boxView, k, 0);
                TableHead.Children.Add(text, k, 0);
            }

            for (var i = 0; i < rowCount; i++)
            {
                for (var j = 0; j < colCount; j++)
                {
                    var boxView = new BoxView()
                    {
                        Margin = new Thickness(0.1),
                        BackgroundColor = Color.White,
                        StyleId = i + "," + j
                        
                    };
                    if (j != 0)
                    {
                        boxView.GestureRecognizers.Add(tapGestureRecognizer);
                    }

                    var text = new Label()
                    {
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.CenterAndExpand
                    };
                    
                    
                    TableBody.Children.Add(boxView, j, i);
                    if (j == 0)
                    {
                        int times = 6 + i;
                        text.Text = string.Format("{0:00}", times);
                        TableBody.Children.Add(text, j, i);
                    }


                }

            }

        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            Label itemText0 = new Label()
            {
                Text = "설정",
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                TextColor = Color.White
            };
            Label titleText = new Label()
            {
                Text = "시간표",
                HorizontalOptions=LayoutOptions.CenterAndExpand,
                VerticalOptions= LayoutOptions.CenterAndExpand,
                TextColor =Color.White
            };
            Label itemText1 = new Label()
            {
                Text = "주말",
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                TextColor = Color.White
            };
            Label itemText2 = new Label()
            {
                Text = "심야",
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions= LayoutOptions.CenterAndExpand,
                TextColor = Color.White
            };
            TitleStack.Children.Add(itemText0);
            TitleStack.Children.Add(titleText);
            TitleStack.Children.Add(itemText1);
            TitleStack.Children.Add(itemText2);
        }


        async void OnTapGestureRecognizerTapped(object sender, EventArgs e)
        {
            var box = (BoxView)sender;

            var idxs = box.StyleId.Split(',');

            await Navigation.PushModalAsync(new SetTimeTablePage(idxs));

        }
    }
}

