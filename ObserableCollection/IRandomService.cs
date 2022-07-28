using DynamicData;
using System;

namespace ObserableCollection
{
    public interface IRandomService
    {
        void UpdateList(int take);

        IObservable<IChangeSet<RandomModel>> ConnectList();
    }
}
