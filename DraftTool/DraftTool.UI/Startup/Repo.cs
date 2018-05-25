using DraftTool.Models;
using DraftTool.UI.Event;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.UI.Startup
{
    class Repo : IRepo
    {
        private IEventAggregator _eventAggregator;
        private List<Card> _cardsWithNumbers;

        public ObservableCollection<Card> Cards { get; }
        public ObservableCollection<Set> Sets { get; }

        public Repo(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _cardsWithNumbers = new List<Card>();

            _eventAggregator.GetEvent<AddCardEvent>().Subscribe(OnAddCard);
            _eventAggregator.GetEvent<RemoveCardEvent>().Subscribe(OnRemoveCard);
            _eventAggregator.GetEvent<AddAllEvent>().Subscribe(OnAddAll);
            _eventAggregator.GetEvent<RemoveAllEvent>().Subscribe(OnRemoveAll);

            Cards = new ObservableCollection<Card>();
            Sets = new ObservableCollection<Set>();

            for(int i=1; i<=53; i++)
            {
                Card card = new Card
                {
                    Name = $"RunnerCard C{i}",
                    CardSide = "Runner",
                    CardSet = "Core",
                    ImageURL = "https://netrunnerdb.com/card_image/01" + (i).ToString("D3") + ".png",
                    NumberOfUses = 3,
                    MaxNumberOfUses = 3
                };
                Cards.Add(card);
            }
            for (int i = 1; i <= 60; i++)
            {
                Card card = new Card
                {
                    Name = $"CorpCard C{i}",
                    CardSide = "Corp",
                    CardSet = "Core",
                    ImageURL = "https://netrunnerdb.com/card_image/01" + (53 + i).ToString("D3") + ".png",
                    NumberOfUses = 3,
                    MaxNumberOfUses = 3
                };
                Cards.Add(card);
            }

            for (int i = 1; i <= 27; i++)
            {
                Card card = new Card
                {
                    Name = $"CorpCard G{i}",
                    CardSide = "Corp",
                    CardSet = "Creation & Control",
                    ImageURL = "https://netrunnerdb.com/card_image/03" + (i).ToString("D3") + ".png",
                    NumberOfUses = 3,
                    MaxNumberOfUses = 3
                };
                Cards.Add(card);
            }
            for (int i = 1; i <= 28; i++)
            {
                Card card = new Card
                {
                    Name = $"RunnerCard G{i}",
                    CardSide = "Runner",
                    CardSet = "Creation & Control",
                    ImageURL = "https://netrunnerdb.com/card_image/03" + (27 + i).ToString("D3") + ".png",
                    NumberOfUses = 3,
                    MaxNumberOfUses = 3
                };
                Cards.Add(card);
            }
            foreach (Card card in Cards)
            {
                for (int j = 0; j < card.MaxNumberOfUses; j++)
                {
                    _cardsWithNumbers.Add(card);
                }
            }

            Set core = new Set { Name = "Core", IsUsed = false };
            Sets.Add(core);
            Set cac = new Set { Name = "Creation & Control", IsUsed = false };
            Sets.Add(cac);
        }

        private void OnAddCard(AddCardEventArgs args)
        {
            _cardsWithNumbers.Add(Cards.Single(c => c.Name == args.Name));
        }

        private void OnRemoveCard(RemoveCardEventArgs args)
        {
            _cardsWithNumbers.Remove(_cardsWithNumbers.Where(c => c.Name == args.Name).Last());
        }

        private void OnAddAll()
        {
            _cardsWithNumbers.Clear();
            foreach (Card card in Cards)
            {
                for (int i = 0; i < card.NumberOfUses; i++)
                {
                    _cardsWithNumbers.Add(card);
                }
            }
        }

        private void OnRemoveAll()
        {
            _cardsWithNumbers.Clear();
        }

        public List<Card> GetUsedCards (string side, IEnumerable<string> sets)
        {
            List<Card>returnDeck = Cards.Where(c => c.CardSide == side && sets.Contains(c.CardSet)).ToList();
            return returnDeck;
        }
    }
}
