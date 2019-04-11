using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoRogue.GameFramework;
using Troschuetz.Random;
using Troschuetz.Random.Generators;
using GreatTaming.Entity;

namespace GreatTaming.Engine
{
    public class GameContext
    {
        private Dictionary<string, Map> maps = new Dictionary<string, Map>();
        public string CurMapID { get; private set; }
        public Map CurMap => maps[CurMapID];
        public IGenerator RNG { get; }
        public GameEntity Player { get; private set; }

        public GameContext(uint seed)
        {
            RNG = new XorShift128Generator(seed);
        }

        public GameContext()
        {
            RNG = new XorShift128Generator();
        }

        public void RegisterMap(string id, Map map)
        {
            maps[id] = map;
        }

        public void RemoveMap(string id)
        {
            maps.Remove(id);
        }

        public void SetPlayer(GameEntity player)
        {
            Player = player;
        }

        public void SetCurMap(string mapID)
        {
            CurMapID = mapID;
            //TODO: scheduler stuff
        }
    }
}
