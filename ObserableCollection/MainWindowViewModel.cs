using DynamicData;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;

namespace ObserableCollection
{
    public class MainWindowViewModel : ReactiveObject
    {
        private readonly IRandomService m_RandomService;
        public ReactiveCommand<Unit, Unit> RandonListCommand { get; set; }

        private readonly ReadOnlyObservableCollection<RandomModel> _items;

        public MainWindowViewModel(IRandomService randomService)
        {
            m_RandomService = randomService;

            RandonListCommand = ReactiveCommand.Create(() => { CallRandomService(); });

            //randomService.ConnectList().Transform(cl => cl).ObserveOn(RxApp.MainThreadScheduler).Bind(out _items).Subscribe(_ => TriggerUpdateUI());
            randomService.ConnectList().ObserveOn(RxApp.MainThreadScheduler).Bind(out _items).Subscribe(_ => TriggerUpdateUI());
        }

        private void CallRandomService()
        {
            Debug.WriteLine("Random is called");
            var random = new Random();
            var take = random.Next(1, 4);
            Debug.WriteLine($"Take is {take}");
            m_RandomService.UpdateList(take);
        }

        private void TriggerUpdateUI()
        {
            Debug.WriteLine("List changed");
            foreach (var model in _items)
            {
                Debug.WriteLine($"{model.Id} {model.Name}");
            }
        }
    }
}
