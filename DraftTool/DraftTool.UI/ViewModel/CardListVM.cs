using DraftTool.Models;
using DraftTool.UI.Event;
using DraftTool.UI.Startup;
using DraftTool.UI.ViewModel.Interfaces;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DraftTool.UI.ViewModel
{
    public class CardListVM : ViewModelBase, ICardListVM
    {
        private IEventAggregator _eventAggregator;
        private IRepo _repo;

        public ObservableCollection<Card> Cards { get; set; }
        public ICommand AddCardCommand { get; }
        public ICommand RemoveCardCommand { get; }
        public ICommand AddAllCommand { get; }
        public ICommand RemoveAllCommand { get; }
        public ICommand ExitCommand { get; }

        public CardListVM(IEventAggregator eventAggregator, IRepo repo)
        {
            _eventAggregator = eventAggregator;
            _repo = repo;

            Cards = _repo.Cards;

            AddCardCommand = new DelegateCommand<string>(OnAddCard);
            RemoveCardCommand = new DelegateCommand<string>(OnRemoveCard);
            AddAllCommand = new DelegateCommand(OnAddAll);
            RemoveAllCommand = new DelegateCommand(OnRemoveAll);
            ExitCommand = new DelegateCommand(OnExit);
        }

        private void OnAddCard(string name)
        {
            Cards.Single(c => c.Name == name).NumberOfUses++;
            _eventAggregator.GetEvent<AddCardEvent>().Publish(new AddCardEventArgs { Name = name });
        }

        private void OnRemoveCard(string name)
        {
            Cards.Single(c => c.Name == name).NumberOfUses--;
            _eventAggregator.GetEvent<RemoveCardEvent>().Publish(new RemoveCardEventArgs { Name = name });
        }

        private void OnAddAll()
        {
            foreach (Card card in Cards)
            {
                card.NumberOfUses = card.MaxNumberOfUses;
            }
            _eventAggregator.GetEvent<AddAllEvent>().Publish();
        }

        private void OnRemoveAll()
        {
            foreach (Card card in Cards)
            {
                card.NumberOfUses = 0;
            }
            _eventAggregator.GetEvent<RemoveAllEvent>().Publish();
        }

        private void OnExit()
        {
            _eventAggregator.GetEvent<BackToMainEvent>().Publish();
        }
    }
}
