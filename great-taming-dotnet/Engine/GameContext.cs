﻿using System;
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
using Microsoft.Xna.Framework;

namespace GreatTaming.Engine {
    public class GameContext {
        private Dictionary<string, Map> maps = new Dictionary<string, Map>();
        public string CurMapID { get; private set; }
        public Map CurMap { get { return maps[CurMapID]; } }
        public IGenerator RNG { get; }
        public Mobile Player { get; private set; }

        public GameContext(uint? seed = null) {
            if (seed == null) {
                RNG = new XorShift128Generator();
            } else {
                RNG = new XorShift128Generator(seed.Value);
            }

        }

        public void RegisterMap(string id, Map map) {
            maps[id] = map;
        }

        public void RemoveMap(string id) {
            maps.Remove(id);
        }

        public void SetPlayer(Mobile player) {
            Player = player;
            Player.SetPlayer();
        }

        public void SetCurMap(string mapID) {
            CurMapID = mapID;
            //TODO: scheduler stuff
        }

        public bool changeMap(Mobile e, string mapID) {
            var m = maps[mapID];
            return m.AddEntity(e);
        }

        public static GameContext NewGame(uint? seed = null) {
            var ctx = new GameContext(seed);
            var m = MapBuilder.Build(85, 85, "mines", ctx, MapType.DUNGEON);
            var m2 = MapBuilder.FromFile("testfixed.txt", "text", Color.Gray, Color.LightGray, ctx);
            var randPos = m.Terrain.RandomPosition((c, t) => t.IsWalkable, ctx.RNG);
            ctx.Player = Mobile.TestPlayer(randPos);
            ctx.SetCurMap("mines");
            ctx.changeMap(ctx.Player, "mines");
            ctx.CurMap.CalculateFOV(randPos, 6);
            return ctx;
        }
    }
}
