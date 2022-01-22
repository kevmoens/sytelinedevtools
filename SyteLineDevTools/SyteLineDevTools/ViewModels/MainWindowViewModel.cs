using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SyteLineDevTools.Models.MainWindow;
using SyteLineDevTools.MVVM.Services;

namespace SyteLineDevTools.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event NavigationRequest NavigationRequest;
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        IMessageBox? messageBox;

        RegionService? regionService;
        private IServiceProvider? serviceProvider;
        public IServiceProvider? ServiceProvider
        {
            get { return serviceProvider; }
            set { serviceProvider = value; OnPropertyChanged(); }
        }
        public ICommand LoadedCommand { get; set; }
        private object? selectedView;

        public object? SelectedView
        {
            get { return selectedView; }
            set
            {
                selectedView = value;
                OnPropertyChanged();
                if (value == null)
                {
                    return;
                }
                var view = HamburgerMenuItem.GetValue(((System.Windows.Controls.ListViewItem)value));
                if (!string.IsNullOrEmpty(view))
                {
                    NavigationRequest?.Invoke(view);
                }
            }
        }

        public MainWindowViewModel(IMessageBox? messageBox, IServiceProvider? serviceProvider, RegionService? regionService)
        {
            this.messageBox = messageBox;
            this.ServiceProvider = serviceProvider;
            this.regionService = regionService;
            LoadedCommand = new DelegateCommand(OnLoaded);
            NavigationRequest += OnNavigationRequest;
        }
        public void OnLoaded()
        {
            OnNavigationRequest("Repository");
        }
        void OnNavigationRequest(string view)
        {
            regionService?.Navigate("MainRegion", view);
        }
        public void Initialize()
        {

        }
    }
}
