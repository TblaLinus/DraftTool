using DraftTool.UI.Wrapper;
using Prism.Events;
using System.Collections.ObjectModel;

namespace DraftTool.UI.Event
{
    class PlayerDoneEvent : PubSubEvent<PlayerDoneEventArgs>
    {
    }

    public class PlayerDoneEventArgs
    {
        public ObservableCollection<CardWrapper> ResultDeck { get; set; }
    }
}
