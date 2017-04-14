
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using IBU_Mobile.Helpers;
using IBU_Mobile.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IBU_Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageMaster : ContentPage
    {
        public ListView IBUListView => IBUMainMenu;
        public Image IBUMessagesIcon => MessagesIcon;
        public MainPageMaster()
        {
            InitializeComponent();
            IBUData.MainPage = this;
            IBUMainMenu.ItemsSource= new ObservableCollection<IBUMenuItem>(new[]
            {
                    new IBUMenuItem { IconPath = "overview.png", PageTitle = "Overview", TargetType = typeof(OverviewPage)},
                    new IBUMenuItem { IconPath = "grades.png", PageTitle = "Grades", TargetType = typeof(GradesPage)},
                    new IBUMenuItem { IconPath = "attendance.png", PageTitle = "Attendance", TargetType = typeof(AttendancePage)},
                    new IBUMenuItem { IconPath = "messages.png", PageTitle = "Messages", TargetType = typeof(MessagesPage)},
                    new IBUMenuItem { IconPath = "lms.png", PageTitle = "LMS", TargetType = typeof(LMSPage)},
                    new IBUMenuItem { IconPath = "document.png", PageTitle = "Document Request", TargetType = typeof(DocumentRequestPage)},
                });
            
            SetUp();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void LogOutTapped(object sender, EventArgs e)
        {
            CurrentApp.Current.LogOutAction.Invoke();
        }

        private async void SetUp()
        {
            StudentImage.HeightRequest = await CircleHeight();
            StudentImage.WidthRequest = await CircleHeight();
            if (IBUData.UserData != null)
            {
                StudentImage.Source = new UriImageSource
                {
                    Uri = new Uri(IBUData.UserData.ImagePath),
                    CachingEnabled = true,
                    CacheValidity = new TimeSpan(180, 0, 0, 0)
                };
                StudentName.Text = IBUData.UserData.FirstName + " " + IBUData.UserData.LastName;
                StudentID.Text = IBUData.UserData.StudentID;
                SetMessages();
            }

        }

        private async Task<double> CircleHeight()
        {
            while (BannerImage.Height < 0)
            {
                await Task.Delay(100);
            }
            return BannerImage.Height * 0.4;
        }
        private void SetMessages()
        {
            MessagesIcon.Source = "messages.png";
            if (IBUData.MessagesData != null)
            {
                foreach (Messages message in IBUData.MessagesData.Messages)
                {
                    if (message.Status == 0)
                    {
                        MessagesIcon.Source = "messagesnew.png";
                        return;
                    }

                }
            }
        }

        public Action SetUpAction
        {
            get
            {
                return new Action(SetUp);
            }
        }

        public Action SetMessagesAction
        {
            get
            {
                return new Action(SetMessages);
            }
        }

        private void MessagesTapped(object sender, EventArgs e)
        {
        }
    }
    public class IBUMenuItem
    {
        public string IconPath { get; set; }
        public string PageTitle { get; set; }
        public Type TargetType { get; set; }
    }
}
