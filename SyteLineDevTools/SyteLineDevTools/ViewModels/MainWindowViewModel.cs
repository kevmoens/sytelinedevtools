using Microsoft.Extensions.Logging;
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
using SyteLineDevTools.SyteLine.Connections;

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
        ILogger<MainWindowViewModel> logger;
        AllConnections connections;

        private IServiceProvider? serviceProvider;
        public IServiceProvider? ServiceProvider
        {
            get { return serviceProvider; }
            set { serviceProvider = value; OnPropertyChanged(); }
        }
        private bool _isHamburgerOpen;

        public bool IsHamburgerOpen
        {
            get { return _isHamburgerOpen; }
            set { _isHamburgerOpen = value; OnPropertyChanged(); }
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
                var view = value?.ToString();
                if (view == null)
                {
                    return;
                }
                if (!string.IsNullOrEmpty(view))
                {
                    NavigationRequest?.Invoke(view);
                }
            }
        }

        public MainWindowViewModel(IMessageBox? messageBox
            ,IServiceProvider? serviceProvider
            ,RegionService? regionService
            ,ILogger<MainWindowViewModel> logger
            ,AllConnections connections
            )
        {
            this.messageBox = messageBox;
            this.ServiceProvider = serviceProvider;
            this.regionService = regionService;
            this.logger = logger;
            this.connections = connections;
            LoadedCommand = new DelegateCommand(OnLoaded);
            NavigationRequest += OnNavigationRequest;
        }
        public async void OnLoaded()
        {
            await connections.LoadConnections();
            OnNavigationRequest("Connections");
        }
        void OnNavigationRequest(string view)
        {
            logger.LogInformation($"OnNavigationRequest {view}");
            regionService?.Navigate("MainRegion", view);
            IsHamburgerOpen = false;
        }
        public void Initialize()
        {

        }
    }
}
