using DraftTool.Models;
using DraftTool.UI.Event;
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
    public class DraftVM : ViewModelBase, IDraftVM
    {
        private IEventAggregator _eventAggregator;
        private Card _selectedCard;

        public ObservableCollection<Card> AvailableCards { get; set; }
        public ObservableCollection<Card> ChosenCards { get; set; }
        public ICommand SelectedCommand { get; }

        public DraftVM(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            ChosenCards = new ObservableCollection<Card>();

            SelectedCommand = new DelegateCommand(OnSelected);
        }

        public Card SelectedCard
        {
            get { return _selectedCard; }
            set
            {
                _selectedCard = value;
                OnPropertyChanged();
            }
        }

        private void OnSelected()
        {
            ChosenCards.Add(SelectedCard);
            AvailableCards.Remove(SelectedCard);
            if (AvailableCards.Count() == 0)
            {
                _eventAggregator.GetEvent<PlayerDoneEvent>().Publish(new PlayerDoneEventArgs { ResultDeck = ChosenCards });
            }
            else
            {
                _eventAggregator.GetEvent<PlayerDoneEvent>().Publish(null);
            }
        }
    }
}
