using HeresyCore.Entities;
using System.ServiceModel;

namespace HeresyAuthService.ServiceInterfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IHeresyAuthService" in both code and config file together.
    [ServiceContract]
    public interface IAuthService
    {
        [OperationContract]
        bool Register(string loginHash, string passHash);

        [OperationContract]
        Token Login(string loginHash, string passHash);

        [OperationContract]
        bool Logout(Token token);

        [OperationContract]
        bool IsLogged(Token token);
    }
}
