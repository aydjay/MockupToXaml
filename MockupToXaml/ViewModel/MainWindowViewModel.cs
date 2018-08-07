using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using MockupToXaml.View.ViewModel;

namespace MockupToXaml.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _fileName;
        private MappingViewModel _mappingViewModel;
        public ICommand OpenCommand => new RelayCommand(Execute);
        public ICommand ExitCommand => new RelayCommand(x => Application.Current.Shutdown());

        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;

                OnPropertyChanged();

                MappingViewModel = new MappingViewModel(FileName);
            }
        }

        public MappingViewModel MappingViewModel
        {
            get { return _mappingViewModel; }
            set
            {
                _mappingViewModel = value;
                OnPropertyChanged();
            }
        }

        private void Execute(object obj)
        {
            var ofd = new OpenFileDialog {Filter = "All Files (*.*)|*.*|Json (*.json)|*.json"};
            var result = ofd.ShowDialog();
            if (result.HasValue && result.Value)
            {
                FileName = ofd.FileName;
            }
        }
    }
}