using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IBU_Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExamsPage : ContentPage
    {
        public ExamsPage()
        {
            InitializeComponent();
            SetUp();

            ToolbarItems.Add(IBUData.ExamsToolbar);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            IBUData.CurrentPage = this;
        }

        private void SetUp()
        {
            if (IBUData.AttendanceData != null)
            {
                ExamsLayout.Children.Clear();
                if (IBUData.AttendanceData.Courses.Length == 0)
                {
                    ExamsLayout.Children.Add(new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.Start,
                        BackgroundColor = Color.FromHex("#BCE8F1"),
                        Margin = 5,
                        Padding = 2,
                        Children =
                        {
                            new StackLayout()
                            {
                                Orientation = StackOrientation.Horizontal,
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                VerticalOptions = LayoutOptions.Start,
                                BackgroundColor = Color.White,
                                Padding = new Thickness(10, 5, 10, 5),
                                Children =
                                {
                                    new Label()
                                    {
                                        Text = "You don't have any exam dates.",
                                        TextColor = Color.FromHex("#32708F"),
                                        HorizontalTextAlignment = TextAlignment.Start,
                                        VerticalTextAlignment = TextAlignment.Center,
                                        FontSize = 17
                                    }
                                }
                            }
                        }
                    });
                }
                else
                {
                    foreach (Terms term in IBUData.ExamsData.Terms)
                    {
                        StackLayout termLayout = new StackLayout()
                        {
                            Padding = new Thickness(0),
                            Spacing = 0,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.Start,
                            BackgroundColor = Color.White,
                            Children =
                            {
                                new StackLayout()
                                {
                                    Orientation = StackOrientation.Horizontal,
                                    HorizontalOptions = LayoutOptions.FillAndExpand,
                                    VerticalOptions = LayoutOptions.Start,
                                    BackgroundColor = Color.FromHex("#D0A650"),
                                    Children =
                                    {
                                        new StackLayout()
                                        {
                                            Orientation = StackOrientation.Horizontal,
                                            HorizontalOptions = LayoutOptions.StartAndExpand,
                                            VerticalOptions = LayoutOptions.Start,
                                            Padding = 10,
                                            Children =
                                            {
                                                new Image()
                                                {
                                                    Source = "datewhite.png",
                                                    HeightRequest = 20,
                                                    Margin = new Thickness(1),
                                                    VerticalOptions = LayoutOptions.FillAndExpand,
                                                    HorizontalOptions = LayoutOptions.Start
                                                },
                                                new Label()
                                                {
                                                    Text = term.Title + " Dates",
                                                    FontSize = 14,
                                                    TextColor = Color.White,
                                                    VerticalTextAlignment = TextAlignment.Center,
                                                    VerticalOptions = LayoutOptions.CenterAndExpand,
                                                    HorizontalOptions = LayoutOptions.StartAndExpand
                                                }
                                            }
                                        },

                                    }
                                },
                                new StackLayout()
                                {
                                    Orientation = StackOrientation.Vertical,
                                    HorizontalOptions = LayoutOptions.FillAndExpand,
                                    VerticalOptions = LayoutOptions.Start,
                                    HeightRequest = 2,
                                    BackgroundColor = Color.FromHex("#D0A650"),
                                }
                            }
                        };

                        Grid examsGrid = new Grid()
                        {
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.Start,
                            BackgroundColor = Color.White,
                            Padding = new Thickness(10),
                            RowSpacing = 5
                        };
                        examsGrid.ColumnDefinitions.Add(new ColumnDefinition()
                        {
                            Width = new GridLength(1, GridUnitType.Star)
                        });
                        examsGrid.ColumnDefinitions.Add(new ColumnDefinition()
                        {
                            Width = new GridLength(1, GridUnitType.Auto)
                        });
                        examsGrid.ColumnDefinitions.Add(new ColumnDefinition()
                        {
                            Width = new GridLength(1, GridUnitType.Auto)
                        });
                        examsGrid.ColumnDefinitions.Add(new ColumnDefinition()
                        {
                            Width = new GridLength(1, GridUnitType.Auto)
                        });
                        examsGrid.RowDefinitions.Add(new RowDefinition()
                        {
                            Height = new GridLength(1, GridUnitType.Auto)
                        });

                        examsGrid.Children.Add(
                            new StackLayout()
                            {
                                Orientation = StackOrientation.Horizontal,
                                Children =
                                {
                                    new Image()
                                    {
                                        Source = "academic.png",
                                        HeightRequest = 15
                                    },
                                    new Label()
                                    {
                                        Text = "Course",
                                        FontAttributes = FontAttributes.Bold,
                                        TextColor = Color.FromHex("#555555"),
                                        FontSize = 12,
                                        Margin = 2
                                    }
                                }
                            },
                            0, 0
                        );
                        examsGrid.Children.Add(
                            new Label()
                            {
                                Text = "Location",
                                FontAttributes = FontAttributes.Bold,
                                HorizontalOptions = LayoutOptions.CenterAndExpand,
                                HorizontalTextAlignment = TextAlignment.Center,
                                TextColor = Color.FromHex("#555555"),
                                FontSize = 12,
                                Margin = 2
                            },
                            1, 0
                        );
                        examsGrid.Children.Add(
                            new Label()
                            {
                                Text = "Date",
                                FontAttributes = FontAttributes.Bold,
                                HorizontalOptions = LayoutOptions.CenterAndExpand,
                                HorizontalTextAlignment = TextAlignment.Center,
                                TextColor = Color.FromHex("#555555"),
                                FontSize = 12,
                                Margin = 2
                            },
                            2, 0
                        );
                        examsGrid.Children.Add(
                            new Label()
                            {
                                Text = "Time",
                                FontAttributes = FontAttributes.Bold,
                                HorizontalOptions = LayoutOptions.CenterAndExpand,
                                HorizontalTextAlignment = TextAlignment.Center,
                                TextColor = Color.FromHex("#555555"),
                                FontSize = 12,
                                Margin = 2
                            },
                            3, 0
                        );


                        examsGrid.RowDefinitions.Add(new RowDefinition()
                        {
                            Height = new GridLength(1, GridUnitType.Auto)
                        });
                        examsGrid.Children.Add(new StackLayout()
                            {
                                Orientation = StackOrientation.Vertical,
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                VerticalOptions = LayoutOptions.Start,
                                HeightRequest = 1,
                                BackgroundColor = Color.FromHex("#111111")
                            }, 0, 4, 1, 2
                        );

                        int row = 1;
                        foreach (Exams exam in term.Exams)
                        {
                            examsGrid.RowDefinitions.Add(new RowDefinition()
                            {
                                Height = new GridLength(1, GridUnitType.Auto)
                            });
                            row++;

                            examsGrid.Children.Add(
                                new StackLayout()
                                {
                                    Orientation = StackOrientation.Vertical,
                                    HorizontalOptions = LayoutOptions.StartAndExpand,
                                    VerticalOptions = LayoutOptions.Start,
                                    Spacing = 0,
                                    Children =
                                    {
                                        new Label()
                                        {
                                            Text = exam.CourseCode,
                                            FontAttributes = FontAttributes.Bold,
                                            TextColor = Color.FromHex("#777777"),
                                            FontSize = 12
                                        },
                                        new Label()
                                        {

                                            Text = exam.Course,
                                            FontAttributes = FontAttributes.Bold,
                                            TextColor = Color.FromHex("#777777"),
                                            FontSize = 12
                                        }
                                    }
                                },
                                0, row
                            );
                            examsGrid.Children.Add(
                                new Label()
                                {
                                    Text = exam.Location,
                                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                                    HorizontalTextAlignment = TextAlignment.Center,
                                    VerticalOptions = LayoutOptions.CenterAndExpand,
                                    VerticalTextAlignment = TextAlignment.Center,
                                    TextColor = Color.FromHex("#777777"),
                                    FontSize = 12
                                },
                                1, row
                            );
                            examsGrid.Children.Add(
                                new Label()
                                {
                                    Text = exam.time.ToString("dd/MM/yyyy"),
                                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                                    HorizontalTextAlignment = TextAlignment.Center,
                                    VerticalOptions = LayoutOptions.CenterAndExpand,
                                    VerticalTextAlignment = TextAlignment.Center,
                                    TextColor = Color.FromHex("#777777"),
                                    FontSize = 12
                                },
                                2, row
                            );
                            examsGrid.Children.Add(
                                new Label()
                                {
                                    Text = exam.time.ToString("HH:mm"),
                                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                                    HorizontalTextAlignment = TextAlignment.Center,
                                    VerticalOptions = LayoutOptions.CenterAndExpand,
                                    VerticalTextAlignment = TextAlignment.Center,
                                    TextColor = Color.FromHex("#777777"),
                                    FontSize = 12
                                },
                                3, row
                            );

                            examsGrid.RowDefinitions.Add(new RowDefinition()
                            {
                                Height = new GridLength(1, GridUnitType.Auto)
                            });
                            row++;
                            examsGrid.Children.Add(new StackLayout()
                                {
                                    Orientation = StackOrientation.Vertical,
                                    HorizontalOptions = LayoutOptions.FillAndExpand,
                                    VerticalOptions = LayoutOptions.Start,
                                    HeightRequest = 1,
                                    BackgroundColor = Color.FromHex("#777777")
                                }, 0, 4, row, row + 1
                            );
                        }

                        termLayout.Children.Add(new StackLayout()
                        {
                            Orientation = StackOrientation.Vertical,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.Start,
                            BackgroundColor = Color.White,
                            Padding = 10,
                            Spacing = 0,
                            Children =
                            {
                                new StackLayout()
                                {
                                    Orientation = StackOrientation.Vertical,
                                    HorizontalOptions = LayoutOptions.FillAndExpand,
                                    VerticalOptions = LayoutOptions.Start,
                                    BackgroundColor = Color.FromHex("#EEEEEE"),
                                    Padding = new Thickness(1, 1, 1, 1),
                                    Children =
                                    {
                                        examsGrid
                                    }
                                }
                            }
                        });
                        ExamsLayout.Children.Add(termLayout);
                    }
                }
            }
        }

        public Action SetUpAction
        {
            get { return new Action(SetUp); }
        }
    }
}
