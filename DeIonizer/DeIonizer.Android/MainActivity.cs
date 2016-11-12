using Android.App;
using Android.OS;
using Android.Content.PM;

using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml;
using Avalonia.Android.Platform.Specific;
using Avalonia.Controls;
using Avalonia;

namespace DeIonizer.Android
{
    [Activity(Label = "DeIonizer.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            DIAndroidApp app;
            if (Avalonia.Application.Current != null)
            {
                app = (DIAndroidApp)Avalonia.Application.Current;
            }
            else
            {
                app = new DIAndroidApp();
                AppBuilder.Configure(app)
                    .UsePlatformDetect()
                    .UseSkia()
                    .SetupWithoutStarting();
            }
        }
    }
}