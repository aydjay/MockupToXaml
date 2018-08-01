using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Microsoft.Win32;
using MockupToXaml.ViewModel;

namespace MockupToXaml.View
{
    /// <summary>
    ///     Interaction logic for MappingView.xaml
    /// </summary>
    public partial class MappingView : Page
    {
        private readonly MappingViewModel viewModel = new MappingViewModel();

        public MappingView()
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        public MappingView(string filename)
        {
            DataContext = viewModel;
            InitializeComponent();
            viewModel.Filename = filename;


            viewModel.LoadMockup();
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "All Files (*.*)|*.*";
            ofd.ShowDialog();

            viewModel.Filename = ofd.FileName;


            viewModel.LoadMockup();
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            viewModel.GenerateCode();

            var window = (Window) XamlReader.Parse(viewModel.WindowXaml);
            window.Show();
        }
    }
}