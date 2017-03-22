
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using IBU_Mobile.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IBU_Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageMaster : ContentPage
    {
        public ListView IBUListView => IBUMainMenu;
        public MainPageMaster()
        {
            InitializeComponent();
            IBUMainMenu.ItemsSource= new ObservableCollection<IBUMenuItem>(new[]
            {
                    new IBUMenuItem { IconPath = "menuicon.png", PageTitle = "Overview", TargetType = typeof(OverviewPage)},
                    new IBUMenuItem { IconPath = "menuicon.png", PageTitle = "Grades", TargetType = typeof(GradesPage)},
                    new IBUMenuItem { IconPath = "menuicon.png", PageTitle = "Attendance", TargetType = typeof(AttendancePage)},
                    new IBUMenuItem { IconPath = "menuicon.png", PageTitle = "Messages", TargetType = typeof(OverviewPage)},
                    new IBUMenuItem { IconPath = "menuicon.png", PageTitle = "LMS", TargetType = typeof(OverviewPage)},
                    new IBUMenuItem { IconPath = "menuicon.png", PageTitle = "Document Request", TargetType = typeof(OverviewPage)},
                });
            
            ;
        }

        private void LogOutTapped(object sender, EventArgs e)
        {
            CurrentApp.Current.LogOutAction.Invoke();
        }
    }
    public class IBUMenuItem
    {
        public string IconPath { get; set; }
        public string PageTitle { get; set; }
        public Type TargetType { get; set; }
    }
}
