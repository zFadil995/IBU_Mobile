
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
                    new IBUMenuItem { IconPath = "menuicon.png", PageTitle = "Overview" },
                    new IBUMenuItem { IconPath = "menuicon.png", PageTitle = "Grades" },
                    new IBUMenuItem { IconPath = "menuicon.png", PageTitle = "Attendance" },
                    new IBUMenuItem { IconPath = "menuicon.png", PageTitle = "Messages" },
                    new IBUMenuItem { IconPath = "menuicon.png", PageTitle = "LMS" },
                    new IBUMenuItem { IconPath = "menuicon.png", PageTitle = "Document Request" },
                });
        }

        private void LogOutTapped(object sender, EventArgs e)
        {
            //
        }
    }
    public class IBUMenuItem
    {
        public string IconPath { get; set; }
        public string PageTitle { get; set; }
    }
}
