using DraftTool.UI.Wrapper;
using Prism.Events;
using System.Collections.Generic;

namespace DraftTool.UI.Event
{
    public class StartCardListEvent : PubSubEvent<StartCardListEventArgs>
    {
    }

    public class StartCardListEventArgs
    {
        public string Side { get; set; }
        public List<SetWrapper> Sets { get; set; }
    }
}
