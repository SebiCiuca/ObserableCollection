using System;

namespace ObserableCollection.FunctionInInterface
{
    public class SecondImplementation : IInterface
    {
        public Func<int, double?> GetFunc()
        {
            return (x) => x + 20 -5 ;
        }
    }
}
