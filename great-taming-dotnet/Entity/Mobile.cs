using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoRogue;
using GoRogue.GameFramework;
using Microsoft.Xna.Framework;
using SadConsole.Components;


namespace GreatTaming.Entity {

    public class Mobile : GameObject {
        public string Name { get; }
        public string Desc { get; }
        public bool IsPlayer { get; private set; }
        public SadConsole.Entities.Entity DrawEntity { get; private set; }
        private Mobile(string name, string desc, int glyph, Coord? pos = null, Color? foreground = null, Color? background = null, int layer = 3)
            : base(pos ?? (0, 0), layer, null, isWalkable: true, isTransparent: true) {
            var fg = foreground ?? Color.White;
            var bg = background ?? Color.Transparent;
            DrawEntity = new SadConsole.Entities.Entity(fg, bg, glyph);
            DrawEntity.Position = pos ?? (0, 0);
            DrawEntity.Components.Add(new EntityViewSyncComponent());
            Name = name;
            Desc = desc;
            Moved += Mobile_Moved;
            IsPlayer = false;
        }

        private void Mobile_Moved(object sender, ItemMovedEventArgs<IGameObject> e) {
            DrawEntity.Position = e.NewPosition;
        }

        public void SetPlayer() {
            IsPlayer = true;
        }

        public void UnsetPlayer() {
            IsPlayer = false;
        }

        public static Mobile TestPlayer(Coord pos) {
            return new Mobile("Player", "A test subject", '@', layer: 4);
        }
    }
}
