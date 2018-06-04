using DraftTool.UI.Wrapper;
using Prism.Events;
using System.Collections.Generic;

namespace DraftTool.UI.Event
{
    //Event som körs då CardListMenu sidans val är gjorda och CardList sidan ska visas
    public class StartCardListEvent : PubSubEvent<StartCardListEventArgs>
    {
    }

    public class StartCardListEventArgs
    {
        public string Side { get; set; }
        public List<SetWrapper> Sets { get; set; }
    }
}
