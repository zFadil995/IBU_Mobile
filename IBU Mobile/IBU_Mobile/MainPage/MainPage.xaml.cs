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
                MasterPage.IBUListView.IsEnabled = false;
                if (MasterPage.IBUListView.SelectedItem != null)
                {
                    Detail = new NavigationPage((Page) Activator.CreateInstance(((IBUMenuItem) MasterPage.IBUListView.SelectedItem).TargetType));
                    MasterPage.IBUListView.SelectedItem = null;
                    IsPresented = false;
                }
                MasterPage.IBUListView.IsEnabled = true;
            };
        }

        protected override bool OnBackButtonPressed()
        {
            Type currentType = ((NavigationPage) Detail).CurrentPage.GetType();
            if (IsPresented == false)
                if (currentType != typeof(OverviewPage))
                {
                    Detail = new NavigationPage(new OverviewPage());
                }
            if (IsPresented)
                IsPresented = false;
            return true;
        }
    }

}
