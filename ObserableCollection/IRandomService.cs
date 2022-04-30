using System.Collections.ObjectModel;

namespace ObserableCollection
{
    public interface IRandomService
    {
        ObservableCollection<RandomModel> ModelList { get; }

        void UpdateList(int take);
    }
}
