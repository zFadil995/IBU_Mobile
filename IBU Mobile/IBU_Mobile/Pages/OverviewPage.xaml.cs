using System;
using System.Linq;
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
            CoursesList.ItemsSource = IBUData.Overview.Courses;
            CoursesList.HeightRequest = 45 * IBUData.Overview.Courses.Length;

            CoursesList.ItemSelected += (sender, args) =>
            {
                CoursesList.SelectedItem = null;
            };

            StudentID.Text = IBUData.Overview.StudentID;
            Email.Text = IBUData.Overview.Email;
            Attendance.Text = IBUData.Overview.Attendance;
            Nationality.Text = IBUData.Overview.Nationality;

            TotalPrice.Text = IBUData.Overview.TotalPrice;
            TotalPaid.Text = IBUData.Overview.TotalPaid;
            TotalToPay.Text = IBUData.Overview.TotalToPay;

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
