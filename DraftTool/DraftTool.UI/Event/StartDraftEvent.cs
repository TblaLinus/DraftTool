﻿using DraftTool.UI.ViewModel.Interfaces;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
