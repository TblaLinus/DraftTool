using DraftTool.UI.Event;
using DraftTool.UI.Wrapper;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DraftTool.UI.Service
{
    //Sköter all logik under en draft
    public class GameEngine : IGameEngine
    {
        private int _activeRound;
        private int _activePlayer;
        private int _activeCard;
        private bool _results;
        private bool _clear;
        private IEventAggregator _eventAggregator;
        private ObservableCollection<CardWrapper>[][] _draftDecks;
        //Alla valda kort för de olika spelarna
        private ObservableCollection<CardWrapper>[] _resultDecks;
        private Random _rnd = new Random();

        public int NumberOfRounds { get; set; }
        public int NumberOfPlayers { get; set; }
        public int NumberOfCards { get; set; }
        public List<CardWrapper> CardList { get; set; }

        public GameEngine(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _activeRound = 1;
            _activePlayer = 1;
            _activeCard = 1;
            _results = false;

            _eventAggregator.GetEvent<StartDraftEvent>().Subscribe(OnStartDraft);
            _eventAggregator.GetEvent<PlayerReadyEvent>().Subscribe(OnPlayerReady);
            _eventAggregator.GetEvent<PlayerDoneEvent>().Subscribe(OnPlayerDone);
        }

        //Körs då draften startar, sätter alla parametrar och visar första PlayerReady sidan
        private void OnStartDraft(StartDraftEventArgs args)
        {
            NumberOfRounds = args.NumberOfRounds;
            NumberOfPlayers = args.NumberOfPlayers;
            NumberOfCards = args.NumberOfCards;
            CardList = args.CardList;
            _draftDecks = GetDraftDecks();
            _resultDecks = new ObservableCollection<CardWrapper>[NumberOfPlayers];
            for (int i = 0; i < NumberOfPlayers; i++)
            {
                _resultDecks[i] = new ObservableCollection<CardWrapper>();
            }

            _eventAggregator.GetEvent<ShowReadyPageEvent>().Publish(new ShowReadyPageEventArgs { Player = _activePlayer, Results = _results});
        }

        //Bestämmer vad som ska visas efter varje PlayerReady sida
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
                    new ShowDraftPageEventArgs { Player = _activePlayer, Clear = _clear, AvailableDeck = _draftDecks[_activeRound - 1][(_activePlayer + _activeCard - 2) % NumberOfPlayers] });
            }
        }

        //Bestämmer vad som ska visas efter varje Draft sida och Ready sida
        private void OnPlayerDone(PlayerDoneEventArgs args)
        {
            //Kollar om draften är klar
            if (_results)
            {
                if (_activePlayer == NumberOfPlayers)
                {
                    _eventAggregator.GetEvent<BackToMainEvent>().Publish();
                    return;
                }
            }
            else
            {
                //Kollar om Ready sidor ska visas
                if (args != null && _activePlayer == NumberOfPlayers && _activeRound == NumberOfRounds)
                {
                    AddResults(args.ResultDeck);
                    _results = true;
                }
                //Kollar om aktuell runda är klar
                else if (args != null && _activePlayer == NumberOfPlayers)
                {
                    AddResults(args.ResultDeck);
                    _activeCard = 0;
                    _activeRound++;
                }
                //Kollar om alla kort är valda och ska läggas till i spelarens result deck
                else if (args != null)
                {
                    AddResults(args.ResultDeck);
                }
                _clear = (args != null && _activePlayer == NumberOfPlayers);
            }            
            SetActive();
            //Visar nästa PlayerReady sida
            _eventAggregator.GetEvent<ShowReadyPageEvent>().Publish(
                new ShowReadyPageEventArgs { Player = _activePlayer, Results = _results });
        }

        //Lägger till kort i aktiv spelares result deck
        private void AddResults(ObservableCollection<CardWrapper> cards)
        {
            foreach (CardWrapper card in cards)
            {
                _resultDecks[_activePlayer - 1].Add(card);
            }
        }

        //Bestämmer vilken spelare som ska bli aktiv
        private void SetActive()
        {
            if (_activePlayer == NumberOfPlayers)
            {
                _activePlayer = 1;
                _activeCard++;
            }
            else
            {
                _activePlayer++;
            }
        }

        //Slumpar fram de olika "händer" som ska användas under draften
        private ObservableCollection<CardWrapper>[][] GetDraftDecks()
        {
            ObservableCollection<CardWrapper>[][] decks = new ObservableCollection<CardWrapper>[NumberOfRounds][];

            for (int i = 0; i < NumberOfRounds; i++)
            {
                decks[i] = new ObservableCollection<CardWrapper>[NumberOfPlayers];
                for (int j = 0; j < NumberOfPlayers; j++)
                {
                    decks[i][j] = new ObservableCollection<CardWrapper>();
                    for (int k = 0; k < NumberOfCards; k++)
                    {
                        CardWrapper card = CardList[_rnd.Next(0, CardList.Count)];
                        decks[i][j].Add(card);
                        CardList.Remove(card);
                    }
                }
            }

            return decks;
        }
    }
}
