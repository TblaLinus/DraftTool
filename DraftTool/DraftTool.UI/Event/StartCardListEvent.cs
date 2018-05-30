using DraftTool.UI.Wrapper;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
