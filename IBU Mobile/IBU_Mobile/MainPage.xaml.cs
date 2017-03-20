using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IBU_Mobile
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        private string _currentPage = "Overview";
        public MainPage()
        {
            InitializeComponent();

            MasterPage.IBUListView.ItemSelected += (sender, args) =>
            {
                if (MasterPage.IBUListView.SelectedItem != null)
                {
                    Detail =
                        new NavigationPage(new OverviewPage()
                        {
                            Title = ((IBUMenuItem) args.SelectedItem).PageTitle
                        });
                    _currentPage = ((IBUMenuItem) args.SelectedItem).PageTitle;
                    MasterPage.IBUListView.SelectedItem = null;
                    if (Device.OS != TargetPlatform.Windows)
                        IsPresented = false;
                }
            };
        }

        protected override bool OnBackButtonPressed()
        {
            if (IsPresented == false)
                if (_currentPage != "Overview")
                {
                    Detail = new NavigationPage(new OverviewPage() {Title = "Overview"});
                    _currentPage = "Overview";
                }
            if (IsPresented)
                IsPresented = false;
            return true;
        }
    }

}
