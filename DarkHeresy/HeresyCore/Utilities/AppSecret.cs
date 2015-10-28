using System.Configuration;
using System.Diagnostics;

namespace HeresyCore.Utilities
{
    public static class AppSecret
    {
        [DebuggerStepThrough]
        public static string Get() => ConfigurationManager.AppSettings["AppSecret"];
    }
}
