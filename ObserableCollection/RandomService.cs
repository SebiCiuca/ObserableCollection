using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace ObserableCollection
{
    public class RandomService : ReactiveObject, IRandomService
    {
        private List<RandomModel> _privateList; 
        public RandomService()
        {
            _privateList = new List<RandomModel>
            {
                new RandomModel { Id = 1, Name = "FirstRandom" },
                new RandomModel { Id = 2, Name = "SecondRandom" },
                new RandomModel { Id = 3, Name = "SecondRandom" },
                new RandomModel { Id = 4, Name = "SecondRandom" }
            };
            _modelList = new();
        }
        private ObservableCollection<RandomModel> _modelList;
        public ObservableCollection<RandomModel> ModelList
        {
            get => _modelList;
            set => this.RaiseAndSetIfChanged(ref _modelList, value);
        }

        public void UpdateList(int take)
        {
            _modelList.Clear();
            Debug.WriteLine($"ModelList count {_modelList.Count}");

            var addToUI = _privateList.Take(take).ToList();
            addToUI.Shuffle();

            _privateList.ForEach(p => ModelList.Add(p));
            Debug.WriteLine($"ModelList count {_modelList.Count}");
        }
    }
}
