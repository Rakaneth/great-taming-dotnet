using GreatTaming.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreatTaming.UI
{
    abstract class UI
    {
        string Name { get;  }
        public UI(string name)
        {
            Name = name;
        }
        public abstract void render();
        public abstract ICommand handle();
        public void enter()
        {
            System.Console.WriteLine($"Entered {Name} screen.");
        }
        public void exit()
        {
            System.Console.WriteLine($"Exited {Name} screen.");
        }
    }
}
