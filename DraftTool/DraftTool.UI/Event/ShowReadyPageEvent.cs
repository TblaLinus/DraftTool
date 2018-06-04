using Prism.Events;

namespace DraftTool.UI.Event
{
    //Event som körs för att ge rätt parametrar varje gång en PlayerReady sida ska visas
    public class ShowReadyPageEvent : PubSubEvent<ShowReadyPageEventArgs>
    {
    }

    public class ShowReadyPageEventArgs
    {
        public int Player { get; set; }
        public bool Results { get; set; }
    }
}
