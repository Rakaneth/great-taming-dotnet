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
            var justPressed = SadConsole.Global.KeyboardState.KeysPressed.FirstOrDefault();
            System.Console.WriteLine($"Key {justPressed.Key} pressed.");
            return new UICommand();
        }

        protected override void init()
        {
            writeCenter(title, 20, "Chroncles of Salaban: The Great Taming", "yellow");
            writeCenter(title, 21, "by Rakaneth");
            writeCenter(title, 21, "Press any key to continue");
            Children.Add(title);
        }
    }
}
