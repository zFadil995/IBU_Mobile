using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBU_Mobile.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IBU_Mobile
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();

            TapGestureRecognizer messagesTapped = new TapGestureRecognizer();
            messagesTapped.Tapped += (s, e) =>
            {
                if (((NavigationPage) Detail).CurrentPage.GetType() != typeof(MessagesPage))
                {
                    Detail = new NavigationPage(new MessagesPage()){BarBackgroundColor = Color.FromHex("#0052A5")};
                }
                if (Device.OS != TargetPlatform.Windows)
                    IsPresented = false;
            };
            MasterPage.IBUMessagesIcon.GestureRecognizers.Add(messagesTapped);


            MasterPage.IBUListView.ItemSelected += (sender, args) =>
            {
                MasterPage.IBUListView.IsEnabled = false;
                if (MasterPage.IBUListView.SelectedItem != null)
                {
                    if (((NavigationPage) Detail).CurrentPage.GetType() !=
                        ((IBUMenuItem) MasterPage.IBUListView.SelectedItem).TargetType)
                    {
                        Detail =
                            new NavigationPage(
                                (Page)
                                Activator.CreateInstance(((IBUMenuItem) MasterPage.IBUListView.SelectedItem).TargetType))
                            { BarBackgroundColor = Color.FromHex("#0052A5") };
                    }
                    MasterPage.IBUListView.SelectedItem = null;
                    if (Device.OS != TargetPlatform.Windows)
                        IsPresented = false;
                }
                MasterPage.IBUListView.IsEnabled = true;
            };
        }

        protected override bool OnBackButtonPressed()
        {
            if (IsPresented == false)
                if (((NavigationPage) Detail).CurrentPage.GetType() != typeof(OverviewPage))
                {
                    Detail = new NavigationPage(new OverviewPage()) { BarBackgroundColor = Color.FromHex("#0052A5") };
                }
            if (IsPresented)
                IsPresented = false;
            return true;
        }
    }

}
