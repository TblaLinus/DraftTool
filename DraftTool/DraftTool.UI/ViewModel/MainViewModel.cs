using DraftTool.Models;
using DraftTool.UI.ViewModel.Interfaces;
using Prism.Commands;
using Prism.Events;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace DraftTool.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;
        private IDraftMenuVM _draftMenuVM;
        private ICardListVM _cardListVM;
        private IPageVM _activePage;

        public ICommand NewDraftCommand { get; }
        public ICommand GoToCardListCommand { get; }
        public ICommand ExitApplicationCommand { get; }
        public List<Card> CardList { get; set; }

        public MainViewModel(IEventAggregator eventAggregator, IDraftMenuVM draftMenuVM, ICardListVM cardListVM)
        {
            _eventAggregator = eventAggregator;
            _draftMenuVM = draftMenuVM;
            _cardListVM = cardListVM;

            NewDraftCommand = new DelegateCommand(OnNewDraft);
            GoToCardListCommand = new DelegateCommand(OnGoToCardList);
            ExitApplicationCommand = new DelegateCommand(OnExitApplication);

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
