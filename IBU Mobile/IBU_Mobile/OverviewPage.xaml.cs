using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
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
        }

        private void SetUp()
        {
            Overview overview = JsonConvert.DeserializeObject<Overview>(JSONData.OverviewData);

            CoursesList.ItemsSource = overview.Courses;
            CoursesList.HeightRequest = 45 * overview.Courses.Length;

            CoursesList.ItemSelected += (sender, args) =>
            {
                CoursesList.SelectedItem = null;
            };

            StudentID.Text = overview.StudentID;
            Email.Text = overview.Email;
            Attendance.Text = overview.Attendance;
            Nationality.Text = overview.Nationality;

            TotalPrice.Text = overview.TotalPrice;
            TotalPaid.Text = overview.TotalPaid;
            TotalToPay.Text = overview.TotalToPay;

        }
    }

    public class Overview
    {
        public string[] Courses { get; set; }
        public string StudentID { get; set; }
        public string Email { get; set; }
        public string Attendance { get; set; }
        public string Nationality { get; set; }
        public string TotalPrice { get; set; }
        public string TotalPaid { get; set; }
        public string TotalToPay { get; set; }

    }
}
