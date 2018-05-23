using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.UI.Service
{
    public interface IGameEngine
    {
        int _numberOfRounds { get; set; }
        int _numberOfPlayers { get; set; }
        int _numberOfCards { get; set; }
    }
}
