using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MockupToXaml.Model;
using System.IO;
using System.Reflection;

namespace MockupToXaml.ViewModel
{
    public class MappingViewModel : ViewModelBase
    {

        public MappingViewModel()
        {
            Uri uri = new Uri(Assembly.GetEntryAssembly().CodeBase);
            string exeBasePath = uri.AbsolutePath.Replace("/", "\\");
            exeBasePath = Path.GetDirectoryName(exeBasePath);

            WindowTemplate = File.ReadAllText(string.Format("{0}\\Templates\\Window.txt", exeBasePath));
            
        }

        private string _Filename;
        public string Filename
        {
            get { return _Filename; }
            set
            {
                _Filename = value;
                OnPropertyChanged();
            }
        }


        private string _WindowTemplate;
        public string WindowTemplate
        {
            get { return _WindowTemplate; }
            set
            {
                _WindowTemplate = value;
                OnPropertyChanged();
            }
        }

        private Mockup _Mockup;
        public Mockup Mockup
        {
            get { return _Mockup; }
            set
            {
                _Mockup = value;
                OnPropertyChanged();
            }
        }

        private MockupControl _MainWindowControl;
        public MockupControl MainWindowControl
        {
            get { return _MainWindowControl; }
            set
            {
                _MainWindowControl = value;
                OnPropertyChanged();
            }
        }

        public void LoadMockup()
        {
            if (!string.IsNullOrEmpty(Filename))
            {
                Mockup = Mockup.LoadFromXML(Filename);
                // TODO: Check the mockup object.  Make sure it is not null, if it is bubble up an error to the UI.

                // Find the title window control, make all controls a sub of that.
                MainWindowControl = Mockup.Controls.SingleOrDefault(p => p.ControlTypeID == "com.balsamiq.mockups::TitleWindow");
                if (MainWindowControl == null)
                {
                    // TODO: Bubble a warning to the UI that the mockup loaded does not conform to the export requirements.
                    return;
                }

                // Enumerate the controls and fixup some values.
                foreach (MockupControl control in Mockup.Controls)
                {
                    if (control == MainWindowControl) continue;
                    
                    // Set the coordinates of the sub-controls to their relative (to the main mockup form) values.
                    control.X = control.X - MainWindowControl.X;
                    control.Y = control.Y - MainWindowControl.Y;
                }
            }
        }

        private string _GeneratedCode;
        public string GeneratedCode
        {
            get { return _GeneratedCode; }
            set
            {
                _GeneratedCode = value;
                OnPropertyChanged();
            }
        }

        private string _RequiredNamespaces;
        public string RequiredNamespaces
        {
            get { return _RequiredNamespaces; }
            set
            {
                _RequiredNamespaces = value;
                OnPropertyChanged();
            }
        }

        private string _WindowXaml;
        public string WindowXaml
        {
            get { return _WindowXaml; }
            set
            {
                _WindowXaml = value;
                OnPropertyChanged();
            }
        }



        public void GenerateCode()
        {
            Local.XamlGenerator gen = new Local.XamlGenerator();
            gen.Mockup = Mockup;
            gen.Generate();
            RequiredNamespaces  = gen.NamespaceHeader;
            GeneratedCode       = gen.GeneratedCode;
            WindowXaml = WindowTemplate.Replace("{Namespaces}", gen.NamespaceHeader).Replace("{Height}", gen.Mockup.Height.ToString())
                                    .Replace("{Width}", gen.Mockup.Width.ToString())
                                    .Replace("{LayoutRoot}", gen.GeneratedCode)
                                    .Replace("{Title}", "Title")
                                    .Replace("{Resources}", gen.ResourceHeader);

        }

    }
}
