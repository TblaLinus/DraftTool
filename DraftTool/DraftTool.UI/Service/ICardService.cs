using DraftTool.UI.Wrapper;
using System.Collections.Generic;

namespace DraftTool.UI.Service
{
    public interface ICardService
    {
        List<CardWrapper> GetCardsWithNumbers(string side, IEnumerable<string> sets);
        List<CardWrapper> GetUsedCards(string side, IEnumerable<string> sets);
        List<CardWrapper> GetCardsByNames(IEnumerable<string> cardNames);
    }
}
