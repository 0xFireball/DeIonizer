using Avalonia;
using Avalonia.Controls;
using Avalonia.Diagnostics;
using Avalonia.Logging.Serilog;
using Avalonia.Markup.Xaml;
using DeIonizer.Views;
using Serilog;

namespace DeIonizer
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            base.Initialize();
        }

        public static void AttachDevTools(Window window)
        {
#if DEBUG
            DevTools.Attach(window);
#endif
        }
    }
}