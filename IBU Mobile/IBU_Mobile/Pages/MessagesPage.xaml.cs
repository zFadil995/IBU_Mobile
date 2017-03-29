using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using IBU_Mobile.Helpers;
using Newtonsoft.Json;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IBU_Mobile.Pages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessagesPage : ContentPage
    {
        public MessagesPage()
        {
            InitializeComponent();
            SetUp();

            ToolbarItems.Add(IBUData.MessagesToolbar);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            IBUData.CurrentPage = this;
        }

        private void SetUp()
        {
            if (IBUData.MessagesData != null)
            {
                MessagesLayout.Children.Clear();
                if (IBUData.MessagesData.Messages.Length == 0)
                {
                    MessagesLayout.Children.Add(new StackLayout()
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
                                BackgroundColor = Color.FromHex("#D9EDF7"),
                                Padding = new Thickness(10, 5, 10, 5),
                                Children =
                                {
                                    new Label()
                                    {
                                        Text = "You don't have any messages.",
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
                    foreach (Messages message in IBUData.MessagesData.Messages)
                    {
                        Image expandMessage = new Image()
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
                                BackgroundColor = Color.FromHex("#F0C670")
                            };
                        Label TitleLabel =
                            new Label()
                            {
                                Text = message.Title,
                                FontSize = 14,
                                TextColor = Color.White,
                                FontAttributes = (message.Status == 0) ? FontAttributes.Bold : FontAttributes.None,
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
                                            Source = "messageswhite.png",
                                            HeightRequest = 20,
                                            Margin = new Thickness(1),
                                            VerticalOptions = LayoutOptions.FillAndExpand,
                                            HorizontalOptions = LayoutOptions.Start
                                        },
                                        TitleLabel
                                    }
                                },

                            }
                        };
                        StackLayout MessageLayout = new StackLayout()
                        {
                            Padding = new Thickness(0),
                            Spacing = 0,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.Start
                        };

                        expandMessage.GestureRecognizers.Add(new TapGestureRecognizer()
                        {
                            Command = new Command(() => { SetMessage(MessageLayout, message, TitleLabel, expandMessage); })
                        });

                        labelLayout.Children.Add(expandMessage);
                        MessageLayout.Children.Add(labelLayout);
                        MessageLayout.Children.Add(lineLayout);

                        MessagesLayout.Children.Add(MessageLayout);
                    }
                }
            }
        }

        public void SetMessage(StackLayout messageLayout, Messages message, Label titleLabel, Image expandMessage)
        {

            if (messageLayout.Children.Count == 2)
            {
                expandMessage.Source = "up.png";
                if(message.Status == 0)
                    SetRead(titleLabel, message);
                messageLayout.Children.Add(new StackLayout()
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
                            Text = message.Contents,
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
                expandMessage.Source = "down.png";
                messageLayout.Children.RemoveAt(2);
            }
        }

        private async void SetRead(Label TitleLabel, Messages message)
        {
            try
            {
                var client = new RestClient("http://54.244.213.136/markread.php");
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("Token", Settings.Token, ParameterType.GetOrPost);
                request.AddParameter("MessageID", message.MessageID, ParameterType.GetOrPost);
                IRestResponse response = await client.Execute(request);
                string data = response.Content;
                ResultData results = JsonConvert.DeserializeObject<ResultData>(data);
                if (results.Result == 1)
                {
                    if (TitleLabel.FontAttributes == FontAttributes.Bold)
                        TitleLabel.FontAttributes = FontAttributes.None;
                    message.Status = 1;
                    ((MainPageMaster) IBUData.MainPage)?.SetMessagesAction();
                }
            }
            catch (Exception e)
            {
                string exception = e.Message;
            }
        }

        public Action SetUpAction
        {
            get { return new Action(SetUp); }
        }

        private class ResultData
        {
            public int Result { get; set; }
        }
    }

}
