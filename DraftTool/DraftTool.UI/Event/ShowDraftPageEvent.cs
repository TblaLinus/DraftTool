using DraftTool.UI.Wrapper;
using Prism.Events;
using System.Collections.ObjectModel;

namespace DraftTool.UI.Event
{
    public class ShowDraftPageEvent :PubSubEvent<ShowDraftPageEventArgs>
    {
    }

    public class ShowDraftPageEventArgs
    {
        public int Player { get; set; }
        public bool Clear { get; set; }
        public ObservableCollection<CardWrapper> AvailableDeck { get; set; }
    }
}
