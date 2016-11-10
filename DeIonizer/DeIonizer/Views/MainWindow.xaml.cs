using Avalonia.Markup.Xaml;
using DeIonizer.VM;
using nkyUI.Controls;

namespace DeIonizer.Views
{
    class MainWindow : KYUIWindow
    {
        public MainWindow()
        {
            this.InitializeComponent();
            App.AttachDevTools(this);
            DataContext = new MainWindowVM();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}