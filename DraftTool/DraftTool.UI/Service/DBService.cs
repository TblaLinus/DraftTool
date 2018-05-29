using DraftTool.DataAccess;
using DraftTool.Models;
using DraftTool.UI.Event;
using DraftTool.UI.Wrapper;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.UI.Service
{
    public class DBService : IDBService
    {
        private IEventAggregator _eventAggregator;
        private IRepo _repo;
        private List<CardWrapper> _cards { get; }
        private List<CardWrapper> _cardsWithNumbers;

        public ObservableCollection<SetWrapper> Sets { get; }

        public DBService(IEventAggregator eventAggregator, IRepo repo)
        {
            _eventAggregator = eventAggregator;
            _repo = repo;
            _cardsWithNumbers = new List<CardWrapper>();

            _eventAggregator.GetEvent<AddCardEvent>().Subscribe(OnAddCard);
            _eventAggregator.GetEvent<RemoveCardEvent>().Subscribe(OnRemoveCard);

            _cards = new List<CardWrapper>();
            foreach (Card DBcard in _repo.GetCards())
            {
                CardWrapper card = new CardWrapper(DBcard);
                card.NumberOfUses = card.MaxNumberOfUses;
                _cards.Add(card);
                for (int j = 0; j < card.MaxNumberOfUses; j++)
                {
                    _cardsWithNumbers.Add(card);
                }
            }

            Sets = new ObservableCollection<SetWrapper>();
            foreach (Set DBset in _repo.GetSets())
            {
                SetWrapper set = new SetWrapper(DBset);
                set.IsUsed = false;
                Sets.Add(set);
            }
        }

        private void OnAddCard(AddCardEventArgs args)
        {
            _cardsWithNumbers.Add(_cards.Single(c => c.Name == args.Name));
        }

        private void OnRemoveCard(RemoveCardEventArgs args)
        {
            _cardsWithNumbers.Remove(_cardsWithNumbers.Where(c => c.Name == args.Name).Last());
        }

        private void AddCardToDB(Card card)
        {
            _repo.AddCard(card);
        }

        public List<CardWrapper> GetCardsWithNumbers(string side, IEnumerable<string> sets)
        {
            List<CardWrapper> returnDeck = _cardsWithNumbers.Where(c => c.Side == side && sets.Contains(c.Set)).ToList();
            return returnDeck;
        }

        public List<CardWrapper> GetUsedCards(string side, IEnumerable<string> sets)
        {
            List<CardWrapper> returnDeck = _cards.Where(c => c.Side == side && sets.Contains(c.Set)).ToList();
            return returnDeck;
        }
    }
}
