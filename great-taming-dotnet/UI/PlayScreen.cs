using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreatTaming.Engine;
using SadConsole;
using SadConsole.Entities;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;
using GoRogue.GameFramework;
using GoRogue;

namespace GreatTaming.UI
{
    class MainScreen : UI
    {
        private GameContext context;
        private ScrollingConsole mapCons = new ScrollingConsole(100, 100, new Rectangle(0, 0, 60, 30))
        {
            Position = new Point(0, 0)
        };
       private Console msgCons = new Console(30, 10)
        {
            Position = new Point(0, 30)
        };
        private Console skillCons = new ControlsConsole(30, 10)
        {
            Position = new Point(30, 30)
        };
        private Console infoCons = new Console(40, 10)
        {
            Position = new Point(60, 30)
        };
        private Console statCons = new Console(40, 30)
        {
            Position = new Point(60, 0)
        };

        public MainScreen(GameContext context): base("main")
        {
            this.context = context;
        }
        public override ICommand Handle()
        {
            throw new NotImplementedException();
        }

        protected override void init()
        {
            foreach (var thing in context.CurMap.GetEntities<>)
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
