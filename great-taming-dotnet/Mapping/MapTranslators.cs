using GoRogue.MapViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreatTaming.Entity;
using GoRogue;

namespace GreatTaming.Mapping
{
    class TerrainTranslator: SettableTranslationMap<bool, Terrain>
    {
        public TerrainTranslator(ISettableMapView<bool> baseMap) : base(baseMap) { }

        protected override Terrain TranslateGet(Coord position, bool value)
        {
            return value ? Terrain.StoneFloor(position) : Terrain.StoneWall(position);
        }

        protected override bool TranslateSet(Terrain value)
        {
            return value.Name.Contains("wall");
        }
    }
}
