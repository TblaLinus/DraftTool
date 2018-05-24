using DraftTool.Models;
using DraftTool.UI.Event;
using DraftTool.UI.Startup;
using DraftTool.UI.ViewModel.Interfaces;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DraftTool.UI.ViewModel
{
    public class DraftMenuVM : ViewModelBase, IDraftMenuVM
    {
        private const int _numberOfRounds = 4;
        private const int _numberOfCards = 3;
        private int _numberOfPlayers;
        private string _side;
        private string _set;
        private List<Card> _cards;
        private IEventAggregator _eventAggregator;
        private ICardRepo _cardRepo;

        public ObservableCollection<int> NumberOfPlayersOptions { get; }
        public ObservableCollection<string> SideOptions { get; }
        public ICommand StartDraftCommand { get; }

        public DraftMenuVM(IEventAggregator eventAggregator, ICardRepo cardRepo)
        {
            NumberOfPlayersOptions = new ObservableCollection<int> {2, 3, 4, 5, 6};
            SideOptions = new ObservableCollection<string> {"Corp", "Runner"};
            _numberOfPlayers = 2;
            _side = "Corp";
            _set = "Core";

            _eventAggregator = eventAggregator;
            _cardRepo = cardRepo;

            StartDraftCommand = new DelegateCommand(OnStartDraft);
        }

        public int NumberOfPlayers
        {
            get { return _numberOfPlayers; }
            set
            {
                _numberOfPlayers = value;
                OnPropertyChanged();
            }
        }

        public string Side
        {
            get { return _side; }
            set
            {
                _side = value;
                OnPropertyChanged();
            }
        }

        private void OnStartDraft()
        {
            _cards = _cardRepo.GetDecks(Side, _set);
            _eventAggregator.GetEvent<StartDraftEvent>().Publish(
                new StartDraftEventArgs { NumberOfRounds = _numberOfRounds, NumberOfPlayers = NumberOfPlayers, NumberOfCards = _numberOfCards, CardList = _cards });
        }
    }
}
