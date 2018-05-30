using Prism.Events;

namespace DraftTool.UI.Event
{
    public class ShowReadyPageEvent : PubSubEvent<ShowReadyPageEventArgs>
    {
    }

    public class ShowReadyPageEventArgs
    {
        public int Player { get; set; }
        public bool Results { get; set; }
    }
}
