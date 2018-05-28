using DraftTool.Models;
using DraftTool.UI.Event;
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
        private IRepo _repo;
        private Func<CardWrapper, ICardVM> _cardVMCreator;

        public ObservableCollection<ICardVM> CardVMList { get; set; }
        public ICommand AddCardCommand { get; }
        public ICommand RemoveCardCommand { get; }
        public ICommand AddAllCommand { get; }
        public ICommand RemoveAllCommand { get; }
        public ICommand ExitCommand { get; }

        public CardListVM(IEventAggregator eventAggregator, IRepo repo, Func<CardWrapper, ICardVM> cardVMCreator)
        {
            _eventAggregator = eventAggregator;
            _repo = repo;
            _cardVMCreator = cardVMCreator;

            CardVMList = new ObservableCollection<ICardVM>();
            foreach (CardWrapper card in _repo.Cards)
            {
                ICardVM cardVM = _cardVMCreator(card);
                CardVMList.Add(cardVM);
            }

            AddAllCommand = new DelegateCommand(OnAddAll);
            RemoveAllCommand = new DelegateCommand(OnRemoveAll);
            ExitCommand = new DelegateCommand(OnExit);
        }

        private void OnAddAll()
        {
            _eventAggregator.GetEvent<AddAllEvent>().Publish();
        }

        private void OnRemoveAll()
        {
            _eventAggregator.GetEvent<RemoveAllEvent>().Publish();
        }

        private void OnExit()
        {
            _eventAggregator.GetEvent<BackToMainEvent>().Publish();
        }
    }
}
