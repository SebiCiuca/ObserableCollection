using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserableCollection.FunctionInInterface
{
    public class UsableClass
    {
        private readonly IInterface _abstractFunctionGetter;

        public UsableClass(IInterface abstractFunctionGetter)
        {
            _abstractFunctionGetter = abstractFunctionGetter;
        }

        public Func<int, double?> GetFunc()
        {
            return _abstractFunctionGetter.GetFunc();
        }
    }
}
