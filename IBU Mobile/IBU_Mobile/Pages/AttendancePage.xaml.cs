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
    public partial class AttendancePage : ContentPage
    {
        public AttendancePage()
        {
            InitializeComponent();
            SetUp();

            ToolbarItems.Add(IBUData.AttendanceToolbar);
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
                AttendanceLayout.Children.Clear();
                if (IBUData.AttendanceData.Courses.Length == 0)
                {
                    AttendanceLayout.Children.Add(new StackLayout()
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
                                        Text = "You don't have any courses registered.",
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
                    foreach (aCourses course in IBUData.AttendanceData.Courses)
                    {
                        Image expandCourse = new Image()
                        {
                            HorizontalOptions = LayoutOptions.EndAndExpand,
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            Source = "down.png",
                            HeightRequest = 35,
                            Margin = new Thickness(1, 1, 4, 1)
                        };
                        StackLayout lineLayout =
                            new StackLayout()
                            {
                                Orientation = StackOrientation.Vertical,
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                VerticalOptions = LayoutOptions.Start,
                                HeightRequest = 2,
                                BackgroundColor = Color.FromHex("#D0A650"),
                            };
                        Label titleLayout =
                            new Label()
                            {
                                Text = course.Name,
                                FontSize = 14,
                                TextColor = Color.White,
                                VerticalTextAlignment = TextAlignment.Center,
                                VerticalOptions = LayoutOptions.CenterAndExpand,
                                HorizontalOptions = LayoutOptions.StartAndExpand
                            };
                        StackLayout labelLayout = new StackLayout()
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
                                            Source = "attendancewhite.png",
                                            HeightRequest = 20,
                                            Margin = new Thickness(1),
                                            VerticalOptions = LayoutOptions.FillAndExpand,
                                            HorizontalOptions = LayoutOptions.Start
                                        },
                                        titleLayout
                                    }
                                },

                            }
                        };
                        StackLayout courseLayout = new StackLayout()
                        {
                            Padding = new Thickness(0),
                            Spacing = 0,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.Start,
                            BackgroundColor = Color.White
                        };

                        expandCourse.GestureRecognizers.Add(new TapGestureRecognizer()
                        {
                            Command = new Command(() => { SetCourse(courseLayout, course, expandCourse); })
                        });

                        labelLayout.Children.Add(expandCourse);
                        courseLayout.Children.Add(labelLayout);
                        courseLayout.Children.Add(lineLayout);

                        AttendanceLayout.Children.Add(courseLayout);
                    }
                }
            }
        }

        public void SetCourse(StackLayout courseLayout, aCourses course, Image expandCourse)
        {

            if (courseLayout.Children.Count == 2)
            {
                expandCourse.Source = "up.png";

                if (course.Attendance.Length > 0)
                {
                    Grid attendanceDetailsGrid = new Grid()
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.Start,
                        BackgroundColor = Color.White,
                        Padding = new Thickness(10),
                        RowSpacing = 5
                    };
                    attendanceDetailsGrid.ColumnDefinitions.Add(new ColumnDefinition()
                    {
                        Width = new GridLength(1, GridUnitType.Auto)
                    });
                    attendanceDetailsGrid.ColumnDefinitions.Add(new ColumnDefinition()
                    {
                        Width = new GridLength(1, GridUnitType.Star)
                    });
                    attendanceDetailsGrid.ColumnDefinitions.Add(new ColumnDefinition()
                    {
                        Width = new GridLength(1, GridUnitType.Auto)
                    });
                    attendanceDetailsGrid.ColumnDefinitions.Add(new ColumnDefinition()
                    {
                        Width = new GridLength(1, GridUnitType.Auto)
                    });
                    attendanceDetailsGrid.RowDefinitions.Add(new RowDefinition()
                    {
                        Height = new GridLength(1, GridUnitType.Auto)
                    });

                    attendanceDetailsGrid.Children.Add(
                        new Label()
                        {
                            Text = "Date",
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.FromHex("#555555"),
                            FontSize = 12,
                            Margin = 2
                        },
                        0, 0
                    );
                    attendanceDetailsGrid.Children.Add(
                        new Label()
                        {
                            Text = "Subject",
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.FromHex("#555555"),
                            FontSize = 12,
                            Margin = 2
                        },
                        1, 0
                    );
                    attendanceDetailsGrid.Children.Add(
                        new Label()
                        {
                            Text = "Full",
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.FromHex("#555555"),
                            FontSize = 12,
                            Margin = 2
                        },
                        2, 0
                    );
                    attendanceDetailsGrid.Children.Add(
                        new Label()
                        {
                            Text = "My",
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.FromHex("#555555"),
                            FontSize = 12,
                            Margin = 2
                        },
                        3, 0
                    );


                    attendanceDetailsGrid.RowDefinitions.Add(new RowDefinition()
                    {
                        Height = new GridLength(1, GridUnitType.Auto)
                    });
                    attendanceDetailsGrid.Children.Add(new StackLayout()
                        {
                            Orientation = StackOrientation.Vertical,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.Start,
                            HeightRequest = 1,
                            BackgroundColor = Color.FromHex("#111111")
                        }, 0, 4, 1, 2
                    );

                    int row = 1;
                    double lab = 0, labhours = 0, lecture = 0, lecturehours = 0;
                    foreach (Attendance attendanceDetail in course.Attendance)
                    {
                        if (attendanceDetail.Type == 1)
                        {
                            lecture += attendanceDetail.Attended;
                            lecturehours += attendanceDetail.Total;
                        }
                        else if (attendanceDetail.Type == 2)
                        {
                            lab += attendanceDetail.Attended;
                            labhours += attendanceDetail.Total;
                        }
                        attendanceDetailsGrid.RowDefinitions.Add(new RowDefinition()
                        {
                            Height = new GridLength(1, GridUnitType.Auto)
                        });
                        row++;

                        attendanceDetailsGrid.Children.Add(
                            new Label()
                            {
                                Text = attendanceDetail.Date,
                                TextColor = (attendanceDetail.Attended == attendanceDetail.Total)?Color.FromHex("#777777"):(( (double) attendanceDetail.Attended / attendanceDetail.Total) >= 0.7)?Color.Orange:Color.Red,
                                FontSize = 12
                            },
                            0, row
                        );
                        attendanceDetailsGrid.Children.Add(
                            new Label()
                            {
                                Text = attendanceDetail.Subject,
                                TextColor = (attendanceDetail.Attended == attendanceDetail.Total) ? Color.FromHex("#777777") : (( (double) attendanceDetail.Attended / attendanceDetail.Total) >= 0.7) ? Color.Orange : Color.Red,
                                FontSize = 12
                            },
                            1, row
                        );
                        attendanceDetailsGrid.Children.Add(
                            new Label()
                            {
                                Text = attendanceDetail.Total.ToString(),
                                HorizontalOptions = LayoutOptions.CenterAndExpand,
                                HorizontalTextAlignment = TextAlignment.Center,
                                TextColor = (attendanceDetail.Attended == attendanceDetail.Total) ? Color.FromHex("#777777") : (((double) attendanceDetail.Attended / attendanceDetail.Total) >= 0.7) ? Color.Orange : Color.Red,
                                FontSize = 12
                            },
                            2, row
                        );
                        attendanceDetailsGrid.Children.Add(
                            new Label()
                            {
                                Text = attendanceDetail.Attended.ToString(),
                                HorizontalOptions = LayoutOptions.CenterAndExpand,
                                HorizontalTextAlignment = TextAlignment.Center,
                                TextColor = (attendanceDetail.Attended == attendanceDetail.Total) ? Color.FromHex("#777777") : (((double) attendanceDetail.Attended / attendanceDetail.Total) >= 0.7) ? Color.Orange : Color.Red,
                                FontSize = 12
                            },
                            3, row
                        );

                        attendanceDetailsGrid.RowDefinitions.Add(new RowDefinition()
                        {
                            Height = new GridLength(1, GridUnitType.Auto)
                        });
                        row++;
                        attendanceDetailsGrid.Children.Add(new StackLayout()
                            {
                                Orientation = StackOrientation.Vertical,
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                VerticalOptions = LayoutOptions.Start,
                                HeightRequest = 1,
                                BackgroundColor = Color.FromHex("#777777")
                            }, 0, 4, row, row + 1
                        );
                    }

                    courseLayout.Children.Add(new StackLayout()
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
                                    attendanceDetailsGrid
                                }
                            }
                        }
                    });
                }
                else
                {
                    courseLayout.Children.Add(new StackLayout()
                        {
                            Orientation = StackOrientation.Horizontal,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.Start,
                            BackgroundColor = Color.FromHex("#BCE8F1"),
                            Padding = 1,
                            Margin = 10,
                            Children =
                            {
                                new StackLayout()
                                {
                                    Orientation = StackOrientation.Horizontal,
                                    HorizontalOptions = LayoutOptions.FillAndExpand,
                                    VerticalOptions = LayoutOptions.Start,
                                    BackgroundColor = Color.FromHex("#D9EDF7"),
                                    Padding = 5,
                                    Children =
                                    {
                                        new Label()
                                        {
                                            Text = "You don't have any attendance details for this course.",
                                            TextColor = Color.FromHex("#32708F"),
                                            HorizontalTextAlignment = TextAlignment.Start,
                                            VerticalTextAlignment = TextAlignment.Center,
                                            FontSize = 12
                                        }
                                    }
                                }
                            }
                        }
                    );
                }
            }
            else
            {
                expandCourse.Source = "down.png";
                courseLayout.Children.RemoveAt(2);
            }
        }

        public Action SetUpAction
        {
            get { return new Action(SetUp); }
        }
    }
}
