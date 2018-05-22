using DraftTool.Models;
using DraftTool.UI.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.UI.ViewModel
{
    public class DraftVM : PageVM, IDraftVM
    {
        private int _numberOfRounds { get; }
        private int _numberOfCards { get; }
        private List<Card> _availableCards { get; set; }
        private List<Card>[] _startDecks { get;}
        private List<Card> _roundDeck;
        private Random _rnd;


        public DraftVM(List<Card> availableCards)
        {
            _numberOfRounds = 4;
            _numberOfCards = 10;
            _availableCards = availableCards;
            _rnd = new Random();
            _startDecks = GetStartDecks();
            _roundDeck = _startDecks[0];
        }

        public List<Card>[] GetStartDecks()
        {
            List<Card> cardPool = new List<Card>(_availableCards);
            List<Card>[] decks = new List<Card>[_numberOfRounds];

            for (int i = 0; i < _numberOfRounds; i++)
            {
                decks[i] = new List<Card>();
                for (int j = 0; j < _numberOfCards; j++)
                {
                    Card card = cardPool[_rnd.Next(0, cardPool.Count)];
                    decks[i].Add(card);
                    cardPool.Remove(card);
                }
            }

            return decks;
        }

        public List<Card> RoundDeck
        {
            get { return _roundDeck; }
            set
            {
                _roundDeck = value;
                OnPropertyChanged();
            }
        }
    }
}
