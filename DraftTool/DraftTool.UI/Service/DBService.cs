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
        private List<CardWrapper> _cardsWithNumbers;

        public ObservableCollection<CardWrapper> Cards { get; }
        public ObservableCollection<SetWrapper> Sets { get; }

        public DBService(IEventAggregator eventAggregator, IRepo repo)
        {
            _eventAggregator = eventAggregator;
            _repo = repo;
            _cardsWithNumbers = new List<CardWrapper>();

            _eventAggregator.GetEvent<AddCardEvent>().Subscribe(OnAddCard);
            _eventAggregator.GetEvent<RemoveCardEvent>().Subscribe(OnRemoveCard);
            _eventAggregator.GetEvent<AddAllEvent>().Subscribe(OnAddAll);
            _eventAggregator.GetEvent<RemoveAllEvent>().Subscribe(OnRemoveAll);

            Cards = new ObservableCollection<CardWrapper>();
            Sets = new ObservableCollection<SetWrapper>();

            //foreach (CardWrapper card in Cards)
            //{
            //    for (int j = 0; j < card.MaxNumberOfUses; j++)
            //    {
            //        _cardsWithNumbers.Add(card);
            //    }
            //}

            Set core = new Set { Name = "Core" };
            SetWrapper coreWrapper = new SetWrapper(core) { IsUsed = false };
            Sets.Add(coreWrapper);
            Set cac = new Set { Name = "Creation & Control" };
            SetWrapper cacWrapper = new SetWrapper(cac) { IsUsed = false };
            Sets.Add(cacWrapper);
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
            foreach (CardWrapper card in Cards)
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

        private void AddCardToDB(Card card)
        {
            _repo.AddCard(card);
        }

        public List<CardWrapper> GetUsedCards(string side, IEnumerable<string> sets)
        {
            List<CardWrapper> returnDeck = _cardsWithNumbers.Where(c => c.Side == side && sets.Contains(c.Set)).ToList();
            return returnDeck;
        }
    }
}
