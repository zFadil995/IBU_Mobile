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
            if (IBUData.GradesData != null)
            {
                GradesLayout.Children.Clear();

                List<string> semesterList = new List<string>();
                List<string> coursesList = new List<string>();

                foreach (Grades grade in IBUData.GradesData.Grades)
                {
                    if (!semesterList.Contains(grade.Semester))
                        semesterList.Add((grade.Semester));
                    if (!coursesList.Contains(grade.Course + "$" + grade.Semester))
                        coursesList.Add((grade.Course + "$" + grade.Semester));
                }
                
                semesterList.Sort();
                semesterList.Reverse();
                coursesList.Sort();

                foreach (string semester in semesterList)
                {
                    StackLayout coursesLayout = new StackLayout()
                    {
                        Orientation = StackOrientation.Vertical,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.Start,
                        Padding = 10,
                        Spacing = 10,
                        BackgroundColor = Color.White
                    };

                    foreach (string course in coursesList)
                    {
                        if (course.Split('$')[1] == semester)
                        {
                            Grid gradesGrid = new Grid()
                            {
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                VerticalOptions = LayoutOptions.Start,
                                BackgroundColor = Color.White,
                                Padding = new Thickness(10),
                                RowSpacing = 5
                            };
                            gradesGrid.ColumnDefinitions.Add(new ColumnDefinition()
                            {
                                Width = new GridLength(1, GridUnitType.Star)
                            });
                            gradesGrid.ColumnDefinitions.Add(new ColumnDefinition()
                            {
                                Width = new GridLength(1, GridUnitType.Star)
                            });
                            gradesGrid.ColumnDefinitions.Add(new ColumnDefinition()
                            {
                                Width = new GridLength(1, GridUnitType.Auto)
                            });
                            gradesGrid.ColumnDefinitions.Add(new ColumnDefinition()
                            {
                                Width = new GridLength(1, GridUnitType.Auto)
                            });
                            gradesGrid.RowDefinitions.Add(new RowDefinition()
                            {
                                Height = new GridLength(1, GridUnitType.Auto)
                            });

                            gradesGrid.Children.Add(
                                new Label()
                                {
                                    Text = "Title",
                                    FontAttributes = FontAttributes.Bold,
                                    TextColor = Color.Black,
                                    FontSize = 12
                                },
                                0, 0
                            );
                            gradesGrid.Children.Add(
                                new Label()
                                {
                                    Text = "Type",
                                    FontAttributes = FontAttributes.Bold,
                                    TextColor = Color.Black,
                                    FontSize = 12
                                },
                                1, 0
                            );
                            gradesGrid.Children.Add(
                                new Label()
                                {
                                    Text = "Percent",
                                    FontAttributes = FontAttributes.Bold,
                                    TextColor = Color.Black,
                                    FontSize = 12
                                },
                                2, 0
                            );
                            gradesGrid.Children.Add(
                                new Label()
                                {
                                    Text = "Grade",
                                    FontAttributes = FontAttributes.Bold,
                                    TextColor = Color.Black,
                                    FontSize = 12
                                },
                                3, 0
                            );
                            //gradesGrid.Children.Add(new StackLayout()
                            //    {
                            //        HorizontalOptions = LayoutOptions.FillAndExpand,
                            //        VerticalOptions = LayoutOptions.Start,
                            //        BackgroundColor = Color.FromHex("#DDDDDD"),
                            //        HeightRequest = 2
                            //    },
                            //    0, 3, 1, 0);

                            int row = 0;
                            float points = 0;
                            foreach (Grades grade in IBUData.GradesData.Grades)
                            {
                                if (grade.Semester == semester && grade.Course == course.Split('$')[0])
                                {
                                    points += int.Parse(grade.Percent) * int.Parse(grade.Grade);
                                    gradesGrid.RowDefinitions.Add(new RowDefinition()
                                    {
                                        Height = new GridLength(1, GridUnitType.Auto)
                                    });
                                    row++;

                                    gradesGrid.Children.Add(
                                        new Label()
                                        {
                                            Text = grade.Title,
                                            TextColor = Color.Black,
                                            FontSize = 12
                                        },
                                        0, row
                                    );
                                    gradesGrid.Children.Add(
                                        new Label()
                                        {
                                            Text = grade.Type,
                                            TextColor = Color.Black,
                                            FontSize = 12
                                        },
                                        1, row
                                    );
                                    gradesGrid.Children.Add(
                                        new Label()
                                        {
                                            Text = grade.Percent,
                                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                                            HorizontalTextAlignment = TextAlignment.Center,
                                            TextColor = Color.Black,
                                            FontSize = 12
                                        },
                                        2, row
                                    );
                                    gradesGrid.Children.Add(
                                        new Label()
                                        {
                                            Text = grade.Grade,
                                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                                            HorizontalTextAlignment = TextAlignment.Center,
                                            TextColor = Color.Black,
                                            FontSize = 12
                                        },
                                        3, row
                                    );
                                }
                            }
                            gradesGrid.RowDefinitions.Add(new RowDefinition()
                            {
                                Height = new GridLength(1, GridUnitType.Auto)
                            });
                            row++;

                            gradesGrid.Children.Add(
                                new Label()
                                {
                                    Text = "Calculated Points: ",
                                    FontAttributes = FontAttributes.Bold,
                                    TextColor = Color.FromHex("#555555"),
                                    FontSize = 12
                                },
                                0, row
                            );
                            gradesGrid.Children.Add(
                                new Label()
                                {
                                    Text = (points / 100).ToString(),
                                    FontAttributes = FontAttributes.Bold,
                                    TextColor = Color.FromHex("#555555"),
                                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                                    HorizontalTextAlignment = TextAlignment.Center,
                                    FontSize = 12
                                },
                                3, row
                            );
                            coursesLayout.Children.Add(new StackLayout()
                            {
                                Orientation = StackOrientation.Vertical,
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                VerticalOptions = LayoutOptions.Start,
                                Padding = 5,
                                Spacing = 0,
                                Children =
                                {
                                    new StackLayout()
                                    {
                                        Orientation = StackOrientation.Horizontal,
                                        HorizontalOptions = LayoutOptions.FillAndExpand,
                                        VerticalOptions = LayoutOptions.Start,
                                        BackgroundColor = Color.FromHex("#EEEEEE"),
                                        Padding = new Thickness(3),
                                        Children =
                                        {
                                            new Image()
                                            {
                                                Source = "placeholder.png",
                                                HeightRequest = 13,
                                                Margin = new Thickness(2),
                                                VerticalOptions = LayoutOptions.FillAndExpand,
                                                HorizontalOptions = LayoutOptions.Start
                                            },
                                            new Label()
                                            {
                                                Text = course.Split('$')[0],
                                                FontSize = 11,
                                                TextColor = Color.Black,
                                                FontAttributes = FontAttributes.Bold,
                                                VerticalTextAlignment = TextAlignment.Center
                                            }
                                        }
                                    },
                                    new StackLayout()
                                    {
                                        Orientation = StackOrientation.Vertical,
                                        HorizontalOptions = LayoutOptions.FillAndExpand,
                                        VerticalOptions = LayoutOptions.Start,
                                        HeightRequest = 1,
                                        BackgroundColor = Color.FromHex("#F0C670")
                                    },
                                    new StackLayout()
                                    {
                                        Orientation = StackOrientation.Vertical,
                                        HorizontalOptions = LayoutOptions.FillAndExpand,
                                        VerticalOptions = LayoutOptions.Start,
                                        BackgroundColor = Color.FromHex("#EEEEEE"),
                                        Padding = new Thickness(1, 0, 1, 1),
                                        Children =
                                        {
                                            gradesGrid
                                        }
                                    }
                                }
                            });
                        }
                    }

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
                                new StackLayout()
                                {
                                    Orientation = StackOrientation.Vertical,
                                    HorizontalOptions = LayoutOptions.FillAndExpand,
                                    VerticalOptions = LayoutOptions.Start,
                                    HeightRequest = 2,
                                    BackgroundColor = Color.FromHex("#F0C670")
                                },
                                coursesLayout
                            }

                        }
                    );
                }
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
