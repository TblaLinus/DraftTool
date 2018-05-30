using DraftTool.UI.Wrapper;
using Prism.Events;
using System.Collections.Generic;

namespace DraftTool.UI.Event
{
    public class StartDraftEvent : PubSubEvent<StartDraftEventArgs>
    {
    }

    public class StartDraftEventArgs
    {
        public int NumberOfRounds { get; set; }
        public int NumberOfPlayers { get; set; }
        public int NumberOfCards { get; set; }
        public List<CardWrapper> CardList { get; set; }
    }
}
