using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserableCollection.FunctionInInterface
{
    public interface IInterface
    {
        Func<int, double?> GetFunc();
    }
}
