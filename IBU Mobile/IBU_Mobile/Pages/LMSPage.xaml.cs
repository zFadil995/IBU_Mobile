using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IBU_Mobile.Pages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LMSPage : ContentPage
    {
        public LMSPage()
        {
            InitializeComponent();
            SetUp();

            ToolbarItems.Add(IBUData.LMSToolbar);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            IBUData.CurrentPage = this;
        }

        private void SetUp()
        {
            if (IBUData.LMSData != null)
            {
                LMSLayout.Children.Clear();
                if (IBUData.LMSData.Deadlines.Length == 0)
                {
                    LMSLayout.Children.Add(new StackLayout()
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
                                        Text = "You don't have any deadlines.",
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
                    foreach (LMSDeadlines deadline in IBUData.LMSData.Deadlines)
                    {
                        TimeSpan timeLeft = deadline.deadline.Subtract(DateTime.Now);

                        Image expandDeadline = new Image()
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
                                BackgroundColor = (timeLeft < new TimeSpan(0)) ? Color.FromHex("#D9244A") : Color.FromHex("#D0A650"),
                            };
                        Label titleLayout =
                            new Label()
                            {
                                Text = deadline.Title,
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
                            BackgroundColor = (timeLeft < new TimeSpan(0)) ? Color.FromHex("#BF0A30") : Color.FromHex("#D0A650"),
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
                                            Source = "academicwhite.png",
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
                        StackLayout deadlineLayout = new StackLayout()
                        {
                            Padding = new Thickness(0),
                            Spacing = 0,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.Start
                        };

                        expandDeadline.GestureRecognizers.Add(new TapGestureRecognizer()
                        {
                            Command = new Command(() => { SetDeadline(deadlineLayout, deadline, expandDeadline); })
                        });

                        labelLayout.Children.Add(expandDeadline);
                        deadlineLayout.Children.Add(labelLayout);
                        deadlineLayout.Children.Add(lineLayout);

                        LMSLayout.Children.Add(deadlineLayout);
                    }
                }
            }
        }

        public void SetDeadline(StackLayout deadlineLayout, LMSDeadlines deadline, Image expandDeadline)
        {

            if (deadlineLayout.Children.Count == 2)
            {
                expandDeadline.Source = "up.png";
                TimeSpan timeLeft = deadline.deadline.Subtract(DateTime.Now);
                string remaining;
                if (timeLeft < new TimeSpan(0))
                {
                    remaining = "Overdue by: " + ToReadableString(timeLeft);
                }
                else
                {
                    remaining = "Time remaining: " + ToReadableString(timeLeft);
                }

                    deadlineLayout.Children.Add(new StackLayout()
                {
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.Start,
                    Padding = 10,
                    Spacing = 10,
                    BackgroundColor = Color.White,
                    Children =
                    {
                        new Label()
                        {
                            Text = deadline.Course,
                            FontSize = 15,
                            TextColor = Color.Black,
                            FontAttributes = FontAttributes.Bold,
                            VerticalTextAlignment = TextAlignment.Start,
                            VerticalOptions = LayoutOptions.StartAndExpand,
                            HorizontalOptions = LayoutOptions.StartAndExpand
                        },
                        new StackLayout()
                        {
                            Orientation = StackOrientation.Vertical,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.Start,
                            HeightRequest = 1,
                            BackgroundColor = Color.FromHex("#777777")
                        },
                        new Label()
                        {
                            Text = remaining,
                            FontSize = 15,
                            TextColor = (timeLeft < new TimeSpan(0)) ? Color.Red : Color.Black,
                            VerticalTextAlignment = TextAlignment.Start,
                            VerticalOptions = LayoutOptions.StartAndExpand,
                            HorizontalOptions = LayoutOptions.StartAndExpand
                        },
                        new StackLayout()
                        {
                            Orientation = StackOrientation.Vertical,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.Start,
                            HeightRequest = 1,
                            BackgroundColor = Color.FromHex("#777777")
                        },
                        new Label()
                        {
                            Text = deadline.Description,
                            FontSize = 14,
                            TextColor = Color.Black,
                            VerticalTextAlignment = TextAlignment.Start,
                            VerticalOptions = LayoutOptions.StartAndExpand,
                            HorizontalOptions = LayoutOptions.StartAndExpand
                        }
                    }
                });
            }
            else
            {
                expandDeadline.Source = "down.png";
                deadlineLayout.Children.RemoveAt(2);
            }
        }
        public static string ToReadableString(TimeSpan timeSpan)
        {
            string formatted =
                $"{(timeSpan.Duration().Days > 0 ? $"{Math.Abs(timeSpan.Days):0} day{(timeSpan.Days == 1 ? String.Empty : "s")}, " : string.Empty)}{(timeSpan.Duration().Hours > 0 ? $"{Math.Abs(timeSpan.Hours):0} hour{(timeSpan.Hours == 1 ? String.Empty : "s")}, " : string.Empty)}{(timeSpan.Duration().Minutes > 0 ? $"{Math.Abs(timeSpan.Minutes):0} minute{(timeSpan.Minutes == 1 ? String.Empty : "s")}, " : string.Empty)}{(timeSpan.Duration().Seconds > 0 ? $"{Math.Abs(timeSpan.Seconds):0} second{(timeSpan.Seconds == 1 ? String.Empty : "s")}" : string.Empty)}";

            if (formatted.EndsWith(", ")) formatted = formatted.Substring(0, formatted.Length - 2);

            if (string.IsNullOrEmpty(formatted)) formatted = "0 seconds";

            return formatted;
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
