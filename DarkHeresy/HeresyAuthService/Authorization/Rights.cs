using System.Collections.Generic;

namespace HeresyAuthService.Authorization
{
    public static class Rights
    {
        public class InternalAuth
        {
            public static string Common => $@"{nameof(InternalAuth)}\{nameof(Common)}";
        }

        public static IEnumerable<string> FullRights => new[]
        {
            InternalAuth.Common.ToLower(),
        };
    }
}