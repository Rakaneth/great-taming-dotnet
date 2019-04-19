using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoRogue;
using Microsoft.Xna.Framework;
using GoRogue.GameFramework;
using SadConsole.Components;

namespace GreatTaming.Entity {
    abstract public class DrawableObject: GameObject {
        public string Name{ get; }
        public string Desc { get; }
        public SadConsole.Entities.Entity DrawEntity { get; set; }
        public DrawableObject(string name, string desc, int glyph, Coord? pos = null, Color? foreground = null, Color? background = null, bool isStatic = false, bool isTransparent = true, bool isWalkable = true, int layer=0)
            : base(pos ?? (0, 0), layer, null, isStatic, isWalkable, isTransparent) {
            Name = name;
            Desc = desc;
            var fg = foreground ?? Color.White;
            var bg = background ?? Color.Transparent;
            DrawEntity = new SadConsole.Entities.Entity(fg, bg, glyph);
            DrawEntity.Position = pos ?? (0, 0);
            DrawEntity.Components.Add(new EntityViewSyncComponent());
        }
    }
}
