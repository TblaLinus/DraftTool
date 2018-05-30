using DraftTool.UI.Wrapper;
using System.Collections.ObjectModel;

namespace DraftTool.UI.Service
{
    public interface ISetService
    {
        ObservableCollection<SetWrapper> Sets { get; }
    }
}
