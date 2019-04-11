using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoRogue;
using Microsoft.Xna.Framework;

namespace GreatTaming.Entity
{
    public class TerrainOpts: GameEntityBuilder
    {
        public TerrainOpts(Coord pos, string name, string desc, int glyph, Color? fg=null, Color? bg=null, bool canWalk = true, bool canSee = true)
        {
            this.fg = fg ?? Color.White;
            this.bg = bg ?? Color.Transparent;
            this.name = name;
            this.desc = desc;
            this.glyph = glyph;
            isWalkable = canWalk;
            isTransparent = canSee;
            isStatic = true;
            this.pos = pos;
        }
    }

    class Terrain: GameEntity
    {
        private Terrain(TerrainOpts opts): base(opts)
        {
        }

        public static Terrain StoneFloor(Coord pos)
        {
            var stoneOpts = new TerrainOpts(
                pos, 
                "Stone floor",
                "A stone floor",
                ' ',
                null,
                Color.LightGray);
            return new Terrain(stoneOpts);
        }

        public static Terrain StoneWall(Coord pos)
        {
            var opts = new TerrainOpts(
                pos,
                "Stone wall",
                "A stone wall",
                ' ',
                null,
                Color.Gray,
                false,
                false);
            return new Terrain(opts);
        }

        public static Terrain WoodWall(Coord pos)
        {
            var opts = new TerrainOpts(
                pos,
                "Wooden wall",
                "A wooden wall",
                ' ',
                null,
                new Color(191, 127, 101),
                false, 
                false);
            return new Terrain(opts);
        }
    }
}
