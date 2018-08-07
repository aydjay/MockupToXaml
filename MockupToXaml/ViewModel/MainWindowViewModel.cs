using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using MockupToXaml.View.ViewModel;

namespace MockupToXaml.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ICommand OpenCommand => new RelayCommand(Execute);
        public ICommand ExitCommand => new RelayCommand(x => Application.Current.Shutdown());

        private void Execute(object obj)
        {
            var ofd = new OpenFileDialog {Filter = "All Files (*.*)|*.*|Json (*.json)|*.json"};
            var result = ofd.ShowDialog();
            if (result.HasValue && result.Value)
            {
                //var view = new MappingView(ofd.FileName);
                //frameMain.Navigate(view);
            }
        }
    }
}