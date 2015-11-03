using System;
using System.ServiceModel;

namespace HeresyCore.Utilities
{
    public static class WcfExtensions
    {
        public static void Using<T>(Action<T> work)
            where T : ICommunicationObject, new()
        {
            var client = new T();
            client.Using(work);
        }

        public static void Using<T>(this T client, Action<T> work)
            where T : ICommunicationObject
        {
            try
            {
                work(client);

                try
                {
                    client.Close();
                }
                catch (CommunicationException)
                {
                }
                catch (TimeoutException)
                {
                }
            }
            finally
            {
                client.Abort();
            }
        }
    }
}
