using DraftTool.UI.Wrapper;
using System.Collections.ObjectModel;

namespace DraftTool.UI.ViewModel.Interfaces
{
    public interface IDraftVM
    {
        ObservableCollection<CardWrapper> AvailableCards { get; set; }
        ObservableCollection<CardWrapper> ChosenCards { get; set; }
    }
}
