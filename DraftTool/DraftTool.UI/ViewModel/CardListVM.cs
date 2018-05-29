using DraftTool.Models;
using DraftTool.UI.Event;
using DraftTool.UI.Service;
using DraftTool.UI.Startup;
using DraftTool.UI.ViewModel.Interfaces;
using DraftTool.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DraftTool.UI.ViewModel
{
    public class CardListVM : ViewModelBase, ICardListVM
    {
        private IEventAggregator _eventAggregator;
        private IDBService _DBService;
        private Func<CardWrapper, ICardVM> _cardVMCreator;

        public ObservableCollection<ICardVM> CardVMList { get; set; }
        public ICommand AddCardCommand { get; }
        public ICommand RemoveCardCommand { get; }
        public ICommand AddAllCommand { get; }
        public ICommand RemoveAllCommand { get; }
        public ICommand ExitCommand { get; }

        public CardListVM(IEventAggregator eventAggregator, IDBService DBService, Func<CardWrapper, ICardVM> cardVMCreator)
        {
            _eventAggregator = eventAggregator;
            _DBService = DBService;
            _cardVMCreator = cardVMCreator;

            _eventAggregator.GetEvent<StartCardListEvent>().Subscribe(OnStartCardList);

            AddAllCommand = new DelegateCommand(OnAddAll);
            RemoveAllCommand = new DelegateCommand(OnRemoveAll);
            ExitCommand = new DelegateCommand(OnExit);
        }

        private void OnStartCardList(StartCardListEventArgs args)
        {
            CardVMList = new ObservableCollection<ICardVM>();
            foreach (CardWrapper card in args.CardList)
            {
                ICardVM cardVM = _cardVMCreator(card);
                CardVMList.Add(cardVM);
            }
        }

        private void OnAddAll()
        {
            foreach(ICardVM card in CardVMList)
            {
                while (card.Card.NumberOfUses < card.Card.MaxNumberOfUses)
                {
                    card.OnAddCard();
                }
            }
        }

        private void OnRemoveAll()
        {
            foreach (ICardVM card in CardVMList)
            {
                while (card.Card.NumberOfUses > 0)
                {
                    card.OnRemoveCard();
                }
            }
        }

        private void OnExit()
        {
            _eventAggregator.GetEvent<BackToMainEvent>().Publish();
        }
    }
}
