using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoRogue.GameFramework;
using GoRogue;

namespace GreatTaming.Mapping {
    public static class MapExtensions {
        public static IEnumerable<Coord> GetExplored(this Map m) {
            for (int x=0; x<m.Width; x++) {
                for (int y=0; y<m.Height; y++) {
                    if (m.Explored[x, y]) {
                        yield return (x, y);
                    }
                }
            }
        }
    }
}
