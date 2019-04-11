using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreatTaming.Engine
{
    class CommandResult
    {
        ICommand Alternate { get;  }
        bool Done { get;  }
        int Cost { get;  }

        public CommandResult(bool done = true, ICommand alternate = null, int cost = 100)
        {
            Done = done;
            Alternate = alternate;
            Cost = cost;
        }

    }
}
