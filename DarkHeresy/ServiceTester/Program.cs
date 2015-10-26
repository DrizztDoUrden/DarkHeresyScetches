using ServiceTester.HeresyService;

namespace ServiceTester
{
    public class Program
    {
        public static void Main()
        {
            using (var service = new HeresyServiceClient())
            {
                var chars = service.GetCharacterList(null);
            }
        }
    }
}
