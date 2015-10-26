using HeresyCore.Entities;
using System.Collections.Generic;
using System.ServiceModel;

namespace HeresyService.ServiceInterfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IHeresyService
    {
        [OperationContract]
        IDictionary<string, Character> GetCharacterList(string token);

        // TODO: Add your service operations here
    }
}
