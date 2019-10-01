using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tz.BackApp.Models
{
    public class GridParam
    {
        public Paging Paging;
        public GridSearch Search;
    }
    public class Paging
    {
        public int currentPageIndex;
        public int pageSize;
        public int totalRecord;
    }

    public class GridSearch {
        public string Condition;
        public string SearchFields;
        public string SearchType;
        public List<SearchValue> SearchValues;
    }

    public class SearchValue {
        public string Value;
    }
       
}