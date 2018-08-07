using System.Windows;
using Microsoft.Win32;
using MockupToXaml.View;
using MockupToXaml.ViewModel;

namespace MockupToXaml
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel();
        }
    }
}