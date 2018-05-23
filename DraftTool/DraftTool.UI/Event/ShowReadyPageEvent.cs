using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
