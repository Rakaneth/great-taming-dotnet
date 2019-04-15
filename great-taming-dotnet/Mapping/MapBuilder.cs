using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoRogue.GameFramework;
using GoRogue.MapGeneration;
using GoRogue.MapViews;
using GreatTaming.Engine;
using GreatTaming.Entity;
using GoRogue;

namespace GreatTaming.Mapping
{
    public enum MapType
    {
        DUNGEON,
        CAVES
    }

    public static class MapBuilder
    {
        public static Map Build(int width, int height, string id, GameContext context, MapType typ)
        {
            var baseMap = new ArrayMap<bool>(width, height);
            List<Coord> doors = new List<Coord>();
            switch (typ)
            {
                case MapType.DUNGEON:
                    var dunResult = QuickGenerators.GenerateDungeonMazeMap(baseMap, context.RNG, 10, 50, 5, 21);
                    foreach (var (room, connectors) in dunResult)
                    {
                        foreach (var side in connectors)
                        {
                            foreach (var door in side)
                            {
                                doors.Add(door);
                            }
                        }
                    }
                    break;
                case MapType.CAVES:
                    var caveResult = QuickGenerators.GenerateCellularAutomataMap(baseMap, context.RNG);
                    break;
            }
            var trans = new TerrainTranslator(baseMap);
            foreach (var newDoor in doors) {
                trans[newDoor] = Terrain.ClosedDoor(newDoor);
            }
            var fullMap = Map.CreateMap(trans, 4, Distance.CHEBYSHEV);
            context.RegisterMap(id, fullMap);
            return fullMap;
        }
    }
}
