﻿using HeresyCore.Entities.Data.Extensions;
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
        public int FreeExp { get; set; } = 0;

        [DataMember]
        public Dice WoundsBase { get; set; } = 0;

        [DataMember]
        public IDictionary<ECharacterStat, Dice> Stats { get; }

        [DataMember]
        public List<Trait> Traits { get; } = new List<Trait>();

        [DataMember]
        public List<Freebie> Freebies { get; } = new List<Freebie>();

        [DataMember]
        public IDictionary<string, ESkillMastery> Skills { get; } = new Dictionary<string, ESkillMastery>();

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
            character.FreeExp += FreeExp;

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
