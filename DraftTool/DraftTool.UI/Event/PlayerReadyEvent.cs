using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.UI.Event
{
    class PlayerReadyEvent : PubSubEvent<PlayerReadyEventArgs>
    {
    }

    public class PlayerReadyEventArgs
    {
        public int Player { get; set; }
        public bool Results { get; set; }
    }
}
