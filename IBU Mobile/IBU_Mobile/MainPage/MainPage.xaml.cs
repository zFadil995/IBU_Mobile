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
        public MainPage()
        {
            InitializeComponent();

            MasterPage.IBUListView.ItemSelected += (sender, args) =>
            {
                if (MasterPage.IBUListView.SelectedItem != null)
                {
                    if (((IBUMenuItem) args.SelectedItem).PageTitle == "Grades")
                    {
                        Detail =
                            new NavigationPage(new GradesPage());
                    }
                    else
                    {
                        Detail =
                            new NavigationPage(new OverviewPage());
                    }
                    MasterPage.IBUListView.SelectedItem = null;
                    if (Device.OS != TargetPlatform.Windows)
                        IsPresented = false;
                }
            };
        }

        protected override bool OnBackButtonPressed()
        {
            if (IsPresented == false)
                if (Detail.GetType() != typeof(OverviewPage))
                {
                    Detail = new NavigationPage(new OverviewPage());
                }
            if (IsPresented)
                IsPresented = false;
            return true;
        }
    }

}
