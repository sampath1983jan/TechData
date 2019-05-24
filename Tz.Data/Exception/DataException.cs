using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.Data.Exception
{
    [Serializable]
    public class DataException: System.Exception
    {
        public DataException() : base() {

        }
        public DataException(string message) : base(message)
        {

        }
        public DataException(string message, System.Exception ex) : base(message,ex)
        {

        }
    }
}



