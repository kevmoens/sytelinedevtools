﻿using Microsoft.Extensions.Logging;
using Neleus.DependencyInjection.Extensions;

namespace SyteLineDevTools.MVVM.Services
{
    /// <summary>
    /// Future goals are to handle handling navigating forward and backwards
    /// Handle view disposing and persistence
    ///
    /// MVP is Navigation Parameters
    /// </summary>
    public class RegionService
    {
        IServiceProvider serviceProvider;
        ILogger<RegionService> logger;
        public RegionService(IServiceProvider serviceProvider, ILogger<RegionService> logger)
        {
            this.serviceProvider = serviceProvider;
            this.logger = logger;
        }
        public Dictionary<string, INavigationService> NavigationServices { get; set; } = new Dictionary<string, INavigationService>(System.StringComparer.InvariantCultureIgnoreCase);
        public void Navigate(string RegionName, string ViewName, Dictionary<string, object>? parameters = null)
        {
            //Need Neuleus so I can register a class that wraps the view so I can pull it back up.
            //Have to make a extension method for Registering the new.

            IView? view;
            try
            {
                view = serviceProvider.GetServiceByName<IView>(ViewName);
            } catch (ArgumentException ex)
            {
                logger?.LogError(ex, $"Navigate Unable to find {ViewName}");
                return;
            }
            if (view == null)
            {
                return;
            }


            NavigationServices[RegionName].Navigate(view);
            if (parameters == null)
            {
                parameters = new Dictionary<string, object>();
            }

            var tyNavAware = view.DataContext?.GetType()?.GetInterface("INavigationParametersAware", true);
            if (tyNavAware != null && view.DataContext != null)
            {
                ((INavigationParametersAware)view.DataContext).NavigatedTo(parameters);
            }
        }
    }
}