using Microsoft.Extensions.DependencyInjection;
using Neleus.DependencyInjection.Extensions;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SyteLineDevTools.MVVM.Services;
using SyteLineDevTools.SyteLine.Connections;

namespace SyteLineDevTools
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static IServiceProvider? ServiceProvider { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            ConfigureServices();
            base.OnStartup(e);
            ViewModelLocator.ServiceProvider = ServiceProvider;
            this.MainWindow = new Views.MainWindow() { Visibility = Visibility.Visible };

        }
        public static void ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            services
                .AddSingleton<RegionService>()
                .AddTransient<IMessageBox, Models.MessageBox>()
                .AddSingleton<AllConnections>(new AllConnections(StringComparer.InvariantCultureIgnoreCase))
                .AddTransient<ViewModels.MainWindowViewModel>()
                .AddTransient<Views.MainWindow>()
                ;
            services.AddByName<IView>()
                .Add<Views.MainWindow>("MainWindow")
                .Build()
                ;

            ServiceProvider = 
                services
                .AddMVVM()
                .AddLogging(options =>
                {
                    options.AddNLog();
                })
                .BuildServiceProvider();



        }
    }
}
