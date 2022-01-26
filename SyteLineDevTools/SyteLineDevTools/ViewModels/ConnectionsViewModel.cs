using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SyteLineDevTools.MVVM.Services;
using SyteLineDevTools.SyteLine.Connections;

namespace SyteLineDevTools.ViewModels
{
    public class ConnectionsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private AllConnections? _allConnections;

        public AllConnections? AllConnections
        {
            get { return _allConnections; }
            set { _allConnections = value; OnPropertyChanged(); }
        }
        private IConnection? _selectedConnection;

        public IConnection? SelectedConnection
        {
            get { return _selectedConnection; }
            set { _selectedConnection = value; OnPropertyChanged(); }
        }

        public ICommand? AddCommand { get; set; }
        public ICommand? RemoveCommand { get; set; }
        public ICommand? UpdateCommand { get; set; }
        public ConnectionsViewModel(AllConnections allConnections)
        {
            AllConnections = allConnections;
            AddCommand = new DelegateCommand(OnAdd);
            RemoveCommand = new DelegateCommand(OnRemove);
            UpdateCommand = new DelegateCommand(OnUpdate);           
        }
        public void OnAdd()
        {

        }
        public void OnRemove()
        {

        }
        public void OnUpdate()
        {

        }
    }
}
