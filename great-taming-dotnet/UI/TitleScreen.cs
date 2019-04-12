using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreatTaming.Engine;
using Console = SadConsole.Console;

namespace GreatTaming.UI
{
    class TitleScreen: UI
    {
        Console title = new Console(100, 40);
        
        public TitleScreen(): base("title")
        {
        }

        public override ICommand Handle()
        {
            UIManager.SetUI("main");
            return new UICommand();
        }

        protected override void init()
        {
            title.Border("Welcome to the Great Taming");
            title.WriteCenter(20, "Chroncles of Salaban: The Great Taming", "yellow");
            title.WriteCenter(21, "by Rakaneth");
            title.WriteCenter(23, "Press any key to continue");
            Children.Add(title);
        }
    }
}
