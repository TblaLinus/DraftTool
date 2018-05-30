using Prism.Events;

namespace DraftTool.UI.Event
{
    class AddCardEvent : PubSubEvent<AddCardEventArgs>
    {
    }

    class AddCardEventArgs
    {
        public string Name { get; set; }
    }
}
