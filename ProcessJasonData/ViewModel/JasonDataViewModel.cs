using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using ProcessJasonData.Model;
using ProcessJasonData.Common;
using System.Windows.Threading;
using System.Windows;


namespace ProcessJasonData.ViewModel
{
    public class JasonDataViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<JasonData> _jasonDataCollection;
        private JasonData _selectedItem;

        public JasonDataViewModel()
        {
            _jasonDataCollection = new ObservableCollection<Model.JasonData>();
            _loadCommand = new DelegateCommand(LoadJasonData, CanLoadJasonData);
            _copyPlainTextCommand = new DelegateCommand(CopyPlainTextToClipboard, CanCopyPlainTextToClipboard);
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
            JasonData[] data = null;
            using (var webClient = new WebClient())
            {
                try
                {
                    json_data = webClient.DownloadString("http://jsonplaceholder.typicode.com/posts");
                    //json_data = webClient.DownloadString(@"D:\JasonData.txt");

                    Task t1 = new Task(() =>
                    {
                        data = JsonConvert.DeserializeObject<JasonData[]>(json_data);
                    });

                    t1.Start();
                    t1.ContinueWith(Task => LoadData(data), TaskScheduler.FromCurrentSynchronizationContext());
                }
                catch (Exception e)
                {
                }
            }            
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
