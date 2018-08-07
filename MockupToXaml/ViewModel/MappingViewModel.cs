using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using MockupToXaml.Local;
using MockupToXaml.Model;
using MockupToXaml.View.ViewModel;

namespace MockupToXaml.ViewModel
{
    public class MappingViewModel : ViewModelBase
    {
        private string _filename;
        private string _generatedCode;
        private MockupControl _mainWindowControl;
        private MockupHolder _mockupHolder;
        private string _requiredNamespaces;
        private string _windowTemplate;
        private string _windowXaml;

        public MappingViewModel(string filename)
        {
            Filename = filename;

            WindowTemplate = Resources.Window;
        }

        public string Filename
        {
            get { return _filename; }
            set
            {
                _filename = value;
                OnPropertyChanged();
            }
        }

        public string WindowTemplate
        {
            get { return _windowTemplate; }
            set
            {
                _windowTemplate = value;
                OnPropertyChanged();
            }
        }

        public MockupHolder MockupHolder
        {
            get { return _mockupHolder; }
            set
            {
                _mockupHolder = value;
                OnPropertyChanged();
            }
        }

        public MockupControl MainWindowControl
        {
            get { return _mainWindowControl; }
            set
            {
                _mainWindowControl = value;
                OnPropertyChanged();
            }
        }

        public string GeneratedCode
        {
            get { return _generatedCode; }
            set
            {
                _generatedCode = value;
                OnPropertyChanged();
            }
        }

        public string RequiredNamespaces
        {
            get { return _requiredNamespaces; }
            set
            {
                _requiredNamespaces = value;
                OnPropertyChanged();
            }
        }

        public string WindowXaml
        {
            get { return _windowXaml; }
            set
            {
                _windowXaml = value;

                //Todo: This is how we spin up a window from raw XAML.
                var window = (Window) XamlReader.Parse(_windowXaml);
                window.Show();

                OnPropertyChanged();
            }
        }

        public ICommand GenerateCommand => new RelayCommand(GenerateCode, CanGenerateCode);

        public void LoadMockup()
        {
            if (!string.IsNullOrEmpty(Filename))
            {
                MockupHolder = MockupHolder.DeserialiseFile(Filename);
                // TODO: Check the mockup object.  Make sure it is not null, if it is bubble up an error to the UI.

                // Find the title window control, make all controls a sub of that.
                MainWindowControl = MockupHolder.Mockup.Controls.Control.SingleOrDefault(p => p.ControlTypeId == "com.balsamiq.mockups::TitleWindow");
                if (MainWindowControl == null)
                {
                    // TODO: Bubble a warning to the UI that the mockup loaded does not conform to the export requirements.
                    return;
                }

                // Enumerate the controls and fixup some values.
                foreach (var control in MockupHolder.Mockup.Controls.Control)
                {
                    if (control == MainWindowControl)
                    {
                        continue;
                    }

                    // Set the coordinates of the sub-controls to their relative (to the main mockup form) values.
                    control.X = control.X - MainWindowControl.X;
                    control.Y = control.Y - MainWindowControl.Y;
                }
            }
        }

        private bool CanGenerateCode(object obj)
        {
            return string.IsNullOrEmpty(Filename) == false;
        }

        public void GenerateCode(object obj)
        {
            var gen = new XamlGenerator();
            LoadMockup();
            gen.MockupHolder = MockupHolder;
            gen.Generate();
            RequiredNamespaces = gen.NamespaceHeader;
            GeneratedCode = gen.GeneratedCode;
            WindowXaml = WindowTemplate.Replace("{Namespaces}", gen.NamespaceHeader).Replace("{Height}", gen.MockupHolder.Mockup.Height.ToString())
                .Replace("{Width}", gen.MockupHolder.Mockup.Width.ToString())
                .Replace("{LayoutRoot}", gen.GeneratedCode)
                .Replace("{Title}", "Title")
                .Replace("{Resources}", gen.ResourceHeader);
        }
    }
}