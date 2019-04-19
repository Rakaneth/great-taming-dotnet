using System;
using System.Collections.Generic;
using GoRogue;
using GoRogue.GameFramework;
using GoRogue.MapGeneration;
using GoRogue.MapViews;
using GreatTaming.Engine;
using GreatTaming.Entity;
using Microsoft.Xna.Framework;
using GreatTaming.UI;

namespace GreatTaming.Mapping {
    public enum MapType {
        DUNGEON,
        CAVES
    }

    public static class MapBuilder {
        public static Map Build(int width, int height, string id, GameContext context, MapType typ) {
            var baseMap = new ArrayMap<Terrain>(width, height);
            var trans = new TerrainTranslator(baseMap, Swatch.StoneWall, Swatch.StoneFloor);

            List<Coord> doors = new List<Coord>();
            switch (typ) {
                case MapType.DUNGEON:
                    var dunResult = QuickGenerators.GenerateDungeonMazeMap(trans, context.RNG, 10, 50, 5, 21);
                    foreach (var (room, connectors) in dunResult) {
                        foreach (var side in connectors) {
                            foreach (var door in side) {
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
                fullMap.AddEntity(new Door(newDoorSpot, false));
            }
            context.RegisterMap(id, fullMap);
            return fullMap;
        }

        public static Map FromFile(string fileName, string id, Color wallColor, Color floorColor, GameContext context) {
            string[] lines = System.IO.File.ReadAllLines(fileName);
            int w = lines[0].Length;
            int h = lines.Length;
            var newMap = new ArrayMap<Terrain>(w, h);
            var closeDoors = new List<Coord>();
            var openDoors = new List<Coord>();

            for (int y = 0; y < lines.Length; y++) {
                var line = lines[y];
                if (line.Length != w) {
                    throw new ArgumentException($"Error reading row {y + 1} of map file. Make sure all rows are the same length. Pad rows with 'x' to represent null tiles and avoid empty rows.");
                }
                for (int x = 0; x < lines[0].Length; x++) {
                    var c = lines[y][x];
                    switch (c) {
                        case '#':
                            newMap[x, y] = Terrain.Wall((x, y), wallColor);
                            break;
                        case '.':
                            newMap[x, y] = Terrain.Floor((x, y), floorColor);
                            break;
                        case '+':
                            closeDoors.Add((x, y));
                            break;
                        case '/':
                            openDoors.Add((x, y));
                            break;
                        case 'x':
                            newMap[x, y] = Terrain.NullTile((x, y));
                            break;
                        case '>':
                            newMap[x, y] = Terrain.DownStairs((x, y));
                            break;
                        case '<':
                            newMap[x, y] = Terrain.UpStairs((x, y));
                            break;
                        default:
                            newMap[x, y] = Terrain.NullTile((x, y));
                            break;
                    }
                }
            }
            var fullMap = Map.CreateMap(newMap, 4, Distance.CHEBYSHEV);
            foreach (var cd in closeDoors) {
                fullMap.AddEntity(new Door(cd, false));
            }
            foreach (var od in openDoors) {
                fullMap.AddEntity(new Door(od, true));
            }
            context.RegisterMap(id, fullMap);
            return fullMap;
        }
    }
}
