﻿using DraftTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.UI.Startup
{
    public interface  ICardRepo
    {
        List<Card> GetDecks(string side, string set);
    }
}
