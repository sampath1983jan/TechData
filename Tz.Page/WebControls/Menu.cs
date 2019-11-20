using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.Page.WebControls
{
    
    public class Menu <T> : Controls.Webcontrol
    {
        private readonly T _value;
        private readonly List<Menu<T>> _children = new List<Menu<T>>();

        public Menu(T value)
        {
            _value = value;
        }

        public Menu<T> this[int i]
        {
            get { return _children[i]; }
        }

        public Menu<T> Parent { get; private set; }

        public T Value { get { return _value; } }

        public ReadOnlyCollection<Menu<T>> Children
        {
            get { return _children.AsReadOnly(); }
        }

        public Menu<T> AddChild(T value)
        {
            var node = new Menu<T>(value) { Parent = this };
            _children.Add(node);
            return node;
        }

        public Menu<T>[] AddChildren(params T[] values)
        {
            return values.Select(AddChild).ToArray();
        }

        public bool RemoveChild(Menu<T> node)
        {
            return _children.Remove(node);
        }

        public void Traverse(Action<T> action)
        {
            action(Value);
            foreach (var child in _children)
                child.Traverse(action);
        }

        public IEnumerable<T> Flatten()
        {
            return new[] { Value }.Concat(_children.SelectMany(x => x.Flatten()));
        }
    }

    public class MenuItem {
        public string MenuID { get; set; } 
        public string Name { get; set; }
        public string Css { get; set; }
        public string SelectEvent { get;set; }
    }
}
