using DraftTool.Models;
using DraftTool.UI.Event;
using DraftTool.UI.Startup;
using DraftTool.UI.ViewModel.Interfaces;
using DraftTool.UI.Wrapper;
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
        private const int _numberOfCards = 4;
        private int _numberOfPlayers;
        private string _side;
        private List<CardWrapper> _cards;
        private IEventAggregator _eventAggregator;
        private IRepo _repo;

        public ObservableCollection<int> NumberOfPlayersOptions { get; }
        public ObservableCollection<string> SideOptions { get; }
        public ObservableCollection <Set> Sets { get; set; }
        public ICommand StartDraftCommand { get; }

        public DraftMenuVM(IEventAggregator eventAggregator, IRepo repo)
        {
            _eventAggregator = eventAggregator;
            _repo = repo;

            NumberOfPlayersOptions = new ObservableCollection<int> {2, 3, 4, 5, 6};
            SideOptions = new ObservableCollection<string> {"Corp", "Runner"};
            Sets = _repo.Sets;
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
            _cards = _repo.GetUsedCards(Side, Sets.Where(s => s.IsUsed).Select(s => s.Name));
            _eventAggregator.GetEvent<StartDraftEvent>().Publish(
                new StartDraftEventArgs { NumberOfRounds = _numberOfRounds, NumberOfPlayers = NumberOfPlayers, NumberOfCards = _numberOfCards, CardList = _cards });
        }
    }
}
