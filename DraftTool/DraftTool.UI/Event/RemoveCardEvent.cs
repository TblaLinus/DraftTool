using Prism.Events;

namespace DraftTool.UI.Event
{
    public class RemoveCardEvent : PubSubEvent<RemoveCardEventArgs>
    {
    }

    public class RemoveCardEventArgs
    {
        public string Name { get; set; }
    }
}
