using DraftTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.DataAccess
{
    public interface IRepo
    {
        List<Card> GetCards();
        void AddCard(Card card);
        void DeleteCard(string name);
        List<Set> GetSets();
        void AddSet(Set set);
        void DeleteSet(string name);
    }
}
