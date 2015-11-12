using HeresyCore.Entities;
using HeresyCore.Entities.Data;
using System.Collections.Generic;
using System.ServiceModel;

namespace HeresyService.ServiceInterfaces
{
    [ServiceContract]
    public interface IInternalHeresyService : IHeresyService
    {
        [OperationContract]
        void RegisterUser(string id, string appSecret);

        [OperationContract]
        IDictionary<string, Character> GetCharacterList(Token token);
        [OperationContract]
        Character GetCharacter(Token token, string charId);

        [OperationContract]
        IDictionary<string, World> GetWorlds();
        [OperationContract]
        World GetWorld(string id);
        [OperationContract]
        bool TryGetWorld(string id, out World world);

        [OperationContract]
        IDictionary<string, Race> GetRaces();
        [OperationContract]
        Race GetRace(string id);
        [OperationContract]
        bool TryGetRace(string id, out Race race);

        [OperationContract]
        IDictionary<string, Class> GetClasses();
        [OperationContract]
        Class GetClass(string id);
        [OperationContract]
        bool TryGetClass(string id, out Class c);

        [OperationContract]
        IDictionary<string, Background> GetBackgrounds();
        [OperationContract]
        Background GetBackground(string id);
        [OperationContract]
        bool TryGetBackground(string id, out Background c);
    }
}
