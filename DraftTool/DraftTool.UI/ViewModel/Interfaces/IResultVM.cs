using DraftTool.UI.Wrapper;
using System.Collections.ObjectModel;

namespace DraftTool.UI.ViewModel.Interfaces
{
    public interface IResultVM
    {
        int Player { get; set; }
        ObservableCollection<CardWrapper> ResultDeck { get; set; }
    }
}
