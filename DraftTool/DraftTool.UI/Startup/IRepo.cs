using DraftTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.UI.Startup
{
    public interface  IRepo
    {
        List<Card> GetDecks(string side, IEnumerable<string> set);
        List<Set> Sets { get; }
    }
}
