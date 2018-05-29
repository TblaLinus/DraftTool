using Autofac.Features.Indexed;
using Autofac.Features.OwnedInstances;
using DraftTool.Models;
using DraftTool.UI.Event;
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
        private Func<IGameVM> _gameVMCreator;
        private ICardMenuVM _cardMenuVM;
        private ICardListVM _cardListVM;
        private IViewModelBase _activePage;

        public ICommand NewDraftCommand { get; }
        public ICommand GoToCardListCommand { get; }
        public ICommand ExitApplicationCommand { get; }

        public MainViewModel(IEventAggregator eventAggregator, ICardMenuVM cardMenuVM, ICardListVM cardListVM, Func<IGameVM> gameVMCreator)
        {
            _eventAggregator = eventAggregator;
            _cardMenuVM = cardMenuVM;
            _cardListVM = cardListVM;
            _gameVMCreator = gameVMCreator;

            _eventAggregator.GetEvent<BackToMainEvent>().Subscribe(OnFinishedDraft);
            _eventAggregator.GetEvent<StartCardListEvent>().Subscribe(OnStartCardList);

            NewDraftCommand = new DelegateCommand(OnNewDraft);
            GoToCardListCommand = new DelegateCommand(OnGoToCardList);
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
            ActivePage = (IViewModelBase)_cardMenuVM;
        }

        private void OnStartCardList(StartCardListEventArgs args)
        {
            ActivePage = (IViewModelBase)_cardListVM;
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
