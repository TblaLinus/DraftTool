using DraftTool.Models;
using DraftTool.UI.Event;
using DraftTool.UI.Service;
using DraftTool.UI.ViewModel.Interfaces;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.UI.ViewModel
{
    public class GameVM : ViewModelBase, IGameVM
    {
        private IEventAggregator _eventAggregator;
        private IGameEngine _gameEngine;
        private IDraftMenuVM _draftMenuVM;
        private IPlayerReadyVM _playerReadyVM;
        private Func<IDraftVM> _draftVMCreator;
        private IDraftVM[] _playerDraftVM;
        private IResultVM _resultVM;
        private IViewModelBase _activePage;

        public GameVM(IEventAggregator eventAggregator, IGameEngine gameEngine, IDraftMenuVM draftMenuVM, IPlayerReadyVM playerReadyVM, Func<IDraftVM> draftVMCreator, IResultVM resultVM)
        {
            _eventAggregator = eventAggregator;
            _gameEngine = gameEngine;
            _draftVMCreator = draftVMCreator;
            _draftMenuVM = draftMenuVM;
            _playerReadyVM = playerReadyVM;
            _resultVM = resultVM;

            ActivePage = (IViewModelBase)_draftMenuVM;

            _eventAggregator.GetEvent<StartDraftEvent>().Subscribe(OnStartDraft);
            _eventAggregator.GetEvent<ShowReadyPageEvent>().Subscribe(OnShowReadyPage);
            _eventAggregator.GetEvent<ShowDraftPageEvent>().Subscribe(OnShowDraftPage);
            _eventAggregator.GetEvent<ShowResultPageEvent>().Subscribe(OnShowResultPage);
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

        private void OnStartDraft(StartDraftEventArgs args)
        {
            _playerDraftVM = new IDraftVM[args.NumberOfPlayers];

            for (int i = 0; i < args.NumberOfPlayers; i++)
            {
                _playerDraftVM[i] = _draftVMCreator();
            }
        }

        private void OnShowReadyPage(ShowReadyPageEventArgs args)
        {
            _playerReadyVM.Results = args.Results;
            _playerReadyVM.Player = args.Player;
            ActivePage = (IViewModelBase)_playerReadyVM;
        }

        private void OnShowDraftPage(ShowDraftPageEventArgs args)
        {
            if (args.Clear)
            {
                foreach (IDraftVM draftVM in _playerDraftVM)
                {
                    draftVM.ChosenCards.Clear();
                }
            }
            _playerDraftVM[args.Player - 1].AvailableCards = args.AvailableDeck;
            ActivePage = (IViewModelBase)_playerDraftVM[args.Player -1];
        }

        private void OnShowResultPage(ShowResultPageEventArgs args)
        {
            _resultVM.Player = args.Player;
            _resultVM.ResultDeck = args.ResultDeck;
            ActivePage = (IViewModelBase)_resultVM;
        }
    }
}
