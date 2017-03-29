using System;
using System.Linq;
using IBU_Mobile.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IBU_Mobile
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OverviewPage : ContentPage
    {
        public OverviewPage()
        {
            InitializeComponent();
            SetUp();

            ToolbarItems.Add(IBUData.OverviewToolbar);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            IBUData.CurrentPage = this;
        }

        private void SetUp()
        {
            CoursesList.Children.Clear();
            if (IBUData.OverviewData != null)
            {
                foreach(string course in IBUData.OverviewData.Courses)
                {
                    CoursesList.Children.Add(new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        Padding = new Thickness(7),
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Children =
                        {
                            new Image() {Source = "subject.png", HeightRequest = 15,},
                            new Label()
                            {
                                VerticalOptions = LayoutOptions.FillAndExpand,
                                VerticalTextAlignment = TextAlignment.Center,
                                Text = course,
                                TextColor = Color.Black,
                                Margin = new Thickness(10, 0, 0, 0),
                                FontSize = 14
                            }
                        }
                    });
                }

                StudentID.Text = IBUData.OverviewData.StudentID;
                Email.Text = IBUData.OverviewData.Email;
                Attendance.Text = IBUData.OverviewData.Attendance;
                Nationality.Text = IBUData.OverviewData.Nationality;

                TotalPrice.Text = IBUData.OverviewData.TotalPrice;
                TotalPaid.Text = IBUData.OverviewData.TotalPaid;
                TotalToPay.Text = IBUData.OverviewData.TotalToPay;
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
