using DraftTool.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.UI.Startup
{
    public interface  IRepo
    {
        ObservableCollection<Card> Cards { get; }
        ObservableCollection<Set> Sets { get; }
        List<Card> GetUsedCards(string side, IEnumerable<string> sets);
    }
}
