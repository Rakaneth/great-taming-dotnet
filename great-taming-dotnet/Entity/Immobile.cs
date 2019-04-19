using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoRogue;
using GoRogue.GameFramework;
using GreatTaming.UI;
using Microsoft.Xna.Framework;
using SadConsole.Components;

namespace GreatTaming.Entity {

    abstract public class Immobile: DrawableObject {
        internal Immobile(string name, string desc, int glyph, Coord? pos = null, Color? foreground = null, Color? background = null, bool isWalkable = true, bool isVisible = true)
            : base(name, desc, glyph, pos, foreground, background, true, isVisible, isWalkable, 1) { }
        
    }

    public class Door: Immobile {
        public bool IsOpen { get; private set; }
        public Door(Coord pos, bool startOpen)
            : base("Door", "A door", startOpen ? '/' : '+' , pos, Color.White, Swatch.Door, startOpen, startOpen) {
            IsOpen = startOpen;
        }
        
        public void Close() {
            if (IsOpen) {
                IsOpen = false;
                DrawEntity.Animation.CurrentFrame[0].Glyph = '+';
                IsWalkable = false;
                IsTransparent = false;
            }
        }

        public void Open() {
            if (!IsOpen) {
                IsOpen = true;
                DrawEntity.Animation.CurrentFrame[0].Glyph = '/';
                IsWalkable = true;
                IsTransparent = true;
            }
        }

        public void Toggle() {
            if (IsOpen) {
                Close();
            } else {
                Open();
            }
        }
    }
}
