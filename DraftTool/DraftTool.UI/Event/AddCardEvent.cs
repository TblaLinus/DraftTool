using Prism.Events;

namespace DraftTool.UI.Event
{
    //Event som körs då man lägger till ett exemplar av ett kort på CardList sidan
    class AddCardEvent : PubSubEvent<AddCardEventArgs>
    {
    }

    class AddCardEventArgs
    {
        public string Name { get; set; }
    }
}
