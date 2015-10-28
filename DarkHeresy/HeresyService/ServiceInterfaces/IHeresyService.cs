using HeresyCore.Entities;
using System.Collections.Generic;
using System.ServiceModel;

namespace HeresyService.ServiceInterfaces
{
    [ServiceContract]
    public interface IHeresyService
    {
        [OperationContract]
        IDictionary<string, Character> GetCharacterList(Token token);
    }
}
