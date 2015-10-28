using HeresyCore.Entities;
using System.Collections.Generic;

namespace HeresyService.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public IDictionary<string, Character> Characters { get; set; }

        public User(string id)
        {
            Id = id;
            Characters = new Dictionary<string, Character>();
        }
    }
}