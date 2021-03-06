﻿using Avalonia.Markup.Xaml;
using DeIonizer.SharedUI.VM;
using nkyUI.Controls;

namespace DeIonizer.SharedUI.Views
{
    public class MainWindow : KYUIWindow
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