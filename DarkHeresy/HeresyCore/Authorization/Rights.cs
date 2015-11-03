using System.Collections.Generic;
using System.Linq;

namespace HeresyCore.Authorization
{
    public static class Rights
    {
        public static class InternalAuth
        {
            public static string Common => $@"{nameof(InternalAuth)}\{nameof(Common)}";

            public static IEnumerable<string> FullRights => new[]
            {
                Common.ToLower(),
            };
        }

        public static class CoreService
        {
            public static string Common => $@"{nameof(CoreService)}\{nameof(Common)}";

            public static IEnumerable<string> FullRights => new[]
            {
                Common.ToLower(),
            };
        }

        public static IEnumerable<string> FullRights => new[]
        {
            InternalAuth.FullRights,
            CoreService.FullRights,
        }.SelectMany(a => a);
    }
}