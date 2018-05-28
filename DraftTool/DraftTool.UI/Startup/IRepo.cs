using DraftTool.Models;
using DraftTool.UI.Wrapper;
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
        ObservableCollection<CardWrapper> Cards { get; }
        ObservableCollection<SetWrapper> Sets { get; }
        List<CardWrapper> GetUsedCards(string side, IEnumerable<string> sets);
    }
}
