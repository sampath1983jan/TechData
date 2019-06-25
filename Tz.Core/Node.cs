using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Tz.Core
{
     
        public class Node<T>
        {
            private readonly T _value;
            private readonly List<Node<T>> _children = new List<Node<T>>();

            public Node(T value)
            {
                _value = value;
            }

        
            public Node<T> this[int i]
            {
                get { return _children[i]; }
            }

            public Node<T> Parent { get; private set; }

            public T Value { get { return _value; } }

            public List<Node<T>> Children
            {
                get { return _children; }
            }

            public Node<T> AddChild(T value)
            {
                var node = new Node<T>(value) { Parent = this };
                _children.Add(node);
                return node;
            }

        public Node<T> AddChild(Node<T> value)
        {
            var node = value;
            node.Parent = this;
            _children.Add(node);
            return node;
        }

        public Node<T>[] AddChildren(params T[] values)
            {
                return values.Select(AddChild).ToArray();
            }

            public bool RemoveChild(Node<T> node)
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
    
}
