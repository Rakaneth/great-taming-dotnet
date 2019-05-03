using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SadConsole;
using SadConsole.Components;
using GreatTaming.Engine;
using GreatTaming.Entity;
using Microsoft.Xna.Framework;

namespace GreatTaming.UI {
    class MapConsoleLogic : LogicConsoleComponent {

        private GameContext ctx;

        public MapConsoleLogic(GameContext ctx) {
            this.ctx = ctx;
        }

        public override void Draw(SadConsole.Console console, TimeSpan delta) {
            
        }

        public override void Update(SadConsole.Console console, TimeSpan delta) {
            var m = ctx.CurMap;
            foreach (var nPos in m.FOV.NewlyUnseen) {
                foreach (var vChild in m.GetEntities<DrawableObject>(nPos)) {
                    vChild.DrawEntity.IsVisible = false;
                }
                if (m.Explored[nPos]) {
                    var eBG = (m.Terrain[nPos] as Terrain).Name == "Wall" ? Swatch.ExploredWall : Swatch.ExploredFloor;        
                    console.SetCellAppearance(nPos.X, nPos.Y, new Cell(Color.Transparent, eBG));
                } else {
                    console[nPos.X, nPos.Y].IsVisible = false;
                }
            }
            foreach (var sPos in m.FOV.NewlySeen) {
                console[sPos.X, sPos.Y].IsVisible = true;
                console.SetCellAppearance(sPos.X, sPos.Y, (m.Terrain[sPos] as Terrain).DrawCell);
                foreach (var obj in m.GetEntities<DrawableObject>(sPos)) {
                    obj.DrawEntity.IsVisible = true;
                }
            }

            //console.Update(delta);
        }
    }
}
