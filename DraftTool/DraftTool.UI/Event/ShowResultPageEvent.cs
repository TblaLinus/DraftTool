using DraftTool.UI.Wrapper;
using Prism.Events;
using System.Collections.ObjectModel;

namespace DraftTool.UI.Event
{
    //Event som körs för att ge rätt parametrar varje gång en Result sida ska visas
    public class ShowResultPageEvent : PubSubEvent<ShowResultPageEventArgs>
    {
    }

    public class ShowResultPageEventArgs
    {
        public int Player { get; set; }
        public ObservableCollection<CardWrapper> ResultDeck { get; set; }
    }
}
