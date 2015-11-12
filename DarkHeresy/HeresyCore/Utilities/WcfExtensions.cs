using System;
using System.ServiceModel;

namespace HeresyCore.Utilities
{
    public static class WcfExtensions
    {
        public static void Using<TService>(Action<TService> worker) where TService : ICommunicationObject, new() =>
            WcfContext<TService>.Using(new TService(), worker);

        public static void Using<TService>(Action<WcfContext<TService>> worker) where TService : ICommunicationObject, new() =>
            WcfContext<TService>.Using(new TService(), worker);

        public static void Using<TService>(Action<TService, WcfContext<TService>> worker) where TService : ICommunicationObject, new() =>
            WcfContext<TService>.Using(new TService(), worker);
    }
}
