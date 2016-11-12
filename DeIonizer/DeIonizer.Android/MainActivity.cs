using Android.App;
using Android.OS;
using Avalonia;
using DeIonizer.SharedUI;

namespace DeIonizer.Android
{
    [Activity(Label = "DeIonizer.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            App app;
            if (Avalonia.Application.Current != null)
            {
                app = (App)Avalonia.Application.Current;
            }
            else
            {
                app = new App();
                AppBuilder.Configure(app)
                    .UseAndroid()
                    .UseSkia()
                    .SetupWithoutStarting();
            }
        }
    }
}