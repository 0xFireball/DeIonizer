using System;
using Android.App;
using Android.OS;
using Android.Content.PM;
using Avalonia.Android;
using Avalonia.Controls;
using Avalonia;
using DeIonizer.SharedUI;
using DeIonizer.SharedUI.Views;

namespace DeIonizer.Android
{
    [Activity(Label = "DeIonizer.Android", MainLauncher = true, Icon = "@drawable/icon",
        LaunchMode = LaunchMode.SingleInstance)]
    public class MainActivity : AvaloniaActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            if (Avalonia.Application.Current == null)
            {
                AppBuilder.Configure(new App())
                    .UseAndroid()
                    .SetupWithoutStarting();
                Content = new MainWindow();
            }
            base.OnCreate(bundle);
        }
    }
}