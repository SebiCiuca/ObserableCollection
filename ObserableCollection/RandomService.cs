using DynamicData;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Disposables;

namespace ObserableCollection
{
    public class RandomService : ReactiveObject, IRandomService
    {
        private List<RandomModel> _privateList;
        private readonly SourceList<RandomModel> _items = new SourceList<RandomModel>();
        public RandomService()
        {
            _privateList = new List<RandomModel>
            {
                new RandomModel { Id = 1, Name = "FirstRandom" },
                new RandomModel { Id = 2, Name = "SecondRandom" },
                new RandomModel { Id = 3, Name = "SecondRandom" },
                new RandomModel { Id = 4, Name = "SecondRandom" }
            };
        }
        public void UpdateList(int take)
        {
            _items.Clear();
            Debug.WriteLine($"ModelList count {_items.Count}");

            var addToUI = _privateList.Take(take).ToList();

            addToUI.ForEach(p => _items.Add(p));
            Debug.WriteLine($"ModelList count {_items.Count}");
        }

        public IObservable<IChangeSet<RandomModel>> ConnectList()
        {
            return _items.Connect();
        }
    }
}
