using System;
using System.ServiceModel;

namespace HeresyCore.Utilities
{
    public class WcfContext<TService> : IDisposable where TService : ICommunicationObject
    {
        public TService Service { get; }

        public WcfContext(TService service)
        {
            Service = service;
        }

        #region Using helpers

        public static void Using(TService service, Action<TService> worker)
        {
            using (var context = new WcfContext<TService>(service))
            {
                worker(service);
            }
        }

        public static void Using(TService service, Action<WcfContext<TService>> worker)
        {
            using (var context = new WcfContext<TService>(service))
            {
                worker(context);
            }
        }

        public static void Using(TService service, Action<TService, WcfContext<TService>> worker)
        {
            using (var context = new WcfContext<TService>(service))
            {
                worker(service, context);
            }
        }

        #endregion

        #region IDisposable

        private bool _isDisposed = false;

        public virtual void Dispose()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(WcfContext<TService>));

            try
            {
                Service.Close();
            }
            catch (CommunicationException)
            {
            }
            catch (TimeoutException)
            {
            }
            finally
            {
                Service.Abort();
            }
            
            _isDisposed = true;
        }

        #endregion
    }
}
