using System.Windows;
using Microsoft.Win32;
using MockupToXaml.View;

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
        }

        private void miOpenMockup_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "All Files (*.*)|*.*|Json (*.json)|*.json";
            var result = ofd.ShowDialog();
            if (result.HasValue && result.Value)
            {
                var view = new MappingView(ofd.FileName);
                frameMain.Navigate(view);
            }
        }

        private void miExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}