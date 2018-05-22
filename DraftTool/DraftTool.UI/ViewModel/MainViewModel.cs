using Autofac.Features.Indexed;
using Autofac.Features.OwnedInstances;
using DraftTool.Models;
using DraftTool.UI.ViewModel.Interfaces;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace DraftTool.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;
        private Func<List<Card>, IDraftVM> _draftVMCreator;
        private IDraftMenuVM _draftMenuVM;
        private ICardListVM _cardListVM;
        private IPageVM _activePage;

        public ICommand NewDraftCommand { get; }
        public ICommand GoToCardListCommand { get; }
        public ICommand ExitApplicationCommand { get; }

        public List<Card> CardList { get; set; }

        public MainViewModel(IEventAggregator eventAggregator, IDraftMenuVM draftMenuVM, ICardListVM cardListVM, Func<List<Card>, IDraftVM> draftVMCreator)
        {
            _eventAggregator = eventAggregator;
            _draftVMCreator = draftVMCreator;
            _draftMenuVM = draftMenuVM;
            _cardListVM = cardListVM;

            NewDraftCommand = new DelegateCommand(OnNewDraft);
            GoToCardListCommand = new DelegateCommand(OnGoToCardList);
            ExitApplicationCommand = new DelegateCommand(OnExitApplication);

            _eventAggregator.GetEvent<Event.StartDraftEvent>().Subscribe(OnStartDraft);

            CardList = Startup.CreateCards.Create();
        }

        private void OnNewDraft()
        {
            ActivePage = (IPageVM)_draftMenuVM;
        }

        private void OnGoToCardList()
        {
            ActivePage = (IPageVM)_cardListVM;
        }

        private void OnExitApplication()
        {
            Application.Current.Shutdown();
        }

        private void OnStartDraft()
        {
            IDraftVM draftVM = _draftVMCreator(CardList);
            ActivePage = (IPageVM)draftVM;
        }

        public IPageVM ActivePage
        {
            get
            {
                return _activePage;
            }

            set
            {
                _activePage = value;
                OnPropertyChanged();
            }
        }
    }
}
