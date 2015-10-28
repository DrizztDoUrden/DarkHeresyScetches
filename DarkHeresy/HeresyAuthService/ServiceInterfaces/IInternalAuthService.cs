using HeresyCore.Entities;
using System.ServiceModel;

namespace HeresyAuthService.ServiceInterfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IHeresyAuthService" in both code and config file together.
    [ServiceContract]
    public interface IInternalAuthService: IAuthService
    {
        [OperationContract]
        string GetId(Token token, string appSecret);
    }
}
