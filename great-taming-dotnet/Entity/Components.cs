using GoRogue.GameFramework.Components;
using GoRogue.GameFramework;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GreatTaming.Entity
{
    public class PrimaryStat {
        internal int Str{ get; set; }
        internal int Stam{ get; set; }
        internal int Spd{ get; set; }
        internal int Skl { get; set; }
        internal int Sag { get; set; }
        internal int Smt { get; set; }

        internal PrimaryStat(int str, int stam, int spd, int skl, int sag, int smt) {
            Str = str;
            Stam = stam;
            Skl = skl;
            Spd = spd;
            Sag = sag;
            Smt = smt;
        }
    }

    internal class Unsubscriber<T> : IDisposable {
        private List<IObserver<T>> observers;
        private IObserver<T> observer;

       internal Unsubscriber (List<IObserver<T>> observers, IObserver<T> observer) {
            this.observers = observers;
            this.observer = observer;
        }

        public void Dispose() {
            if (observers.Contains(observer)) {
                observers.Remove(observer);
            }
        }
    }

    abstract public class ObservableComponent<T> : IGameObjectComponent, IObservable<T> {
        public IGameObject Parent { get; set; }
        protected List<IObserver<T>> observers;
        protected T data;

        protected ObservableComponent(T data) {
            this.data = data;
            observers = new List<IObserver<T>>();
        }
        
        public IDisposable Subscribe(IObserver<T> observer) {
            if (!observers.Contains(observer)) {
                observers.Add(observer);
            }
            foreach (var ob in observers) {
                ob.OnNext(data);
            }
            return new Unsubscriber<T>(observers, observer);
        }

        protected void NotifyObservers() {
            foreach (var ob in observers) {
                ob.OnNext(data);
            }
        }
    }

    public class ObPrimaryStats : ObservableComponent<PrimaryStat> {
        public int Str {
            get => data.Str;
            set {
                data.Str = value;
                NotifyObservers();
            }
        }

        public int Stam {
            get => data.Stam;
            set {
                data.Stam = value;
                NotifyObservers();
            }
        }

        public int Spd {
            get => data.Spd;
            set {
                data.Spd = value;
                NotifyObservers();
            }
        }

        public int Skl {
            get => data.Skl;
            set {
                data.Skl = value;
                NotifyObservers();
            }
        }

        public int Sag {
            get => data.Sag;
            set {
                data.Sag = value;
                NotifyObservers();
            }
        }

        public int Smt {
            get => data.Smt;
            set {
                data.Smt = value;
                NotifyObservers();
            }
        }



        public ObPrimaryStats(int str=10, int stam=10, int spd=10, int skl=10, int sag=10, int smt=10)
            : base(new PrimaryStat(str, stam, spd, skl, sag, smt)) {}
        
    }


}