using DraftTool.UI.Event;
using DraftTool.UI.ViewModel.Interfaces;
using Prism.Commands;
using Prism.Events;
using System;
using System.Windows;
using System.Windows.Input;

namespace DraftTool.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;
        private Func<IGameVM> _gameVMCreator;
        private Func<ICardMenuVM> _cardMenuVMCreator;
        private ICardListVM _cardListVM;
        private IAddRemoveVM _addRemoveVM;
        private IViewModelBase _activePage;

        public ICommand NewDraftCommand { get; }
        public ICommand GoToCardListCommand { get; }
        public ICommand GoToAddRemoveCommand { get; }
        public ICommand ExitApplicationCommand { get; }

        public MainViewModel(IEventAggregator eventAggregator, Func<ICardMenuVM> cardMenuVMCreator, ICardListVM cardListVM, IAddRemoveVM addRemoveVM, Func<IGameVM> gameVMCreator)
        {
            _eventAggregator = eventAggregator;
            _cardMenuVMCreator = cardMenuVMCreator;
            _cardListVM = cardListVM;
            _addRemoveVM = addRemoveVM;
            _gameVMCreator = gameVMCreator;

            _eventAggregator.GetEvent<BackToMainEvent>().Subscribe(OnFinishedDraft);
            _eventAggregator.GetEvent<StartCardListEvent>().Subscribe(OnStartCardList);

            NewDraftCommand = new DelegateCommand(OnNewDraft);
            GoToCardListCommand = new DelegateCommand(OnGoToCardList);
            GoToAddRemoveCommand = new DelegateCommand(OnGoToAddRemove);
            ExitApplicationCommand = new DelegateCommand(OnExitApplication);
        }

        public IViewModelBase ActivePage
        {
            get { return _activePage; }
            set
            {
                _activePage = value;
                OnPropertyChanged();
            }
        }

        private void OnNewDraft()
        {
            ActivePage = (IViewModelBase)_gameVMCreator();
        }

        private void OnGoToCardList()
        {
            ActivePage = (IViewModelBase)_cardMenuVMCreator();
        }

        private void OnStartCardList(StartCardListEventArgs args)
        {
            ActivePage = (IViewModelBase)_cardListVM;
        }

        private void OnGoToAddRemove()
        {
            ActivePage = (IViewModelBase)_addRemoveVM;
        }

        private void OnFinishedDraft()
        {
            ActivePage = null;
        }

        private void OnExitApplication()
        {
            Application.Current.Shutdown();
        }
    }
}
