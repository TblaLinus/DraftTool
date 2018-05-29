using DraftTool.UI.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.UI.Service
{
    public interface ICardService
    {
        List<CardWrapper> GetCardsWithNumbers(string side, IEnumerable<string> sets);
        List<CardWrapper> GetUsedCards(string side, IEnumerable<string> sets);
    }
}
