using System;
using SadConsole;
using Microsoft.Xna.Framework;
using Console = SadConsole.Console;
using GreatTaming.UI;

namespace GreatTaming
{
    class Program
    {
        static void Main(string[] args)
        {
            SadConsole.Game.Create(100, 40);
            SadConsole.Game.OnInitialize = Init;
            SadConsole.Game.OnUpdate = Update;
            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }

        static void Init()
        {
            UIManager.Register(new TitleScreen());
            UIManager.SetUI("title");
        }

        static void Update(GameTime t)
        {
            UIManager.CurrentUI.Handle();
        }
    }
}
