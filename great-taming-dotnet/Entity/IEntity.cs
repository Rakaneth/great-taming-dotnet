using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoRogue;
namespace GreatTaming.Entity
{
    interface IEntity
    {
        Coord Pos { get; set; }
        UInt32 ID { get;  }
        string Name { get; set; }
        string Desc { get; set; }
    }
}
