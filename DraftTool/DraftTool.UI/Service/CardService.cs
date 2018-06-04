using DraftTool.DataAccess;
using DraftTool.Models;
using DraftTool.UI.Event;
using DraftTool.UI.Wrapper;
using Prism.Events;
using System.Collections.Generic;
using System.Linq;

namespace DraftTool.UI.Service
{
    //Sköter kommunikation mellan CardRepo och resten av programmet samt håller olika listor med kort
    public class CardService : DBServiceBase, ICardService
    {
        private ICardRepo _repo;
        private List<CardWrapper> _cardsWithNumbers;

        //Alla tillgängliga kort i databasen
        public List<CardWrapper> Cards { get; }

        public CardService(IEventAggregator eventAggregator, ICardRepo repo) : base(eventAggregator)
        {
            _repo = repo;
            _cardsWithNumbers = new List<CardWrapper>();

            _eventAggregator.GetEvent<AddCardEvent>().Subscribe(OnAddCard);
            _eventAggregator.GetEvent<RemoveCardEvent>().Subscribe(OnRemoveCard);

            Cards = new List<CardWrapper>();
            foreach (Card DBcard in _repo.GetCards())
            {
                CardWrapper card = new CardWrapper(DBcard);
                card.NumberOfUses = card.MaxNumberOfUses;
                Cards.Add(card);
                for (int j = 0; j < card.MaxNumberOfUses; j++)
                {
                    _cardsWithNumbers.Add(card);
                }
            }
        }

        private void OnAddCard(AddCardEventArgs args)
        {
            _cardsWithNumbers.Add(Cards.Single(c => c.Name == args.Name));
        }

        private void OnRemoveCard(RemoveCardEventArgs args)
        {
            _cardsWithNumbers.Remove(_cardsWithNumbers.Where(c => c.Name == args.Name).Last());
        }

        public void AddCardToDB(CardWrapper card)
        {
            _repo.AddCard(card.Model);
        }

        public void DeleteCardFromDB(CardWrapper card)
        {
            _repo.DeleteCard(card.Name);
        }

        //Ger en lista med kort från aktuell sida och set där varje kort finns med så många gånger det ska användas
        public List<CardWrapper> GetCardsWithNumbers(string side, IEnumerable<string> sets)
        {
            List<CardWrapper> returnDeck = _cardsWithNumbers.Where(c => c.Side == side && sets.Contains(c.Set)).ToList();
            return returnDeck;
        }

        //Ger en lista med alla kort från aktuell sida och set
        public List<CardWrapper> GetUsedCards(string side, IEnumerable<string> sets)
        {
            List<CardWrapper> returnDeck = Cards.Where(c => c.Side == side && sets.Contains(c.Set)).ToList();
            return returnDeck;
        }

        //Ger en lista med motsvarande kort från en lista med kortnamn
        public List<CardWrapper> GetCardsByNames (IEnumerable<string> cardNames)
        {
            List<CardWrapper> returnDeck = new List<CardWrapper>();
            foreach (string name in cardNames)
            {
                CardWrapper card = Cards.Single(c => c.Name == name);
                returnDeck.Add(card);
            }
            return returnDeck;
        }
    }
}
