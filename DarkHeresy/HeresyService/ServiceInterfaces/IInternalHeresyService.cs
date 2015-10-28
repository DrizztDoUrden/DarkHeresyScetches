using System.ServiceModel;

namespace HeresyService.ServiceInterfaces
{
    [ServiceContract]
    public interface IInternalHeresyService : IHeresyService
    {
        [OperationContract]
        void RegisterUser(string id, string appSecret);
    }
}
