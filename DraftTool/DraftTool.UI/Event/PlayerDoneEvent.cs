using DraftTool.UI.Wrapper;
using Prism.Events;
using System.Collections.ObjectModel;

namespace DraftTool.UI.Event
{
    //Event som körs för att gå vidare varje gång man är färdig med sitt val under en draft eller färdig med sin resultatsida
    class PlayerDoneEvent : PubSubEvent<PlayerDoneEventArgs>
    {
    }

    public class PlayerDoneEventArgs
    {
        public ObservableCollection<CardWrapper> ResultDeck { get; set; }
    }
}
