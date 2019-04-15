﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreatTaming.Engine;
using GreatTaming.Entity;
using SadConsole;
using SadConsole.Entities;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;
using GoRogue.GameFramework;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using GoRogue;

namespace GreatTaming.UI {
    class MainScreen : UI {
        private GameContext context;
        private ScrollingConsole mapCons = new ScrollingConsole(100, 100, new Rectangle(0, 0, 60, 30)) {
            Position = new Point(0, 0)
        };
        private Console msgCons = new Console(30, 10) {
            Position = new Point(0, 30)
        };
        private Console skillCons = new ControlsConsole(30, 10) {
            Position = new Point(30, 30)
        };
        private Console infoCons = new Console(40, 10) {
            Position = new Point(60, 30)
        };
        private Console statCons = new Console(40, 30) {
            Position = new Point(60, 0)
        };

        public MainScreen(GameContext context) : base("main") {
            this.context = context;
            loadMap();
            mapCons.CenterViewPortOnPoint(context.Player.Position);
        }
        public override ICommand Handle() {
            var justPressed = SadConsole.Global.KeyboardState.KeysPressed.FirstOrDefault();
            System.Console.WriteLine($"Key {justPressed.Key} pressed.");
            return new UICommand();
        }


        private void loadMap() {
            var m = context.CurMap;
            mapCons.Children.Clear();
            for (int x = 0; x<m.Width; x++) {
                for (int y=0; y<m.Height; y++) {
                    var terrain = m.Terrain[x, y] as Terrain;
                    mapCons.Children.Add(terrain.DrawEntity);
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
