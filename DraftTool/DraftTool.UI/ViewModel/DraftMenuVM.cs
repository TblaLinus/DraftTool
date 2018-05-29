using DraftTool.Models;
using DraftTool.UI.Event;
using DraftTool.UI.HelperClasses;
using DraftTool.UI.Service;
using DraftTool.UI.Startup;
using DraftTool.UI.ViewModel.Interfaces;
using DraftTool.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DraftTool.UI.ViewModel
{
    public class DraftMenuVM : ViewModelBase, IDraftMenuVM
    {
        private const int _numberOfRounds = 4;
        private const int _numberOfCards = 4;
        private int _numberOfPlayers;
        private string _side;
        private List<CardWrapper> _cards;
        private IEventAggregator _eventAggregator;
        private ICardService _cardService;
        private ISetService _setService;

        public ObservableCollection<int> NumberOfPlayersOptions { get; }
        public ObservableCollection<string> SideOptions { get; }
        public ItemsChangeObservableCollection<SetWrapper> Sets { get; set; }
        public ICommand StartDraftCommand { get; }

        public DraftMenuVM(IEventAggregator eventAggregator, ICardService cardService, ISetService setService)
        {
            _eventAggregator = eventAggregator;
            _cardService = cardService;
            _setService = setService;

            NumberOfPlayersOptions = new ObservableCollection<int> { 2, 3, 4, 5, 6 };
            SideOptions = new ObservableCollection<string> { "Corp", "Runner" };
            Sets = new ItemsChangeObservableCollection<SetWrapper>(_setService.Sets);

            Sets.CollectionChanged += Sets_CollectionChanged;
            _numberOfPlayers = 2;
            _side = "Corp";

            StartDraftCommand = new DelegateCommand(OnStartDraft, OnStartDraftCanExecute);
        }

        private void Sets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ((DelegateCommand)StartDraftCommand).RaiseCanExecuteChanged();
        }

        public int NumberOfPlayers
        {
            get { return _numberOfPlayers; }
            set
            {
                _numberOfPlayers = value;
                OnPropertyChanged();
                ((DelegateCommand)StartDraftCommand).RaiseCanExecuteChanged();
            }
        }

        public string Side
        {
            get { return _side; }
            set
            {
                _side = value;
                OnPropertyChanged();
                ((DelegateCommand)StartDraftCommand).RaiseCanExecuteChanged();
            }
        }

        private void OnStartDraft()
        {
            _eventAggregator.GetEvent<StartDraftEvent>().Publish(
                new StartDraftEventArgs { NumberOfRounds = _numberOfRounds, NumberOfPlayers = NumberOfPlayers, NumberOfCards = _numberOfCards, CardList = _cards });
            Sets.Clear();
        }

        private bool OnStartDraftCanExecute()
        {
            _cards = _cardService.GetCardsWithNumbers(Side, Sets.Where(s => s.IsUsed).Select(s => s.Name));
            return _cards.Count() >= _numberOfPlayers * _numberOfRounds * _numberOfCards;
        }
    }
}
