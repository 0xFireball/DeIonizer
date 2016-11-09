using Avalonia.Markup.Xaml;
using nkyUI.Controls;

namespace DeIonizer
{
    public class MainWindow : KYUIWindow
    {
        public MainWindow()
        {
            this.InitializeComponent();
            App.AttachDevTools(this);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}