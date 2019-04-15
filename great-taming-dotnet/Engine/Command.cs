﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreatTaming.Entity;

namespace GreatTaming.Engine
{
    interface ICommand
    {
        CommandResult Execute(Mobile entity);
    }
}
