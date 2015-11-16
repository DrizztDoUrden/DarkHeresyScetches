using HeresyCore.Entities.Data;
using HeresyCore.Entities.Data.Learning;
using HeresyCore.Entities.Data.Traits;
using HeresyCore.Entities.Enums;
using HeresyCore.Entities.Properties;
using HeresyCore.Entities.Properties.Moddifiers;
using HeresyCore.Utilities;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HeresyCore.Entities
{
    [DataContract]
    public class Character : Entity
    {
        public const int DefaultStatCost = 30;
        public const int StatLimit = 60;
        
        #region Properties

        [DataMember]
        public ECreationStage CreationStage { get; set; } = ECreationStage.RaceSelection;

        [DataMember]
        public int Wounds { get; set; } = 0;
        [DataMember]
        public Property<int> MaxWounds { get; set; } = 0;

        [DataMember]
        public int FreeExp { get; set; } = 0;
        [DataMember]
        public int SpentExp { get; set; } = 0;

        [DataMember]
        public int RanksTaken { get; set; } = 0;

        [DataMember]
        public int FatePoints { get; set; } = 0;
        [DataMember]
        public Property<int> MaxFatePoints { get; set; } = 0;

        [DataMember]
        public IDictionary<ECharacterStat, Property<int>> Stats { get; } = ECharacterStatExtensions.GetStatsDictionary(stat => new Property<int>());

        [DataMember]
        public IDictionary<ECharacterStat, int> StatCosts { get; set; } = ECharacterStatExtensions.GetStatsDictionary(stat => DefaultStatCost);

        [DataMember]
        public IDictionary<string, string> Groups { get; } = new Dictionary<string, string>();

        [DataMember]
        public IDictionary<string, ESkillMastery> Skills { get; } = new Dictionary<string, ESkillMastery>();

        [DataMember]
        public IDictionary<string, Property<int>> TestBonuses { get; } = new Dictionary<string, Property<int>>();

        [DataMember]
        public IDictionary<string, TraitData> Traits { get; } = new Dictionary<string, TraitData>();

        [DataMember]
        public List<LearningPackage> LearningPackages { get; } = new List<LearningPackage>();

        [DataMember]
        public List<Freebie> Freebies { get; } = new List<Freebie>();

        #endregion

        #region Helpers

        public Character AddTrait(Trait trait) => trait.Add(this);

        public bool AddGroup(Group group) => group.Add(this);

        public bool SpendExp(int amount)
        {
            if (FreeExp < amount)
                return false;

            FreeExp -= amount;
            SpentExp += amount;
            return true;
        }

        #region Stat upgrades

        public class StatUpgradesCollection
        {
            private Character _owner;

            public StatUpgradesCollection(Character owner)
            {
                _owner = owner;
            }
            
            public int this[ECharacterStat stat]
            {
                get
                {
                    IntAddModdifier mod;

                    _owner.Stats[stat].Moddifiers
                        .TryGetConverted("Upgrades", out mod, m => (IntAddModdifier)m);

                    return mod?.Value ?? 0;
                }
                set
                {
                    var prop = _owner.Stats[stat];
                    prop.Moddifiers["Upgrades"] = value;
                }
            }
        }

        public StatUpgradesCollection StatUpgrades { get; }

        #endregion

        #endregion

        public Character()
        {
             StatUpgrades = new StatUpgradesCollection(this);
        }
    }
}