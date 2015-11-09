using HeresyCore.Entities.Data.Extensions;
using HeresyCore.Entities.Data.Interfaces;
using HeresyCore.Entities.Data.Traits;
using HeresyCore.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace HeresyCore.Entities.Data
{
    [DataContract]
    public abstract class Group : Entity, IStatsContainer, ITraitsContainer, ISkillsContainer, IFreebiesContainer
    {
        public abstract string GroupTypeName { get; }
        protected virtual Dice GetDefaultStatValue(ECharacterStat stat) => 0;
        protected virtual void AddCore(Character character) { }

        [DataMember]
        public Dice WoundsBase { get; set; } = 0;

        [DataMember]
        public Dictionary<ECharacterStat, Dice> Stats { get; set; }

        [DataMember]
        public List<Trait> Traits { get; set; } = new List<Trait>();

        [DataMember]
        public List<Freebie> Freebies { get; set; } = new List<Freebie>();

        [DataMember]
        public Dictionary<string, ESkillMastery> Skills { get; set; } = new Dictionary<string, ESkillMastery>();

        public Group()
        {
            Stats = Enum.GetValues(typeof(ECharacterStat))
                .Cast<ECharacterStat>()
                .ToDictionary(stat => stat, GetDefaultStatValue);
        }

        public Character Add(Character character)
        {
            if (character.Groups.ContainsKey(GroupTypeName))
                throw new Exception($"Попытка добавить группу типа <{GroupTypeName}> персонажу {character.GetDebugIdString()}, у которого уже есть группа этого типа");

            character.Groups.Add(GroupTypeName, Id);

            character.MaxWounds.Moddifiers.Add(GroupTypeName, WoundsBase.Roll().Sum);

            AddCore(character);

            character
                .AddStats(this)
                .AddTraits(this)
                .AddSkills(this)
                .AddFreebies(this);

            return character;
        }
    }
}
