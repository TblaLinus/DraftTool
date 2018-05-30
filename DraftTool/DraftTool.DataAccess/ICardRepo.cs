using DraftTool.Models;
using System.Collections.Generic;

namespace DraftTool.DataAccess
{
    public interface ICardRepo
    {
        List<Card> GetCards();
        void AddCard(Card card);
        void DeleteCard(string name);
    }
}
