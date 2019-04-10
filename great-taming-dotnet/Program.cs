using System;
using SadConsole;
using Microsoft.Xna.Framework;
using Console = SadConsole.Console;

namespace GreatTaming
{
    class Program
    {
        static void Main(string[] args)
        {
            SadConsole.Game.Create(100, 40);
            SadConsole.Game.OnInitialize = Init;
            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }

        static void Init()
        {
            var console = new Console(100, 40);
            console.FillWithRandomGarbage();
            console.Fill(new Rectangle(3, 3, 23, 3), Color.Violet, Color.Black, 0, 0);
            console.Print(4, 4, "Hello, SadConsole!");
            SadConsole.Global.CurrentScreen = console;
        }
    }
}
