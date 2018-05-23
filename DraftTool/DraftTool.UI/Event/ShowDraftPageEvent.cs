using DraftTool.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.UI.Event
{
    public class ShowDraftPageEvent :PubSubEvent<ShowDraftPageEventArgs>
    {
    }

    public class ShowDraftPageEventArgs
    {
        public int Player { get; set; }
        public bool Clear { get; set; }
        public ObservableCollection<Card> AvailableDeck { get; set; }
    }
}
