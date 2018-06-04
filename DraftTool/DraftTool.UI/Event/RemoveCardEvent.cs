using Prism.Events;

namespace DraftTool.UI.Event
{
    //Event som körs då man tar bort ett exemplar av ett kort på CardList sidan
    public class RemoveCardEvent : PubSubEvent<RemoveCardEventArgs>
    {
    }

    public class RemoveCardEventArgs
    {
        public string Name { get; set; }
    }
}
