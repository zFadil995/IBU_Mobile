using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using System.Threading.Tasks;

namespace IBU_Mobile.Droid
{
    [Activity(Label = "IBU Mobile", Icon = "@drawable/icon", Theme = "@style/MainTheme.Splash", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
        }

        protected override void OnResume()
        {
            base.OnResume();

            Task startupWork = new Task(() =>
            {
                StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            });
            startupWork.Start();
        }
    }
}