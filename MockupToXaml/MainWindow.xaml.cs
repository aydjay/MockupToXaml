using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace MockupToXaml
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void miOpenMockup_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All Files (*.*)|*.*|Balsamiq Mockups (*.bmml)|*.bmml";
            ofd.ShowDialog();

            View.MappingView view = new View.MappingView(ofd.FileName);
            frameMain.Navigate(view);

        }

        private void miExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
