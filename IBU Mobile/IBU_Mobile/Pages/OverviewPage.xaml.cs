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
            IBUData.CurrentPage = this;
            SetUp();
        }

        private void SetUp()
        {
            CoursesList.Children.Clear();
            if (IBUData.Overview != null)
            {
                foreach(string course in IBUData.Overview.Courses)
                {
                    CoursesList.Children.Add(new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        Padding = new Thickness(7),
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Children =
                        {
                            new Image() {Source = "menuicon.png", HeightRequest = 15,},
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

                StudentID.Text = IBUData.Overview.StudentID;
                Email.Text = IBUData.Overview.Email;
                Attendance.Text = IBUData.Overview.Attendance;
                Nationality.Text = IBUData.Overview.Nationality;

                TotalPrice.Text = IBUData.Overview.TotalPrice;
                TotalPaid.Text = IBUData.Overview.TotalPaid;
                TotalToPay.Text = IBUData.Overview.TotalToPay;
            }
        }

        public Action SetUpAction
        {
            get
            {
                return new Action(() => SetUp());
            }
        }
    }
}
