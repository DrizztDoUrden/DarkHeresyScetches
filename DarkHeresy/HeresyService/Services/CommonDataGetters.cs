using HeresyCore.Descriptions;
using HeresyCore.Entities.Data;
using HeresyCore.Utilities;
using System.Collections.Generic;
using TestContent;

namespace HeresyService.Services
{
    public partial class HeresyService
    {
        public IDictionary<string, World> GetWorlds() => Worlds.Collection.All;
        public World GetWorld(string id) => Worlds.Collection.Get(id);
        public bool TryGetWorld(string id, out World world) => Worlds.Collection.TryGet(id, out world);

        public IDictionary<string, Race> GetRaces() => Races.Collection.All;
        public Race GetRace(string id) => Races.Collection.Get(id);
        public bool TryGetRace(string id, out Race race) => Races.Collection.TryGet(id, out race);

        public IDictionary<string, Class> GetClasses() => Classes.Collection.All;
        public Class GetClass(string id) => Classes.Collection.Get(id);
        public bool TryGetClass(string id, out Class c) => Classes.Collection.TryGet(id, out c);

        public IDictionary<string, Background> GetBackgrounds() => Backgrounds.Collection.All;
        public Background GetBackground(string id) => Backgrounds.Collection.Get(id);
        public bool TryGetBackground(string id, out Background bg) => Backgrounds.Collection.TryGet(id, out bg);
        
        public IDictionary<string, WorldDescription> GetWorldDescriptions() => Worlds.Collection.All.Remap(w => (WorldDescription)w);
        public WorldDescription GetWorldDescription(string id) => Worlds.Collection.Get(id);
        public bool TryGetWorldDescription(string id, out WorldDescription desc) => Worlds.Collection.All.TryGetConverted(id, out desc, e => e);

        public IDictionary<string, RaceDescription> GetRaceDescriptions() => Races.Collection.All.Remap(w => (RaceDescription)w);
        public RaceDescription GetRaceDescription(string id) => Races.Collection.Get(id);
        public bool TryGetRaceDescription(string id, out RaceDescription desc) => Races.Collection.All.TryGetConverted(id, out desc, e => e);

        public IDictionary<string, ClassDescription> GetClassDescriptions() => Classes.Collection.All.Remap(w => (ClassDescription)w);
        public ClassDescription GetClassDescription(string id) => Classes.Collection.Get(id);
        public bool TryGetClassDescription(string id, out ClassDescription desc) => Classes.Collection.All.TryGetConverted(id, out desc, e => e);

        public IDictionary<string, BackgroundDescription> GetBackgroundDescriptions() => Backgrounds.Collection.All.Remap(w => (BackgroundDescription)w);
        public BackgroundDescription GetBackgroundDescription(string id) => Backgrounds.Collection.Get(id);
        public bool TryGetBackgroundDescription(string id, out BackgroundDescription desc) => Backgrounds.Collection.All.TryGetConverted(id, out desc, e => e);
    }
}