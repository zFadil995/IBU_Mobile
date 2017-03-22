using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using IBU_Mobile.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IBU_Mobile
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GradesPage : ContentPage
    {
        public GradesPage()
        {
            InitializeComponent();
            IBUData.CurrentPage = this;
            SetUp();
        }

        private void SetUp()
        {
            List<string> semesterList = new List<string>();
            List<string> coursesList = new List<string>();

            foreach (Grades grades in IBUData.GradesData.Grades)
            {
                if (!semesterList.Contains(grades.Semester))
                    semesterList.Add((grades.Semester));
                if (!coursesList.Contains(grades.Course))
                    coursesList.Add((grades.Course));
            }

            foreach (string semester in semesterList)
            {
                GradesLayout.Children.Add(new StackLayout()
                {
                    Padding = new Thickness(0),
                    Spacing = 0,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.Start,
                    Children =
                    {
                        new StackLayout()
                        {
                            Orientation = StackOrientation.Horizontal,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.Start,
                            Padding = 10,
                            BackgroundColor = Color.FromHex("#D0A650"),
                            Children =
                            {
                                new Image()
                                {
                                    Source = "placeholder.png", 
                                    HeightRequest = 15, 
                                    Margin = new Thickness(3),
                                    VerticalOptions = LayoutOptions.FillAndExpand,
                                    HorizontalOptions = LayoutOptions.Start
                                },
                                new Label()
                                {
                                    Text = semester,
                                    FontSize = 14,
                                    TextColor = Color.White,
                                    FontAttributes = FontAttributes.Bold,
                                    VerticalTextAlignment = TextAlignment.Center
                                }
                            }
                        },
                        new StackLayout(){ Orientation = StackOrientation.Vertical,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.Start,
                            HeightRequest = 2,
                            BackgroundColor = Color.FromHex("#F0C670")},

                        new StackLayout(){ Orientation = StackOrientation.Vertical,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.Start,
                            Padding = 10,
                            BackgroundColor = Color.White}
                    }

                });
            }

        }

        public Action SetUpAction
        {
            get
            {
                return new Action(SetUp);
            }
        }
    }
}
