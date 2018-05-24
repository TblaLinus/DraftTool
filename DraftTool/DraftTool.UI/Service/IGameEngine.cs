using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.UI.Service
{
    public interface IGameEngine
    {
        int NumberOfRounds { get; set; }
        int NumberOfPlayers { get; set; }
        int NumberOfCards { get; set; }
    }
}
