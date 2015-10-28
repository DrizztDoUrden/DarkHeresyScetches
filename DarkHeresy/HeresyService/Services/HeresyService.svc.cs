using HeresyCore.Entities;
using HeresyService.ServiceInterfaces;
using System.Collections.Generic;

namespace HeresyService.Services
{
    public class HeresyService: IHeresyService
    {
        public IDictionary<string, Character> GetCharacterList(Token token)
        {
            return new Dictionary<string, Character>
            {
                { "Test", new Character() },
            };
        }
    }
}
