using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoRogue.GameFramework;
using Troschuetz.Random;
using Troschuetz.Random.Generators;
using GoRogue.MapViews;
using GreatTaming.Entity;
using GreatTaming.Mapping;

namespace GreatTaming.Engine
{
    public class GameContext
    {
        private Dictionary<string, Map> maps = new Dictionary<string, Map>();
        public string CurMapID { get; private set; }
        public Map CurMap => maps[CurMapID];
        public IGenerator RNG { get; }
        public GameEntity Player { get; private set; }

        public GameContext(uint? seed=null)
        {
            if (seed == null)
            {
                RNG = new XorShift128Generator();
            }
            else
            {
                RNG = new XorShift128Generator(seed.Value);
            }
            
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

        public bool changeMap(GameEntity e, string mapID)
        {
            var m = maps[mapID];
            return m.AddEntity(e);
        }

        public static GameContext NewGame(uint? seed)
        {
            var ctx = new GameContext(seed);
            var m = MapBuilder.Build(85, 85, "mines", ctx, MapType.DUNGEON);
            var randPos = m.Terrain.RandomPosition((c, t) => t.IsWalkable,  ctx.RNG);
            ctx.Player = Mobile.TestPlayer(randPos);
            ctx.SetCurMap("mines");
            ctx.changeMap(ctx.Player, "mines");
            return ctx;
        }
    }
}
