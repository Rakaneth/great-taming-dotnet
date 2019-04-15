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

        public static Terrain StoneFloor(Coord pos) {
            return new Terrain("Stone floor", ' ', pos, background: Color.LightGray);
        }

        public static Terrain StoneWall(Coord pos) {
            return new Terrain("Stone wall", ' ', pos, background: Color.Gray, isWalkable: false, isVisible: false);
        }

        public static Terrain WoodWall(Coord pos) {
            return new Terrain("Wood wall", ' ', pos, background: Color.DarkGoldenrod, isWalkable: false, isVisible: false);
        }

        public static Terrain WoodFloor(Coord pos) {
            return new Terrain("Wood floor", ' ', pos, background: Color.Goldenrod);
        }

        public static Terrain ClosedDoor(Coord pos) {
            return new Terrain("Closed door", '+', pos, foreground: Color.White, background: Color.Sienna);
        }
    }
}
