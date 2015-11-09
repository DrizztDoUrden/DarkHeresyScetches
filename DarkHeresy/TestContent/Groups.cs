using HeresyCore.Entities.Data;
using System.Collections.Generic;
using System.Linq;

namespace TestContent
{
    public abstract class Groups<T> where T : Group
    {
        protected Dictionary<string, T> AllGroups { get; }

        protected Groups(params T[] groups)
        {
            AllGroups = groups.ToDictionary(w => w.Id);
        }

        public Dictionary<string, T> All => AllGroups;

        public T Get(string id) => AllGroups[id];

        public bool TryGet(string id, out T group) => AllGroups.TryGetValue(id, out group);
    }
}
