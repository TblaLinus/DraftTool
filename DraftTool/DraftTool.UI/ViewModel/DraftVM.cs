using DraftTool.Models;
using DraftTool.UI.Event;
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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DraftTool.UI.ViewModel
{
    public class DraftVM : ViewModelBase, IDraftVM
    {
        private IEventAggregator _eventAggregator;
        private CardWrapper _selectedCard;
        private CardWrapper _selectedAvailableCard;
        private CardWrapper _selectedChosenCard;

        public ObservableCollection<CardWrapper> AvailableCards { get; set; }
        public ObservableCollection<CardWrapper> ChosenCards { get; set; }
        public ICommand SelectedCommand { get; }

        public DraftVM(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            ChosenCards = new ObservableCollection<CardWrapper>();

            SelectedCommand = new DelegateCommand(OnSelected);
        }

        public CardWrapper SelectedCard
        {
            get { return _selectedCard; }
            set
            {
                _selectedCard = value;
                OnPropertyChanged();
            }
        }

        public CardWrapper SelectedAvailableCard
        {
            get { return _selectedAvailableCard; }
            set
            {
                _selectedAvailableCard = value;
                if (_selectedAvailableCard != null)
                {
                    SelectedCard = value;
                    SelectedChosenCard = null;
                }
            }
        }

        public CardWrapper SelectedChosenCard
        {
            get { return _selectedChosenCard; }
            set
            {
                _selectedChosenCard = value;
                if (_selectedChosenCard != null)
                {
                    SelectedCard = value;
                    SelectedAvailableCard = null;
                }
            }
        }

        private void OnSelected()
        {
            ChosenCards.Add(SelectedAvailableCard);
            AvailableCards.Remove(SelectedAvailableCard);
            SelectedCard = null;
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
