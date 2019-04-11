using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreatTaming.Entity;

namespace GreatTaming.Engine
{
    class UICommand : ICommand
    {
        public CommandResult Execute(GameEntity entity)
        {
            return new CommandResult(cost: 0);
        }
    }
}
