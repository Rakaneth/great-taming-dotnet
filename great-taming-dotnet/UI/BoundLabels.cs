using System;
using SadConsole.Controls;
using GreatTaming.Entity;
using Microsoft.Xna.Framework;

namespace GreatTaming.UI {
    public class PriStatsDisplay : DrawingSurface, IObserver<PrimaryStat> {

        private PrimaryStat data;

        public PriStatsDisplay(Point pos) : base(8, 6) {
            Position = pos;
            OnDraw = (c) => {
                Surface.Print(0, 0, $"Str {data.Str,4}");
                Surface.Print(0, 1, $"Stam {data.Stam,3}");
                Surface.Print(0, 2, $"Spd {data.Spd,4}");
                Surface.Print(0, 3, $"Skl {data.Skl,4}");
                Surface.Print(0, 4, $"Sag {data.Sag,4}");
                Surface.Print(0, 5, $"Smt {data.Smt,4}");
            };
        }

        public void OnCompleted() {
            throw new NotImplementedException();
        }

        public void OnError(Exception error) {
            throw new NotImplementedException();
        }

        public void OnNext(PrimaryStat value) {
            data = value;
            IsDirty = true;
        }
    }
}
