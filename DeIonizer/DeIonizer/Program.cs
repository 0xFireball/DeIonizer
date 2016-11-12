using Avalonia.Controls;
using Avalonia.Logging.Serilog;
using DeIonizer.SharedUI;
using DeIonizer.SharedUI.Views;
using Serilog;

namespace DeIonizer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            InitializeLogging();
            AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .Start<MainWindow>();
        }

        private static void InitializeLogging()
        {
#if DEBUG

            SerilogLogger.Initialize(new LoggerConfiguration()
                .MinimumLevel.Warning()
                .WriteTo.Trace(outputTemplate: "{Area}: {Message}")
                .CreateLogger());

#endif
        }
    }
}