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
    class PlayerDoneEvent : PubSubEvent<PlayerDoneEventArgs>
    {
    }

    public class PlayerDoneEventArgs
    {
        public ObservableCollection<CardWrapper> ResultDeck { get; set; }
    }
}
