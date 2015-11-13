using HeresyCore.Descriptions;
using HeresyCore.Entities;
using HeresyCore.Entities.Enums;
using System.Collections.Generic;
using System.ServiceModel;

namespace HeresyService.ServiceInterfaces
{
    [ServiceContract]
    public interface IHeresyService
    {
        [OperationContract]
        IDictionary<string, CharacterDescription> GetCharacterDescriptionList(Token token);
        [OperationContract]
        CharacterDescription GetCharacterDescription(Token token, string charId);

        #region Data getters

        [OperationContract]
        IDictionary<string, WorldDescription> GetWorldDescriptions();
        [OperationContract]
        WorldDescription GetWorldDescription(string id);
        [OperationContract]
        bool TryGetWorldDescription(string id, out WorldDescription world);

        [OperationContract]
        IDictionary<string, RaceDescription> GetRaceDescriptions();
        [OperationContract]
        RaceDescription GetRaceDescription(string id);
        [OperationContract]
        bool TryGetRaceDescription(string id, out RaceDescription race);

        [OperationContract]
        IDictionary<string, ClassDescription> GetClassDescriptions();
        [OperationContract]
        ClassDescription GetClassDescription(string id);
        [OperationContract]
        bool TryGetClassDescription(string id, out ClassDescription c);

        [OperationContract]
        IDictionary<string, BackgroundDescription> GetBackgroundDescriptions();
        [OperationContract]
        BackgroundDescription GetBackgroundDescription(string id);
        [OperationContract]
        bool TryGetBackgroundDescription(string id, out BackgroundDescription c);

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

        #region Character Management

        [OperationContract]
        bool SelectFreebie(Token token, string charId, int freebieId, int optionId);

        [OperationContract]
        bool UpgradeStat(Token token, string charId, ECharacterStat stat, int points);

        #endregion
    }
}
