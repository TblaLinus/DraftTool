using DraftTool.Models;
using DraftTool.UI.Wrapper;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.UI.Event
{
    public class ShowResultPageEvent : PubSubEvent<ShowResultPageEventArgs>
    {
    }

    public class ShowResultPageEventArgs
    {
        public int Player { get; set; }
        public ObservableCollection<CardWrapper> ResultDeck { get; set; }
    }
}
