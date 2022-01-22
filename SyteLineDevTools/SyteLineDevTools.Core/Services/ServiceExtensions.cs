using Microsoft.Extensions.DependencyInjection;

namespace SyteLineDevTools.MVVM.Services
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddMVVM(this IServiceCollection services)
        {
            //services.AddSingleton<RegionService>()
            //    .AddTransient<IMessageBox, LCGitModLabels.Models.MessageBox>()
            //    .AddTransient<ViewModels.MainWindowViewModel>()
            //    .AddTransient<ViewModels.RepositoryViewModel>()
            //    .AddTransient<Views.MainWindow>()
            //    .AddTransient<Views.Repository>()
            //    ;
            //services.AddByName<IView>()
            //    .Add<Views.MainWindow>("MainWindow")
            //    .Add<Views.Repository>("Repository")
            //    .Build()
            //    ;
            return services;
        }
        public static void UseMVVM(this IServiceProvider serviceProvider)
        {
            ViewModelLocator.ServiceProvider = serviceProvider;
        }
    }
}
