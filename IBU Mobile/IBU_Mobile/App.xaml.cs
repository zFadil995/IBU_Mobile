using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBU_Mobile.Helpers;
using Xamarin.Forms;

namespace IBU_Mobile
{
    public static class CurrentApp
    {
        public static App Current;
    }
    public partial class App : Application
    {
        public App()
        {
            CurrentApp.Current = this;
            InitializeComponent();
            LoadApp();
        }

        private void LoadApp()
        {
            if (Settings.Token == string.Empty)
            {
                MainPage = new LoginPage();
            }
            else
            {
                IBUData.SetUpData();

                MainPage = new MainPage();

                IBUData.UpdateData();
            }
        }

        public Action SetUpAction
        {
            get
            {
                return new Action(() => LoadApp());
            }
        }

        public Action LogOutAction
        {
            get
            {
                return new Action(() => { Settings.UserData = String.Empty; Settings.OverviewData = String.Empty; Settings.GradesData = String.Empty; Settings.Token = String.Empty; LoadApp(); });
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
