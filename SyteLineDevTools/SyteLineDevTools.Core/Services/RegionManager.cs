using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace SyteLineDevTools.MVVM.Services
{
    public class RegionManager : Frame
    {
        public static IServiceProvider GetServiceProvider(DependencyObject Obj)
        {
            return (IServiceProvider)Obj.GetValue(ServiceProviderProperty);
        }
        public static void SetServiceProvider(DependencyObject Obj, IServiceProvider Value)
        {
            Obj.SetValue(ServiceProviderProperty, Value);
            AlignToRegionService(Obj);
        }

        public static readonly DependencyProperty ServiceProviderProperty = DependencyProperty.RegisterAttached("ServiceProvider",
                 typeof(IServiceProvider), typeof(RegionManager), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnServiceProviderChanged));


        public static void OnServiceProviderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AlignToRegionService(d);
        }

        public static string GetRegionName(DependencyObject Obj)
        {
            return (string)Obj.GetValue(RegionNameProperty);
        }
        public static void SetRegionName(DependencyObject Obj, string Value)
        {
            Obj.SetValue(RegionNameProperty, Value);
            AlignToRegionService(Obj);
        }

        public static readonly DependencyProperty RegionNameProperty = DependencyProperty.RegisterAttached("RegionName",
                 typeof(string), typeof(RegionManager), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnRegionNameChanged));



        public static void OnRegionNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AlignToRegionService(d);
        }
        public static void AlignToRegionService(DependencyObject obj)
        {
            var serviceProvider = GetServiceProvider(obj);
            var regionName = GetRegionName(obj);
            if (string.IsNullOrEmpty(regionName)) return;
            if (serviceProvider == null) return;
            var regionService = serviceProvider.GetService<RegionService>();
            if (regionService != null && !regionService.NavigationServices.ContainsKey(regionName))
            {
                regionService.NavigationServices.Add(regionName, new WpfNavigationService((Frame)obj));
            }
        }
    }
}
