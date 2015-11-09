using HeresyCore.Entities;
using HeresyCore.Entities.Data;
using HeresyCore.Entities.Enums;
using System.Collections.Generic;
using System.ServiceModel;

namespace HeresyService.ServiceInterfaces
{
    [ServiceContract]
    public interface IHeresyService
    {
        [OperationContract]
        IDictionary<string, Character> GetCharacterList(Token token);

        #region Data getters

        [OperationContract]
        Dictionary<string, World> GetWorlds();
        [OperationContract]
        World GetWorld(string id);
        [OperationContract]
        bool TryGetWorld(string id, out World world);

        [OperationContract]
        Dictionary<string, Race> GetRaces();
        [OperationContract]
        Race GetRace(string id);
        [OperationContract]
        bool TryGetRace(string id, out Race race);

        [OperationContract]
        Dictionary<string, Class> GetClasses();
        [OperationContract]
        Class GetClass(string id);
        [OperationContract]
        bool TryGetClass(string id, out Class c);
        
        [OperationContract]
        Dictionary<string, Background> GetBackgrounds();
        [OperationContract]
        Background GetBackground(string id);
        [OperationContract]
        bool TryGetBackground(string id, out Background c);

        #endregion

        #region Character creation

        [OperationContract]
        string CreateCharacter(Token token, string name);

        [OperationContract]
        bool SelectRace(Token token, string charId, string raceId);
        [OperationContract]
        bool RerollStat(Token token, string charId, ECharacterStat stat);
        [OperationContract]
        bool SkipReroll(Token token, string charId);
        [OperationContract]
        bool SelectWorld(Token token, string charId, string worldId);
        [OperationContract]
        bool SelectClass(Token token, string charId, string classId);
        [OperationContract]
        bool SelectBackground(Token token, string charId, string backgroundId);
        [OperationContract]
        bool SkipBackground(Token token, string charId);

        #endregion
    }
}
