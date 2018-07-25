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
using Microsoft.Win32;
using System.Windows.Markup;
using System.Reflection;
using System.IO;

namespace MockupToXaml.View
{
    /// <summary>
    /// Interaction logic for MappingView.xaml
    /// </summary>
    public partial class MappingView : Page
    {
        ViewModel.MappingViewModel viewModel = new ViewModel.MappingViewModel();

        public MappingView()
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }

        public MappingView(string filename)
        {
            this.DataContext = viewModel;
            InitializeComponent();
            viewModel.Filename = filename;


            viewModel.LoadMockup();
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All Files (*.*)|*.*";
            ofd.ShowDialog();

            viewModel.Filename = ofd.FileName;

            
            viewModel.LoadMockup();
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            viewModel.GenerateCode();

            Window window = (Window)XamlReader.Parse(viewModel.WindowXaml);
            window.Show();
        }

       
    }
}
