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
    public partial class DocumentRequestPage : ContentPage
    {
        public DocumentRequestPage()
        {
            InitializeComponent();
            SetUp();

            ToolbarItems.Add(IBUData.DocumentsToolbar);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            IBUData.CurrentPage = this;
        }

        private void SetUp()
        {
            expandPreviousImage.GestureRecognizers.Clear();
            expandPreviousImage.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(expandPreviousRequests)
            });
        }

        public Action SetUpAction
        {
            get { return new Action(() => {
                expandPreviousRequests();
                expandPreviousRequests();
            }); }
        }

        private void expandPreviousRequests()
        {
            if (PreviousRequestsLayout.Children.Count == 2)
            {
                expandPreviousImage.Source = "up.png";
                StackLayout documentsLayout = new StackLayout()
                {
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.Start,
                    Padding = 10,
                    Spacing = 10,
                    BackgroundColor = Color.White
                };
                if (IBUData.DocumentsData.Documents.Length == 0)
                {
                    documentsLayout.Children.Add(new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.Start,
                        BackgroundColor = Color.FromHex("#BCE8F1"),
                        Margin = 5,
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
                                        Text = "You don't have any previous requests.",
                                        TextColor = Color.FromHex("#32708F"),
                                        HorizontalTextAlignment = TextAlignment.Start,
                                        VerticalTextAlignment = TextAlignment.Center,
                                        FontSize = 14
                                    }
                                }
                            }
                        }
                    });
                }
                else
                {
                    Grid documentsGrid = new Grid()
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.Start,
                        BackgroundColor = Color.White,
                        Padding = new Thickness(10),
                        RowSpacing = 5
                    };
                    documentsGrid.ColumnDefinitions.Add(new ColumnDefinition()
                    {
                        Width = new GridLength(1, GridUnitType.Star)
                    });
                    documentsGrid.RowDefinitions.Add(new RowDefinition()
                    {
                        Height = new GridLength(1, GridUnitType.Auto)
                    });
                    documentsGrid.ColumnDefinitions.Add(new ColumnDefinition()
                    {
                        Width = new GridLength(1, GridUnitType.Auto)
                    });
                    documentsGrid.ColumnDefinitions.Add(new ColumnDefinition()
                    {
                        Width = new GridLength(1, GridUnitType.Auto)
                    });
                    documentsGrid.ColumnDefinitions.Add(new ColumnDefinition()
                    {
                        Width = new GridLength(1, GridUnitType.Auto)
                    });
                    documentsGrid.ColumnDefinitions.Add(new ColumnDefinition()
                    {
                        Width = new GridLength(1, GridUnitType.Auto)
                    });
                    documentsGrid.ColumnDefinitions.Add(new ColumnDefinition()
                    {
                        Width = new GridLength(1, GridUnitType.Auto)
                    });

                    documentsGrid.Children.Add(
                        new Label()
                        {
                            Text = "Document Type",
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.FromHex("#555555"),
                            FontSize = 12
                        },
                        0, 0
                    );
                    documentsGrid.Children.Add(
                        new Image()
                        {
                            Source = "tr.png",
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            HeightRequest = 15
                        },
                        1, 0
                    );
                    documentsGrid.Children.Add(
                        new Image()
                        {
                            Source = "en.png",
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            HeightRequest = 15
                        },
                        2, 0
                    );
                    documentsGrid.Children.Add(
                        new Image()
                        {
                            Source = "ba.png",
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            HeightRequest = 15
                        },
                        3, 0
                    );
                    documentsGrid.Children.Add(
                        new Label()
                        {
                            Text = " Date ",
                            FontAttributes = FontAttributes.Bold,
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            HorizontalTextAlignment = TextAlignment.Center,
                            VerticalTextAlignment = TextAlignment.Center,
                            TextColor = Color.FromHex("#555555"),
                            FontSize = 12
                        },
                        4, 0
                    );
                    documentsGrid.Children.Add(
                        new Label()
                        {
                            Text = " Status",
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.FromHex("#555555"),
                            FontSize = 12
                        },
                        5, 0
                    );


                    documentsGrid.RowDefinitions.Add(new RowDefinition()
                    {
                        Height = new GridLength(1, GridUnitType.Auto)
                    });
                    documentsGrid.Children.Add(new StackLayout()
                        {
                            Orientation = StackOrientation.Vertical,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.Start,
                            HeightRequest = 1,
                            BackgroundColor = Color.FromHex("#111111")
                        }, 0, 6, 1, 2
                    );

                    int row = 1;
                    foreach (Documents document in IBUData.DocumentsData.Documents)
                    {
                        documentsGrid.RowDefinitions.Add(new RowDefinition()
                        {
                            Height = new GridLength(1, GridUnitType.Auto)
                        });
                        row++;

                        documentsGrid.Children.Add(
                            new Label()
                            {
                                Text = (document.DocumentType == 1)?"Student Document":"Transcript",
                                TextColor = Color.FromHex("#777777"),
                                VerticalTextAlignment = TextAlignment.Center,
                                FontSize = 12
                            },
                            0, row
                        );
                        documentsGrid.Children.Add(
                            new Label()
                            {
                                Text = (document.Turkish == 0)?"x":document.Turkish.ToString(),
                                HorizontalOptions = LayoutOptions.CenterAndExpand,
                                HorizontalTextAlignment = TextAlignment.Center,
                                VerticalTextAlignment = TextAlignment.Center,
                                TextColor = Color.FromHex("#777777"),
                                FontSize = 12
                            },
                            1, row
                        );
                        documentsGrid.Children.Add(
                            new Label()
                            {
                                Text = (document.English == 0) ? "x" : document.English.ToString(),
                                HorizontalOptions = LayoutOptions.CenterAndExpand,
                                HorizontalTextAlignment = TextAlignment.Center,
                                VerticalTextAlignment = TextAlignment.Center,
                                TextColor = Color.FromHex("#777777"),
                                FontSize = 12
                            },
                            2, row
                        );
                        documentsGrid.Children.Add(
                            new Label()
                            {
                                Text = (document.Bosnian == 0) ? "x" : document.Bosnian.ToString(),
                                HorizontalOptions = LayoutOptions.CenterAndExpand,
                                HorizontalTextAlignment = TextAlignment.Center,
                                VerticalTextAlignment = TextAlignment.Center,
                                TextColor = Color.FromHex("#777777"),
                                FontSize = 12
                            },
                            3, row
                        );
                        documentsGrid.Children.Add(
                            new Label()
                            {
                                Text = document.Date,
                                HorizontalOptions = LayoutOptions.CenterAndExpand,
                                HorizontalTextAlignment = TextAlignment.Center,
                                TextColor = Color.FromHex("#777777"),
                                VerticalTextAlignment = TextAlignment.Center,
                                FontSize = 12
                            },
                            4, row
                        );
                        documentsGrid.Children.Add(
                            new Image()
                            {
                                Source = (document.Status == 0) ? "notdone.png" : "done.png",
                                HorizontalOptions = LayoutOptions.CenterAndExpand,
                                VerticalOptions = LayoutOptions.CenterAndExpand,
                                HeightRequest = 12
                            },
                            5, row
                        );

                        documentsGrid.RowDefinitions.Add(new RowDefinition()
                        {
                            Height = new GridLength(1, GridUnitType.Auto)
                        });
                        row++;
                        documentsGrid.Children.Add(new StackLayout()
                        {
                            Orientation = StackOrientation.Vertical,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.Start,
                            HeightRequest = 1,
                            BackgroundColor = Color.FromHex("#777777")
                        }, 0, 6, row, row + 1
                        );
                    }
                    documentsLayout.Children.Add(documentsGrid);
                }
                PreviousRequestsLayout.Children.Add(documentsLayout);
            }
            else
            {
                expandPreviousImage.Source = "down.png";

                PreviousRequestsLayout.Children.RemoveAt(2);
            }
        }

        private void DocumentTypeClicked(object sender, EventArgs e)
        {
            if (DocumentTypeButton.Text == "STUDENT DOCUMENT")
            {
                DocumentTypeButton.Text = "TRANSCRIPT";
                TRButton.IsEnabled = false;
                BAButton.IsEnabled = false;
            }
            else if (DocumentTypeButton.Text == "TRANSCRIPT")
            {
                DocumentTypeButton.Text = "STUDENT DOCUMENT";
                TRButton.IsEnabled = true;
                BAButton.IsEnabled = true;
            }
        }

        private void DocumentRequestClicked(object sender, EventArgs e)
        {
            if (DocumentTypeButton.Text == "STUDENT DOCUMENT")
            {
                RequestDocument(1, (TRButton.BackgroundColor == Color.White)?1:0, (ENButton.BackgroundColor == Color.White) ? 1 : 0, (BAButton.BackgroundColor == Color.White) ? 1 : 0, ExplanationEntry.Text);
            }
            else if (DocumentTypeButton.Text == "TRANSCRIPT")
            {
                RequestDocument(2, 0, (ENButton.BackgroundColor == Color.White) ? 1 : 0, 0, ExplanationEntry.Text);
            }
        }

        private async void RequestDocument(int type, int tr, int en, int ba, string description)
        {
            DocumentTypeButton.IsEnabled = false;
            TRButton.IsEnabled = false;
            ENButton.IsEnabled = false;
            BAButton.IsEnabled = false;
            ExplanationEntry.IsEnabled = false;
            if (tr == 0 && en == 0 && ba == 0)
            {
                await DisplayAlert("No language selected", "Please select a language for your document", "OK");
            }
            else if (description == String.Empty || description == null)
            {
                await DisplayAlert("No description included", "Please include a description for your document", "OK");
            }
            else
            {
                try
                {
                    var client = new RestClient("http://54.244.213.136/request.php");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    request.AddParameter("Token", Settings.Token, ParameterType.GetOrPost);
                    request.AddParameter("DocumentType", type, ParameterType.GetOrPost);
                    request.AddParameter("Turkish", tr, ParameterType.GetOrPost);
                    request.AddParameter("English", en, ParameterType.GetOrPost);
                    request.AddParameter("Bosnian", ba, ParameterType.GetOrPost);
                    request.AddParameter("Date", DateTime.Now.ToString("yyyy-MM-dd"), ParameterType.GetOrPost);
                    request.AddParameter("Description", description, ParameterType.GetOrPost);
                    IRestResponse response = await client.Execute(request);
                    string data = response.Content;
                    ResultData results = JsonConvert.DeserializeObject<ResultData>(data);
                    if (results.Result == 1)
                    {
                        await DisplayAlert("Success", "You have succesfully requested a document", "OK");
                        IBUData.UpdateDocuments();
                    }
                    else
                    {
                        await DisplayAlert("Error", "An error occured while requesting your document", "OK");
                    }
                }
                catch (Exception e)
                {
                    string exception = e.Message;
                    await DisplayAlert("Error", "An error occured while requesting your document", "OK");
                }
            }
            DocumentTypeButton.IsEnabled = true;
            TRButton.IsEnabled = true;
            ENButton.IsEnabled = true;
            BAButton.IsEnabled = true;
            ExplanationEntry.IsEnabled = true;
        }

        private void TRClicked(object sender, EventArgs e)
        {
            if (((Button) sender).BackgroundColor == Color.FromHex("#CCCCCC"))
            {
                ((Button) sender).BackgroundColor = Color.White;
            }
            else if (((Button)sender).BackgroundColor == Color.White)
            {
                ((Button) sender).BackgroundColor = Color.FromHex("#CCCCCC");
            }
        }

        private void ENClicked(object sender, EventArgs e)
        {
            if (((Button)sender).BackgroundColor == Color.FromHex("#CCCCCC"))
            {
                ((Button)sender).BackgroundColor = Color.White;
            }
            else if (((Button)sender).BackgroundColor == Color.White)
            {
                ((Button)sender).BackgroundColor = Color.FromHex("#CCCCCC");
            }
        }

        private void BAClicked(object sender, EventArgs e)
        {
            if (((Button)sender).BackgroundColor == Color.FromHex("#CCCCCC"))
            {
                ((Button)sender).BackgroundColor = Color.White;
            }
            else if (((Button)sender).BackgroundColor == Color.White)
            {
                ((Button)sender).BackgroundColor = Color.FromHex("#CCCCCC");
            }
        }

        private class ResultData
        {
            public int Result { get; set; }
        }
    }
}
