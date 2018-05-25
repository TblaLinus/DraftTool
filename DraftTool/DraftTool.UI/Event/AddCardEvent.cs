using DraftTool.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
