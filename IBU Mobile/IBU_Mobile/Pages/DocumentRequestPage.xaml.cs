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
            documentTypePicker.Items.Add("Student Document");
            documentTypePicker.Items.Add("Transcript");



            expandPreviousImage.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(() => { expandPreviousRequests(); }) });
        }

        public Action SetUpAction
        {
            get { return new Action(SetUp); }
        }

        private void expandPreviousRequests()
        {
            if (PreviousRequestsLayout.Children.Count == 2)
            {
                expandPreviousImage.Source = "up.png";
                if (IBUData.DocumentsData.Documents.Length == 0)
                {
                    PreviousRequestsLayout.Children.Add(new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.Start,
                        BackgroundColor = Color.FromHex("#BCE8F1"),
                        Margin = 15,
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
                    
                }
            }
            else
            {
                expandPreviousImage.Source = "down.png";

                PreviousRequestsLayout.Children.RemoveAt(2);
            }
        }
    }
}
