using ProcessJasonData.Common;
using ProcessJasonData.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;


namespace ProcessJasonData.ViewModel
{
    public class JasonDataViewModel : INotifyPropertyChanged
    {
        private string _jasonDataPath;
        private ObservableCollection<JasonData> _jasonDataCollection;
        private JasonData _selectedItem;
        private DataController _dataController;
        private IConfigurationManager _configManager;

        public IConfigurationManager ConfigManager
        {
            get
            {
                return _configManager;
            }
            set
            {
                _configManager = value;
            }
        }

        public JasonDataViewModel()
        {
            _configManager = new JasonConfigurationManager();            
            _jasonDataCollection = new ObservableCollection<Model.JasonData>();
            _loadCommand = new DelegateCommand(LoadJasonData, CanLoadJasonData);
            _copyPlainTextCommand = new DelegateCommand(CopyPlainTextToClipboard, CanCopyPlainTextToClipboard);
            _dataController = new DataController();
        }

        private bool CanCopyPlainTextToClipboard()
        {
            return true;
        }

        private void CopyPlainTextToClipboard()
        {
            if (SelectedItem != null)
            {
                Clipboard.SetText(SelectedItem.Body);
            }
        }

        private void LoadData(JasonData[] data)
        {
            if (data != null)
            {
               this.JasonDataCollection.Clear();
                foreach (JasonData obj in data)
                {
                    this.JasonDataCollection.Add(obj);
                }
            }
        }

        private bool CanLoadJasonData()
        {
            return true;
        }

        private void LoadJasonData()
        {
            var json_data = string.Empty;
            _jasonDataPath = _configManager.GetAppSetting("JasonDataPath");
            Task<JasonData[]> t1 = new Task<JasonData[]>(()=> _dataController.DownloadAndProcessdata(_jasonDataPath));
            t1.Start();
            t1.ContinueWith(Task => LoadData(t1.Result), TaskScheduler.FromCurrentSynchronizationContext());            
        }

        public ObservableCollection<JasonData> JasonDataCollection
        {
            get
            {
                return _jasonDataCollection;
            }
            set
            {
                _jasonDataCollection = value;
                RaisePropertyChanged("JasonDataCollection");
            }
        }

        public JasonData SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }

        private DelegateCommand _loadCommand;

        public DelegateCommand LoadCommand
        {
            get { return _loadCommand; }
            set { _loadCommand = value; }
        }

        private DelegateCommand _copyPlainTextCommand;

        public DelegateCommand CopyPlainTextCommand
        {
            get { return _copyPlainTextCommand; }
            set { _copyPlainTextCommand = value; }
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(null, new PropertyChangedEventArgs(property));
            }
        }
        #endregion
    }
}
