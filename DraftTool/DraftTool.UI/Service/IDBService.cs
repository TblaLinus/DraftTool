using DraftTool.UI.Wrapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.UI.Service
{
    public interface IDBService
    {
        ObservableCollection<SetWrapper> Sets { get; }
        List<CardWrapper> GetCardsWithNumbers(string side, IEnumerable<string> sets);
        List<CardWrapper> GetUsedCards(string side, IEnumerable<string> sets);
    }
}
