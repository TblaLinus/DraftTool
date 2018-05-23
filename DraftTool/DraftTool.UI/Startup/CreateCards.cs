using DraftTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.UI.Startup
{
    class CreateCards
    {
        public static List<Card> Create()
        {
            List<Card> cardList = new List<Card>();

            for(int i=1; i<=200; i++)
            {
                Card card = new Card { Name = $"TestCard {i}", ImageURL = $"URL {i}", MaxNumberOfUses = 3 };
                cardList.Add(card);
            }

            return cardList;
        }
    }
}
