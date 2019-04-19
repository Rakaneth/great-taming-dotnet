using System.Linq;
using GoRogue;
using GreatTaming.Engine;
using GreatTaming.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SadConsole;
using Console = SadConsole.Console;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using GreatTaming.Mapping;

namespace GreatTaming.UI {
    class MainScreen : UI {
        private GameContext context;
        private ScrollingConsole mapCons = new ScrollingConsole(100, 100, new Rectangle(0, 0, 60, 30)) {
            Position = new Point(0, 0)
        };
        private Console msgCons = new Console(30, 10) {
            Position = new Point(0, 30)
        };
        private ControlsConsole skillCons = new ControlsConsole(30, 10) {
            Position = new Point(30, 30)
        };
        private Console infoCons = new Console(40, 10) {
            Position = new Point(60, 30)
        };
        private ControlsConsole statCons = new ControlsConsole(40, 30) {
            Position = new Point(60, 0)
        };

        public MainScreen(GameContext context) : base("main") {
            this.context = context;
            loadMap();
            loadEntities();
            CenterOnObject(context.Player);
            var statDisplay = new PriStatsDisplay(new Point(1, 1));
            context.Player.GetComponent<ObPrimaryStats>().Subscribe(statDisplay);
            statCons.Add(statDisplay);
        }
        public override ICommand Handle() {
            
            Direction d = Direction.NONE;
            var curKey = SadConsole.Global.KeyboardState.KeysPressed.FirstOrDefault();

            switch (curKey.Key) {
                case Keys.NumPad8:
                    d = Direction.UP;
                    break;
                case Keys.NumPad9:
                    d = Direction.UP_RIGHT;
                    break;
                case Keys.NumPad6:
                    d = Direction.RIGHT;
                    break;
                case Keys.NumPad3:
                    d = Direction.DOWN_RIGHT;
                    break;
                case Keys.NumPad2:
                    d = Direction.DOWN;
                    break;
                case Keys.NumPad1:
                    d = Direction.DOWN_LEFT;
                    break;
                case Keys.NumPad4:
                    d = Direction.LEFT;
                    break;
                case Keys.NumPad7:
                    d = Direction.UP_LEFT;
                    break;
                case Keys.S:
                    context.Player.GetComponent<ObPrimaryStats>().Str++;
                    break;
            }
            context.Player.Position = context.Player.Position.Translate(d.DeltaX, d.DeltaY);
            context.CurMap.CalculateFOV(context.Player.Position, 6);
            loadMap();
            CenterOnObject(context.Player);
            return new UICommand();
        }

        private void CenterOnObject(Mobile m) {
            mapCons.CenterViewPortOnPoint(m.Position);
        }


        private void loadMap() {
            var m = context.CurMap;     
            mapCons.Clear();
            foreach (var ePos in m.GetExplored()) {
                var eBG = (m.Terrain[ePos] as Terrain).Name == "Wall" ? Swatch.ExploredWall : Swatch.ExploredFloor;
                mapCons.SetCellAppearance(ePos.X, ePos.Y, new Cell(Color.Transparent, eBG));
            }
            foreach (var pos in m.FOV.CurrentFOV) {
                var t = m.Terrain[pos] as Terrain;
                mapCons.SetCellAppearance(pos.X, pos.Y, t.DrawCell);
            }
            foreach (var nPos in m.FOV.NewlyUnseen) {
                foreach (var vChild in Children.OfType<SadConsole.Entities.Entity>().Where(e => e.Position == nPos)) {
                    vChild.IsVisible = false;
                }
            }
            foreach (var sPos in m.FOV.NewlySeen) {
                foreach (var hChild in Children.OfType<SadConsole.Entities.Entity>().Where(e => e.Position == sPos)) {
                    hChild.IsVisible = true;
                }
            }
        }


        private void loadEntities() {
            var m = context.CurMap;
            mapCons.Children.Clear();
            foreach (var thing in m.Entities.Items.OfType<DrawableObject>()) {
                mapCons.Children.Add(thing.DrawEntity);
                if (!m.FOV.BooleanFOV[thing.DrawEntity.Position]) {
                    thing.DrawEntity.IsVisible = false;
                }
            }
        }

        protected override void init() {
            msgCons.Border("Messages");
            skillCons.Border("Skills");
            infoCons.Border("Info");
            statCons.Border("Stats");
            Children.Add(msgCons);
            Children.Add(skillCons);
            Children.Add(infoCons);
            Children.Add(statCons);
            Children.Add(mapCons);
        }
    }
}
