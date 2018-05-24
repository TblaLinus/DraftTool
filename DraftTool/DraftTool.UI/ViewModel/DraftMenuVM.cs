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
        private List<Card> _cards;
        private IEventAggregator _eventAggregator;
        private IRepo _cardRepo;

        public ObservableCollection<int> NumberOfPlayersOptions { get; }
        public ObservableCollection<string> SideOptions { get; }
        public ObservableCollection <Set> Sets { get; set; }
        public ICommand StartDraftCommand { get; }

        public DraftMenuVM(IEventAggregator eventAggregator, IRepo cardRepo)
        {
            _eventAggregator = eventAggregator;
            _cardRepo = cardRepo;

            NumberOfPlayersOptions = new ObservableCollection<int> {2, 3, 4, 5, 6};
            SideOptions = new ObservableCollection<string> {"Corp", "Runner"};
            Sets = new ObservableCollection<Set>(_cardRepo.Sets);
            _numberOfPlayers = 2;
            _side = "Corp";

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
            _cards = _cardRepo.GetDecks(Side, Sets.Where(s => s.IsUsed).Select(s => s.Name));
            _eventAggregator.GetEvent<StartDraftEvent>().Publish(
                new StartDraftEventArgs { NumberOfRounds = _numberOfRounds, NumberOfPlayers = NumberOfPlayers, NumberOfCards = _numberOfCards, CardList = _cards });
        }
    }
}
