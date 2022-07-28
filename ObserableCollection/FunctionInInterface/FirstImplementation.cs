using System;

namespace ObserableCollection.FunctionInInterface
{
    public class FirstImplementation : IInterface
    {
        public Func<int, double?> GetFunc()
        {
            //write here something to return a function that will output a double

            return (n) => n * 20 + 5;
        }
    }
}
