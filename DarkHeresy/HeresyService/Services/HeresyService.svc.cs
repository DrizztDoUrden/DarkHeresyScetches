using HeresyCore.Entities;
using HeresyService.InternalAuthService;
using HeresyService.ServiceInterfaces;
using System.Collections.Generic;
using System.Configuration;

namespace HeresyService.Services
{
    public class HeresyService: IHeresyService
    {
        public IDictionary<string, Character> GetCharacterList(Token token)
        {
            string id;

            using (var auth = new InternalAuthServiceClient())
            {
                id = auth.GetId(token, ConfigurationManager.AppSettings["AppSecret"]);
            }

            if (id == null)
                return null;

            return new Dictionary<string, Character>
            {
                { "Test", new Character() },
            };
        }
    }
}
