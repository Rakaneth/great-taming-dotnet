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
using Microsoft.Xna.Framework;

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
            var baseMap = new ArrayMap<Terrain>(width, height);
            var trans = new TerrainTranslator(baseMap, Color.Gray, Color.LightGray);

            List<Coord> doors = new List<Coord>();
            switch (typ)
            {
                case MapType.DUNGEON:
                    var dunResult = QuickGenerators.GenerateDungeonMazeMap(trans, context.RNG, 10, 50, 5, 21);
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
                    var caveResult = QuickGenerators.GenerateCellularAutomataMap(trans, context.RNG);
                    break;
            }
            
            var fullMap = Map.CreateMap(baseMap, 4, Distance.CHEBYSHEV);
            foreach (var newDoorSpot in doors) {
                fullMap.SetTerrain(Terrain.ClosedDoor(newDoorSpot));
            }
            context.RegisterMap(id, fullMap);
            return fullMap;
        }
    }
}
