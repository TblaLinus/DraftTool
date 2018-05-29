using DraftTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.DataAccess
{
    public interface ICardRepo
    {
        List<Card> GetCards();
        void AddCard(Card card);
        void DeleteCard(string name);
        
    }
}
