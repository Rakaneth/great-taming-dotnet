using GreatTaming.Engine;
using System;
using System.Collections.Generic;
using SadConsole;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;


namespace GreatTaming.UI
{
    abstract class UI : SadConsole.ContainerConsole
    {
        public string Name { get;  }
        public UI(string name)
        {
            Name = name;
            IsVisible = false;
            IsFocused = false;
            init();
        }

        protected abstract void init();
        public abstract ICommand Handle();
        public void Enter()
        {
            System.Console.WriteLine($"Entered {Name} screen.");
        }
        public void Exit()
        {
            System.Console.WriteLine($"Exited {Name} screen.");
        }

        protected string decorate(string text, string fg, string bg="default")
        {
            return $"[c:r f:{fg}][c:r b:{bg}]{text}[c:undo][c:undo]";
        }

        protected void writeCenter(Console cons, int y, string text, string fg="default", string bg="default")
        {
            var x = (cons.Width - text.Length) / 2;
            cons.Cursor.Position = new Point(x, y);
            cons.Cursor.Print(decorate(text, fg, bg));
        }
    }
}
