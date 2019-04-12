using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoRogue;
using GoRogue.GameFramework;
using Microsoft.Xna.Framework;



namespace GreatTaming.Entity
{
    public class GameEntityBuilder
    {
        public Color fg;
        public Color bg;
        public int glyph;
        public int layer;
        public bool isWalkable;
        public bool isTransparent;
        public bool isStatic;
        public string name;
        public string desc;
        public Coord pos;

        public GameEntityBuilder()
        {
            fg = Color.White;
            bg = Color.Transparent;
            glyph = 0;
            layer = 0;
            isWalkable = true;
            isTransparent = true;
            isStatic = false;
            name = "No name";
            desc = "No desc";
            pos = Coord.NONE;
        }

    }

    public class GameEntity : SadConsole.Entities.Entity, IGameObject
    {
        private IGameObject _go;
        public string Desc { get; private set; }

        public GameEntity(GameEntityBuilder opts) : base(opts.fg, opts.bg, opts.glyph)
        {
            //TODO: MAJOR REWORK: use base GameObjects and give them components like a sane person
            _go = new GameObject(opts.pos, opts.layer, null, opts.isStatic, opts.isWalkable, opts.isTransparent);
            Name = opts.name;
            Desc = opts.desc;
            Position = opts.pos;
            Components.Add(new SadConsole.Components.EntityViewSyncComponent());
        }

        public Map CurrentMap => _go.CurrentMap;

        public bool IsStatic => _go.IsStatic;

        public bool IsTransparent { get => _go.IsTransparent; set => _go.IsTransparent = value; }
        public bool IsWalkable { get => _go.IsWalkable; set => _go.IsWalkable = value; }

        public uint ID => _go.ID;

        public int Layer => _go.Layer;

        Coord IGameObject.Position {
            get => _go.Position;
            set {
                _go.Position = value;
                Position = value;
            }
        }

        event EventHandler<ItemMovedEventArgs<IGameObject>> IGameObject.Moved
        {
            add
            {
                _go.Moved += value;
            }

            remove
            {
                _go.Moved -= value;
            }
        }

        public void AddComponent(object component)
        {
            _go.AddComponent(component);
        }

        public T GetComponent<T>()
        {
            return _go.GetComponent<T>();
        }

        public IEnumerable<T> GetComponents<T>()
        {
            return _go.GetComponents<T>();
        }

        public bool HasComponent(Type componentType)
        {
            return _go.HasComponent(componentType);
        }

        public bool HasComponent<T>()
        {
            return _go.HasComponent<T>();
        }

        public bool HasComponents(params Type[] componentTypes)
        {
            return _go.HasComponents(componentTypes);
        }

        public bool MoveIn(Direction direction)
        {
            return _go.MoveIn(direction);
        }

        public void OnMapChanged(Map newMap)
        {
            _go.OnMapChanged(newMap);
        }

        public void RemoveComponent(object component)
        {
            _go.RemoveComponent(component);
        }

        public void RemoveComponents(params object[] components)
        {
            _go.RemoveComponents(components);
        }
    }
}
