using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.ClientManager
{
    public interface INetImplimentor
    {
        void Load();
        bool Save();
        bool Remove();
    }
}
