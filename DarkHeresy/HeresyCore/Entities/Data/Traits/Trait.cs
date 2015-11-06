using System;
using System.Runtime.Serialization;

namespace HeresyCore.Entities.Data.Traits
{
    [DataContract]
    public abstract class Trait
    {
        [DataMember]
        public abstract string Id { get; }
        [DataMember]
        public abstract string Name { get; }

        public abstract void OnAdd(Character character, TraitData traitData);
        public abstract void OnRemove(Character character, TraitData traitData);
        public virtual bool OnStack(Character character, TraitData traitData, TraitData oldData) => true;

        public abstract void Add(Character character, object content);

        public void Add<TContent>(Character character)
        {
            var newData = new TraitData<TContent>(this);
            Add(character, newData);
        }

        public void Add<TContent>(Character character, TContent content)
        {
            var newData = new TraitData<TContent>(this, content);
            Add(character, newData);
        }

        public void Remove(Character character)
        {
            TraitData data;

            if (!character.Traits.TryGetValue(Id, out data))
                throw new Exception($"У персонажа <{character.Name}[{character.Id}]> нет трейта <{Name}[{Id}]>");

            OnRemove(character, data);
        }

        protected void Add(Character character, TraitData newData)
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

        public void Add(Character character)
        {
            Add<TContent>(character);
        }

        public void Add(Character character, TContent content)
        {
            Add<TContent>(character, content);
        }

        public sealed override void Add(Character character, object content)
        {
            var newData = new TraitData<TContent>(this, content);
            Add(character, newData);
        }
    }
}
