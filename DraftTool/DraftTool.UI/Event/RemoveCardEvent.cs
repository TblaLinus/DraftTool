using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
