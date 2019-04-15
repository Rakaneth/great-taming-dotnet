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
    class Terrain : GameObject {

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
            return new Terrain("Stone floor", ' ', pos, background: floorColor);
        }

        public static Terrain Wall(Coord pos, Color wallColor) {
            return new Terrain("Stone wall", ' ', pos, background: wallColor, isWalkable: false, isVisible: false);
        }

        public static Terrain ClosedDoor(Coord pos) {
            return new Terrain("Closed door", '+', pos, foreground: Color.White, background: Color.Sienna);
        }

        public static Terrain OpenDoor(Coord pos) {
            return new Terrain("Open door", '/', pos, foreground: Color.White, background: Color.Sienna);
        }
    }
}
