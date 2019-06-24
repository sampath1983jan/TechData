using System;
using System.Collections.Generic;
using System.Text;

namespace Tz.Core
{
  public  class ComponentNode
    {
        private Component Component;
        public LinkComponentField Relation;
        public int Left;
        public int Right;
        public string ComponentID;

    }

    public class LinkComponentField {
        public string ParentField;
        public string RelatedField;
    }
}
