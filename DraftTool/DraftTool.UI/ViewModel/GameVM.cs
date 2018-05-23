using DraftTool.Models;
using DraftTool.UI.Event;
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
        private const int _numberOfRounds = 4;
        private const int _numberOfPlayers = 2;
        private const int _numberOfCards = 10;
        private int _activeRound;
        private int _activePlayer;
        private IEventAggregator _eventAggregator;
        private IDraftMenuVM _draftMenuVM;
        private IPlayerReadyVM _playerReadyVM;
        private Func<IDraftVM> _draftVMCreator;
        private IDraftVM[] _playerDraftVM;
        private IResultVM _resultVM;
        private IViewModelBase _activePage;
        private ObservableCollection<Card>[,] _draftDecks;
        private ObservableCollection<Card>[] _resultDecks;
        private Random _rnd = new Random();

        public List<Card> CardList { get; set; }

        public GameVM(IEventAggregator eventAggregator, IDraftMenuVM draftMenuVM, IPlayerReadyVM playerReadyVM, Func<IDraftVM> draftVMCreator, IResultVM resultVM)
        {
            _activePlayer = 1;
            _eventAggregator = eventAggregator;
            _draftVMCreator = draftVMCreator;
            _draftMenuVM = draftMenuVM;
            _playerReadyVM = playerReadyVM;
            _playerDraftVM = new IDraftVM[_numberOfPlayers];
            _resultDecks = new ObservableCollection<Card>[_numberOfPlayers];

            CardList = Startup.CreateCards.Create();
            ActivePage = (IViewModelBase)_draftMenuVM;

            _eventAggregator.GetEvent<StartDraftEvent>().Subscribe(OnStartDraft);
            _eventAggregator.GetEvent<PlayerReadyEvent>().Subscribe(OnPlayerReady);
            _eventAggregator.GetEvent<PlayerDoneEvent>().Subscribe(OnPlayerDone);
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

        private void OnStartDraft()
        {
            _draftDecks = GetDraftDecks();
            for (int i = 0; i < _playerDraftVM.Length; i++)
            {
                _playerDraftVM[i] = _draftVMCreator();
                _playerDraftVM[i].AvailableCards = _draftDecks[0, i];
            }
            ActivePage = (IViewModelBase)_playerReadyVM;
        }

        private void OnPlayerReady(PlayerReadyEventArgs args)
        {
            if (args.Results)
            {
                _resultVM.Player = args.Player;
                ActivePage = (IViewModelBase)_resultVM;
            }
            else
            {
                ActivePage = (IViewModelBase)_playerDraftVM[args.Player - 1];
            }
        }

        private void OnPlayerDone(PlayerDoneEventArgs args)
        {
            if (args.ResultDeck != null)
            {
                _resultDecks[_activePlayer - 1] = args.ResultDeck;
                if (_activePlayer == _numberOfPlayers)
                {
                    _playerReadyVM.DraftFinished = true;
                }
            }
            if (_activePlayer == _numberOfPlayers)
            {
                _activePlayer = 1;
            }
            else
            {
                _activePlayer++;
            }
            _playerReadyVM.Player = _activePlayer;
            ActivePage = (IViewModelBase)_playerReadyVM;
        }

        private ObservableCollection<Card>[,] GetDraftDecks()
        {
            ObservableCollection<Card>[,] decks = new ObservableCollection<Card>[_numberOfRounds, _numberOfPlayers];

            for (int i = 0; i < _numberOfRounds; i++)
            {
                for (int j = 0; j < _numberOfPlayers; j++)
                {
                    decks[i, j] = new ObservableCollection<Card>();
                    for (int k = 0; k < _numberOfCards; k++)
                    {
                        Card card = CardList[_rnd.Next(0, CardList.Count)];
                        decks[i, j].Add(card);
                        CardList.Remove(card);
                    }
                }
            }

            return decks;
        }

    }
}
