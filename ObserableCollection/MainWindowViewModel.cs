using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;

namespace ObserableCollection
{
    public class MainWindowViewModel : ReactiveObject
    {
        private readonly IRandomService m_RandomService;
        public ReactiveCommand<Unit, Unit> RandonListCommand { get; set; }

        public MainWindowViewModel(IRandomService randomService)
        {
            m_RandomService = randomService;

            RandonListCommand = ReactiveCommand.Create(() => { CallRandomService(); });

            randomService.WhenAnyValue(rs => rs.ModelList).WhereNotNull().Subscribe(_ => TriggerUpdateUI());
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
            foreach (var model in m_RandomService.ModelList)
            {
                Debug.WriteLine($"{model.Id} {model.Name}");
            }
        }
    }
}
