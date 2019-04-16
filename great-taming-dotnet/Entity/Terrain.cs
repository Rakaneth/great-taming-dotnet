using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoRogue;
using Microsoft.Xna.Framework;
using GoRogue.GameFramework;
using SadConsole;


namespace GreatTaming.Entity {
    public class Terrain : GameObject {

        private Color fg;
        private Color bg;
        private int glyph;
        public string Name { get; }
        public Cell DrawCell => new Cell(fg, bg, glyph);

        private Terrain(string name, int glyph, Coord pos, Color? foreground = null, Color? background = null, bool isWalkable = true, bool isVisible = true)
            : base(pos, 0, null, true, isWalkable, isVisible) {
            Name = name;
            fg = foreground ?? Color.Transparent;
            bg = background ?? Color.Transparent;
            this.glyph = glyph;
        }

        public static Terrain Floor(Coord pos, Color floorColor) {
            return new Terrain("Floor", ' ', pos, background: floorColor);
        }

        public static Terrain Wall(Coord pos, Color wallColor) {
            return new Terrain("Wall", ' ', pos, background: wallColor, isWalkable: false, isVisible: false);
        }

        public static Terrain ClosedDoor(Coord pos) {
            return new Terrain("Closed door", '+', pos, foreground: Color.White, background: Color.Sienna);
        }

        public static Terrain OpenDoor(Coord pos) {
            return new Terrain("Open door", '/', pos, foreground: Color.White, background: Color.Sienna);
        }

        public static Terrain NullTile(Coord pos) {
            return new Terrain("", 0, pos, isWalkable: false, isVisible: false);
        }

        public static Terrain DownStairs(Coord pos) {
            return new Terrain("Stairs down", '>', pos, foreground: Color.Yellow);
        }

        public static Terrain UpStairs(Coord pos) {
            return new Terrain("Stairs up", '<', pos, foreground: Color.Yellow);
        }
    }
}
