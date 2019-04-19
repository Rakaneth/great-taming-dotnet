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

    public class Mobile : DrawableObject {
        public bool IsPlayer { get; private set; }
        private Mobile(string name, string desc, int glyph, Coord? pos = null, Color? foreground = null, Color? background = null, int layer = 3)
            : base(name, desc, glyph, pos, foreground, background, false, true, true, 3)  {
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
            var player = new Mobile("Player", "A test subject", '@', pos: pos, layer: 4);
            player.AddComponent(new ObPrimaryStats());
            return player;
        }
    }
}
