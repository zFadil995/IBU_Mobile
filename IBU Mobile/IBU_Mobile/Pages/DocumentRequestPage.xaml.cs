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
        private bool TR = false, EN = false, BA = false;
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
            TRImage.GestureRecognizers.Clear();
            TRImage.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command((() =>
                {
                    if (!TR)
                    {
                        TR = !TR;
                        TRImage.Source = "checkfull.png";
                    }
                    else if (TR)
                    {
                        TR = !TR;
                        TRImage.Source = "checkempty.png";
                    }
                }))
            });
            ENImage.GestureRecognizers.Clear();
            ENImage.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command((() =>
                {
                    if (!EN)
                    {
                        EN = !EN;
                        ENImage.Source = "checkfull.png";
                    }
                    else if (EN)
                    {
                        EN = !EN;
                        ENImage.Source = "checkempty.png";
                    }
                }))
            });
            BAImage.GestureRecognizers.Clear();
            BAImage.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command((() =>
                {
                    if (!BA)
                    {
                        BA = !BA;
                        BAImage.Source = "checkfull.png";
                    }
                    else if (BA)
                    {
                        BA = !BA;
                        BAImage.Source = "checkempty.png";
                    }
                }))
            });
            DocumentTypePicker.GestureRecognizers.Clear();
            DocumentTypePicker.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command((() =>
                {
                    if (DocumentType.Text == "STUDENT DOCUMENT")
                    {
                        DocumentType.Text = "TRANSCRIPT";
                        TRImage.Source = "checkfalse.png";
                        TRImage.IsEnabled = false;
                        BAImage.Source = "checkfalse.png";
                        BAImage.IsEnabled = false;

                    }
                    else if (DocumentType.Text == "TRANSCRIPT")
                    {
                        DocumentType.Text = "STUDENT DOCUMENT";
                        TRImage.Source = (TR)? "checkfull.png" : "checkempty.png";
                        TRImage.IsEnabled = true;
                        BAImage.Source = (BA) ? "checkfull.png" : "checkempty.png";
                        BAImage.IsEnabled = true;
                    }
                }))
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

        private void DocumentRequestClicked(object sender, EventArgs e)
        {
            if (DocumentType.Text == "STUDENT DOCUMENT")
            {
                RequestDocument(1, (TR)?1:0, (EN) ? 1 : 0, (BA) ? 1 : 0, ExplanationEntry.Text);
            }
            else if (DocumentType.Text == "TRANSCRIPT")
            {
                RequestDocument(2, 0, (EN) ? 1 : 0, 0, ExplanationEntry.Text);
            }
        }

        private async void RequestDocument(int type, int tr, int en, int ba, string description)
        {
            TRImage.IsEnabled = false;
            ENImage.IsEnabled = false;
            BAImage.IsEnabled = false;
            ExplanationEntry.IsEnabled = false;
            if (tr == 0 && en == 0 && ba == 0)
            {
                await DisplayAlert("Warning", "Please select a language for your document", "OK");
            }
            else if (description == String.Empty || description == null)
            {
                await DisplayAlert("Warning", "Please include a description for your document", "OK");
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
                        if (PreviousRequestsLayout.Children.Count == 2)
                            expandPreviousRequests();
                    }
                    else
                    {
                        await DisplayAlert("Warning", "An error occured while requesting your document", "OK");
                    }
                }
                catch (Exception e)
                {
                    string exception = e.Message;
                    await DisplayAlert("Warning", "An error occured while requesting your document", "OK");
                }
            }
            TRImage.IsEnabled = true;
            ENImage.IsEnabled = true;
            BAImage.IsEnabled = true;
            ExplanationEntry.IsEnabled = true;
        }

        private class ResultData
        {
            public int Result { get; set; }
        }
    }
}
