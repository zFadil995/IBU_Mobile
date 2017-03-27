using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using IBU_Mobile.Helpers;
using IBU_Mobile.Pages;
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
            SetUp();

            ToolbarItems.Add(IBUData.GradesToolbar);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            IBUData.CurrentPage = this;
        }

        private void SetUp()
        {
            if (IBUData.GradesData != null)
            {
                GradesLayout.Children.Clear();

                AddSemester(IBUData.GradesData.CurrentSemester);
            }
            if (IBUData.GradesData != null && IBUData.GradesData.PreviousSemesters.Length > 0)
            {
                foreach (Semesters previousSemester in IBUData.GradesData.PreviousSemesters)
                {
                    AddSemester(previousSemester);
                }
            }
        }

        private void AddSemester(Semesters semester)
        {
            Image expandSemester = new Image()
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Source = "down.png",
                HeightRequest = 20,
                Margin = new Thickness(1)
            };
            StackLayout lineLayout =
                new StackLayout()
                {
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.Start,
                    HeightRequest = 2,
                    BackgroundColor = Color.FromHex("#F0C670")
                };
            StackLayout labelLayout = new StackLayout()
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
                                Source = "academic.png",
                                HeightRequest = 20,
                                Margin = new Thickness(1),
                                VerticalOptions = LayoutOptions.FillAndExpand,
                                HorizontalOptions = LayoutOptions.Start
                            },
                            new Label()
                            {
                                Text =
                                    semester.Year + " / " +
                                    ((semester.Semester == 1) ? "Fall" : "Spring"),
                                FontSize = 14,
                                TextColor = Color.White,
                                FontAttributes = FontAttributes.Bold,
                                VerticalTextAlignment = TextAlignment.Center,
                                VerticalOptions = LayoutOptions.CenterAndExpand,
                                HorizontalOptions = LayoutOptions.StartAndExpand
                            }
                        }
            };
            StackLayout SemesterLayout = new StackLayout()
            {
                Padding = new Thickness(0),
                Spacing = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start
            };
           
            expandSemester.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(() => { SetSemester(SemesterLayout, semester, expandSemester); }) });

            labelLayout.Children.Add(expandSemester);
            SemesterLayout.Children.Add(labelLayout);
            SemesterLayout.Children.Add(lineLayout);

            if (semester == IBUData.GradesData.CurrentSemester)
            {
                SetSemester(SemesterLayout, semester, expandSemester);
            }

            GradesLayout.Children.Add(SemesterLayout);
        }
        private void SetSemester(StackLayout SemesterLayout, Semesters previousSemester, Image expandSemester)
        {

            if (SemesterLayout.Children.Count == 2)
            {
                expandSemester.Source = "up.png";
                StackLayout coursesLayout = new StackLayout()
                {
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.Start,
                    Padding = 10,
                    Spacing = 10,
                    BackgroundColor = Color.White
                };

                foreach (Courses previousSemesterCourse in previousSemester.Courses)
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
                    gradesGrid.RowDefinitions.Add(new RowDefinition()
                    {
                        Height = new GridLength(1, GridUnitType.Auto)
                    });
                    if (previousSemesterCourse.Grades.Length > 0)
                    {
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

                        gradesGrid.Children.Add(
                            new Label()
                            {
                                Text = "Title",
                                FontAttributes = FontAttributes.Bold,
                                TextColor = Color.FromHex("#555555"),
                                FontSize = 12
                            },
                            0, 0
                        );
                        gradesGrid.Children.Add(
                            new Label()
                            {
                                Text = "Type",
                                FontAttributes = FontAttributes.Bold,
                                TextColor = Color.FromHex("#555555"),
                                FontSize = 12
                            },
                            1, 0
                        );
                        gradesGrid.Children.Add(
                            new Label()
                            {
                                Text = "Percent  ",
                                FontAttributes = FontAttributes.Bold,
                                TextColor = Color.FromHex("#555555"),
                                FontSize = 12
                            },
                            2, 0
                        );
                        gradesGrid.Children.Add(
                            new Label()
                            {
                                Text = "Grade",
                                FontAttributes = FontAttributes.Bold,
                                TextColor = Color.FromHex("#555555"),
                                FontSize = 12
                            },
                            3, 0
                        );


                        gradesGrid.RowDefinitions.Add(new RowDefinition()
                        {
                            Height = new GridLength(1, GridUnitType.Auto)
                        });
                        gradesGrid.Children.Add(new StackLayout()
                        {
                            Orientation = StackOrientation.Vertical,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.Start,
                            HeightRequest = 1,
                            BackgroundColor = Color.FromHex("#111111")
                        }, 0, 4, 1, 2
                        );

                        int row = 1;
                        float points = 0;
                        foreach (Grades grade in previousSemesterCourse.Grades)
                        {
                            points += grade.Percent * grade.Grade;
                            gradesGrid.RowDefinitions.Add(new RowDefinition()
                            {
                                Height = new GridLength(1, GridUnitType.Auto)
                            });
                            row++;

                            gradesGrid.Children.Add(
                                new Label()
                                {
                                    Text = grade.Title,
                                    TextColor = Color.FromHex("#777777"),
                                    FontSize = 12
                                },
                                0, row
                            );
                            gradesGrid.Children.Add(
                                new Label()
                                {
                                    Text = grade.Type,
                                    TextColor = Color.FromHex("#777777"),
                                    FontSize = 12
                                },
                                1, row
                            );
                            gradesGrid.Children.Add(
                                new Label()
                                {
                                    Text = grade.Percent.ToString(),
                                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                                    HorizontalTextAlignment = TextAlignment.Center,
                                    TextColor = Color.FromHex("#777777"),
                                    FontSize = 12
                                },
                                2, row
                            );
                            gradesGrid.Children.Add(
                                new Label()
                                {
                                    Text = grade.Grade.ToString(),
                                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                                    HorizontalTextAlignment = TextAlignment.Center,
                                    TextColor = Color.FromHex("#777777"),
                                    FontSize = 12
                                },
                                3, row
                            );

                            gradesGrid.RowDefinitions.Add(new RowDefinition()
                            {
                                Height = new GridLength(1, GridUnitType.Auto)
                            });
                            row++;
                            gradesGrid.Children.Add(new StackLayout()
                            {
                                Orientation = StackOrientation.Vertical,
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                VerticalOptions = LayoutOptions.Start,
                                HeightRequest = 1,
                                BackgroundColor = Color.FromHex("#777777")
                            }, 0, 4, row, row + 1
                            );

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
                                FontSize = 13
                            },
                            0, row
                        );
                        gradesGrid.Children.Add(
                            new Label()
                            {
                                Text = (points / 100).ToString(),
                                FontAttributes = FontAttributes.Bold,
                                TextColor = ((points / 100) > 54.45) ? Color.FromHex("#555555") : Color.Red,
                                HorizontalOptions = LayoutOptions.CenterAndExpand,
                                HorizontalTextAlignment = TextAlignment.Center,
                                FontSize = 13
                            },
                            3, row
                        );

                        if (previousSemesterCourse?.FinalGrade != 0)
                        {
                            gradesGrid.RowDefinitions.Add(new RowDefinition()
                            {
                                Height = new GridLength(1, GridUnitType.Auto)
                            });
                            row++;
                            gradesGrid.Children.Add(new StackLayout()
                            {
                                Orientation = StackOrientation.Vertical,
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                VerticalOptions = LayoutOptions.Start,
                                HeightRequest = 1,
                                BackgroundColor = Color.FromHex("#222222")
                            }, 0, 4, row, row + 1
                            );

                            gradesGrid.RowDefinitions.Add(new RowDefinition()
                            {
                                Height = new GridLength(1, GridUnitType.Auto)
                            });
                            row++;

                            gradesGrid.Children.Add(
                                new Label()
                                {
                                    Text = "Final Grade: ",
                                    FontAttributes = FontAttributes.Bold,
                                    TextColor = Color.FromHex("#222222"),
                                    FontSize = 13
                                },
                                0, row
                            );
                            gradesGrid.Children.Add(
                                new Label()
                                {
                                    Text = previousSemesterCourse.FinalGrade.ToString(),
                                    FontAttributes = FontAttributes.Bold,
                                    TextColor =
                                        ((previousSemesterCourse.FinalGrade) > 5)
                                            ? Color.FromHex("#111111")
                                            : Color.Red,
                                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                                    HorizontalTextAlignment = TextAlignment.Center,
                                    FontSize = 13
                                },
                                3, row
                            );
                        }
                    }
                    else
                    {
                        gradesGrid.Children.Add(new StackLayout()
                        {
                            Orientation = StackOrientation.Horizontal,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.Start,
                            BackgroundColor = Color.FromHex("#BCE8F1"),
                            Padding = 1,
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
                                            Text = "You don't have any grades for this course.",
                                            TextColor = Color.FromHex("#32708F"),
                                            HorizontalTextAlignment = TextAlignment.Start,
                                            VerticalTextAlignment = TextAlignment.Center,
                                            FontSize = 12
                                        }
                                    }
                                }
                            }
                        }, 0, 0
                        );
                    }
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
                                    Source = "subject.png",
                                    HeightRequest = 13,
                                    Margin = new Thickness(2),
                                    VerticalOptions = LayoutOptions.FillAndExpand,
                                    HorizontalOptions = LayoutOptions.Start
                                },
                                new Label()
                                {
                                    Text =
                                        previousSemesterCourse.CourseCode + "/ " +
                                        previousSemesterCourse.CourseName,
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

                SemesterLayout.Children.Add(coursesLayout);
            }
            else
            {
                expandSemester.Source = "down.png";

                SemesterLayout.Children.RemoveAt(2);
            }

            
        }

        public Action SetUpAction
        {
            get { return new Action(SetUp); }
        }
    }
}
