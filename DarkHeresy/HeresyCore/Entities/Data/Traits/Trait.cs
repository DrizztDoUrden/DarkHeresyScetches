using System;
using System.Runtime.Serialization;

namespace HeresyCore.Entities.Data.Traits
{
    [DataContract]
    public abstract class Trait : Entity
    {
        public abstract void OnAdd(Character character, TraitData traitData);
        public abstract void OnRemove(Character character, TraitData traitData);
        public virtual bool OnStack(Character character, TraitData traitData, TraitData oldData) => true;

        protected abstract TraitData GetDefaultData();

        protected abstract TraitData GetData(object content);

        public void Add(Character character)
        {
            var newData = GetDefaultData();
            AddCore(character, newData);
        }

        public void Add(Character character, object content)
        {
            var newData = GetData(content);
            AddCore(character, newData);
        }

        public void Remove(Character character)
        {
            TraitData data;

            if (!character.Traits.TryGetValue(Id, out data))
                throw new Exception($"У персонажа {character.GetDebugIdString()} нет трейта {GetDebugIdString()}");

            OnRemove(character, data);
            character.Traits.Remove(Id);
        }

        protected void AddCore(Character character, TraitData newData)
        {
            TraitData data;
            if (character.Traits.TryGetValue(Id, out data))
            {
                var replaceData = OnStack(character, newData, data);
                if (replaceData)
                {
                    character.Traits[Id] = newData;
                }
            }
            else
            {
                character.Traits.Add(Id, newData);
                OnAdd(character, newData);
            }
        }
    }

    public abstract class Trait<TContent> : Trait
    {
        public abstract void OnAdd(Character character, TraitData<TContent> traitData);
        public abstract void OnRemove(Character character, TraitData<TContent> traitData);
        public virtual bool OnStack(Character character, TraitData<TContent> traitData, TraitData<TContent> oldData) => true;

        protected override TraitData GetDefaultData() => new TraitData<TContent>(this);
        protected override TraitData GetData(object content) => new TraitData<TContent>(this, content);

        public sealed override void OnAdd(Character character, TraitData traitData)
        {
            OnAdd(character, (TraitData<TContent>)traitData);
        }

        public sealed override void OnRemove(Character character, TraitData traitData)
        {
            OnRemove(character, (TraitData<TContent>)traitData);
        }

        public sealed override bool OnStack(Character character, TraitData traitData, TraitData oldData)
        {
            return OnStack(character, (TraitData<TContent>)traitData, (TraitData<TContent>)oldData);
        }

        public void Add(Character character, TContent content)
        {
            var newData = new TraitData<TContent>(this, content);
            AddCore(character, newData);
        }
    }
}
