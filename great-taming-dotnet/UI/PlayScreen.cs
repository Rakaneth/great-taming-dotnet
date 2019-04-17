using System.Linq;
using GoRogue;
using GreatTaming.Engine;
using GreatTaming.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SadConsole;
using Console = SadConsole.Console;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

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
            CenterOnObject(context.Player);
            return new UICommand();
        }

        private void CenterOnObject(Mobile m) {
            mapCons.CenterViewPortOnPoint(m.Position);
        }


        private void loadMap() {
            var m = context.CurMap;
            mapCons.Children.Clear();
            mapCons.Clear();
            for (int x = 0; x < m.Width; x++) {
                for (int y = 0; y < m.Height; y++) {
                    var t = m.Terrain[x, y] as Terrain;
                    mapCons.SetCellAppearance(x, y, t.DrawCell);
                }
            }                                               
            foreach (var thing in m.Entities.Items.OfType<Mobile>()) {
                mapCons.Children.Add(thing.DrawEntity);
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
