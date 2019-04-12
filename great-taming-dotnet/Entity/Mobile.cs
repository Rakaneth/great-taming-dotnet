using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoRogue;
using Microsoft.Xna.Framework;

namespace GreatTaming.Entity
{
    class MobileOpts: GameEntityBuilder
    {
        public MobileOpts(Coord pos, string name, string desc, int glyph, Color? fg=null, Color? bg = null)
        {
            this.fg = fg ?? Color.White;
            this.bg = bg ?? Color.Transparent;
            this.name = name;
            this.desc = desc;
            this.glyph = glyph;
            isWalkable = true;
            isTransparent = true;
            isStatic = false;
            this.pos = pos;
            this.layer = 3;
        }
    }

    class Mobile: GameEntity
    {
        Mobile(MobileOpts opts) : base(opts) { }

        public static Mobile TestPlayer(Coord pos)
        {
            var opts = new MobileOpts(pos, "Player", "The player!", '@', Color.Yellow);
            return new Mobile(opts);
        }
    }
}
