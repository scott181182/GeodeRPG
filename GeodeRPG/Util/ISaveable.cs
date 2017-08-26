using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarioYeah.Util
{
    public interface ISaveable<T>
    {
        T Clone();
    }
}
