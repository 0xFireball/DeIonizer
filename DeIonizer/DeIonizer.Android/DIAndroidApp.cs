using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using Avalonia.Themes.Default;
using DeIonizer.Views;
using System;

namespace DeIonizer.Android
{
    internal class DIAndroidApp : Application
    {
        public void Run()
        {
            Styles.Add(new DefaultTheme());
            var loader = new AvaloniaXamlLoader();
            var baseLight = (IStyle)loader.Load(
                new Uri("resm:nkyUI.Themes.BaseLight.xaml?assembly=nkyUI")
            );

            var blueAccent = (IStyle)loader.Load(
                new Uri("resm:nkyUI.Themes.Accents.Blue.xaml?assembly=nkyUI")
            );

            var kyuiControls = (IStyle)loader.Load(
                new Uri("resm:nkyUI.Styles.Controls.xaml?assembly=nkyUI")
            );

            Styles.Add(baseLight);
            Styles.Add(blueAccent);
            Styles.Add(kyuiControls);

            var wnd = new MainWindow();
            //wnd.AttachDevTools();

            Run(wnd);
        }

        private static Window CreateSimpleWindow()
        {
            throw new NotImplementedException();
        }
    }
}