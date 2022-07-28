using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserableCollection.FunctionInInterface
{
    internal class UseOfUsableClass
    {
        public UseOfUsableClass()
        {
            var useableClass1 = new UsableClass(new FirstImplementation());
            var useableClass2 = new UsableClass(new FirstImplementation());

            int input = 3;
            var func1 = useableClass1.GetFunc();

            var result = func1.Invoke(input);

            var funct2 = useableClass2.GetFunc();

            var result2 = funct2.Invoke(input);
        }
    }
}
