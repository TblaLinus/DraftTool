using DraftTool.Models;
using DraftTool.UI.Event;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.UI.Service
{
    public class GameEngine : IGameEngine
    {
        private int _activeRound;
        private int _activePlayer;
        private bool _results;
        private bool _clear;
        private IEventAggregator _eventAggregator;
        private ObservableCollection<Card>[][] _draftDecks;
        private ObservableCollection<Card>[] _resultDecks;
        private Random _rnd = new Random();

        public int _numberOfRounds { get; set; }
        public int _numberOfPlayers { get; set; }
        public int _numberOfCards { get; set; }
        public List<Card> CardList { get; set; }

        public GameEngine(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _activeRound = 1;
            _activePlayer = 1;
            _results = false;

            CardList = Startup.CreateCards.Create();

            _eventAggregator.GetEvent<StartDraftEvent>().Subscribe(OnStartDraft);
            _eventAggregator.GetEvent<PlayerReadyEvent>().Subscribe(OnPlayerReady);
            _eventAggregator.GetEvent<PlayerDoneEvent>().Subscribe(OnPlayerDone);
        }

        private void OnStartDraft(StartDraftEventArgs args)
        {
            _numberOfRounds = args.NumberOfRounds;
            _numberOfPlayers = args.NumberOfPlayers;
            _numberOfCards = args.NumberOfCards;
            _draftDecks = GetDraftDecks();
            _resultDecks = new ObservableCollection<Card>[_numberOfPlayers];
            for (int i = 0; i < _numberOfPlayers; i++)
            {
                _resultDecks[i] = new ObservableCollection<Card>();
            }

            _eventAggregator.GetEvent<ShowReadyPageEvent>().Publish(new ShowReadyPageEventArgs { Player = _activePlayer, Results = _results});
        }

        private void OnPlayerReady()
        {
            if (_results)
            {
                _eventAggregator.GetEvent<ShowResultPageEvent>().Publish(
                    new ShowResultPageEventArgs { Player = _activePlayer, ResultDeck = _resultDecks[_activePlayer - 1] });
            }
            else
            {
                _eventAggregator.GetEvent<ShowDraftPageEvent>().Publish(
                    new ShowDraftPageEventArgs { Player = _activePlayer, Clear = _clear, AvailableDeck = _draftDecks[_activeRound - 1][_activePlayer - 1] });
            }
        }

        private void OnPlayerDone(PlayerDoneEventArgs args)
        {
            if (_results)
            {
                if (_activePlayer == _numberOfPlayers)
                {
                    _eventAggregator.GetEvent<FinishedDraftEvent>().Publish();
                    return;
                }
            }
            else
            {
                if (args != null && _activePlayer == _numberOfPlayers && _activeRound == _numberOfRounds)
                {
                    AddResults(args.ResultDeck);
                    _results = true;
                }
                else if (args != null && _activePlayer == _numberOfPlayers)
                {
                    AddResults(args.ResultDeck);
                    _activeRound++;
                }
                else if (args != null)
                {
                    AddResults(args.ResultDeck);
                }
                _clear = (args != null && _activePlayer == _numberOfPlayers);
            }            
            SetActivePlayerAndRound();
            _eventAggregator.GetEvent<ShowReadyPageEvent>().Publish(
                new ShowReadyPageEventArgs { Player = _activePlayer, Results = _results });
        }

        private void AddResults(ObservableCollection<Card> cards)
        {
            foreach (Card card in cards)
            {
                _resultDecks[_activePlayer - 1].Add(card);
            }
        }

        private void SetActivePlayerAndRound()
        {
            if (_activePlayer == _numberOfPlayers)
            {
                _activePlayer = 1;
            }
            else
            {
                _activePlayer++;
            }
        }

        private ObservableCollection<Card>[][] GetDraftDecks()
        {
            ObservableCollection<Card>[][] decks = new ObservableCollection<Card>[_numberOfRounds][];

            for (int i = 0; i < _numberOfRounds; i++)
            {
                decks[i] = new ObservableCollection<Card>[_numberOfPlayers];
                for (int j = 0; j < _numberOfPlayers; j++)
                {
                    decks[i][j] = new ObservableCollection<Card>();
                    for (int k = 0; k < _numberOfCards; k++)
                    {
                        Card card = CardList[_rnd.Next(0, CardList.Count)];
                        decks[i][j].Add(card);
                        CardList.Remove(card);
                    }
                }
            }

            return decks;
        }
    }
}
