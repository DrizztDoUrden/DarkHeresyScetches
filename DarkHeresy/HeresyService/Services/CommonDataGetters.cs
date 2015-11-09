using HeresyCore.Entities.Data;
using System.Collections.Generic;
using TestContent;

namespace HeresyService.Services
{
    public partial class HeresyService
    {
        public Dictionary<string, World> GetWorlds() => Worlds.Collection.All;
        public World GetWorld(string id) => Worlds.Collection.Get(id);
        public bool TryGetWorld(string id, out World world) => Worlds.Collection.TryGet(id, out world);

        public Dictionary<string, Race> GetRaces() => Races.Collection.All;
        public Race GetRace(string id) => Races.Collection.Get(id);
        public bool TryGetRace(string id, out Race race) => Races.Collection.TryGet(id, out race);

        public Dictionary<string, Class> GetClasses() => Classes.Collection.All;
        public Class GetClass(string id) => Classes.Collection.Get(id);
        public bool TryGetClass(string id, out Class c) => Classes.Collection.TryGet(id, out c);

        public Dictionary<string, Background> GetBackgrounds() => Backgrounds.Collection.All;
        public Background GetBackground(string id) => Backgrounds.Collection.Get(id);
        public bool TryGetBackground(string id, out Background bg) => Backgrounds.Collection.TryGet(id, out bg);
    }
}