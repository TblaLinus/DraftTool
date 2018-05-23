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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DraftTool.UI.ViewModel
{
    public class DraftVM : ViewModelBase, IDraftVM
    {
        private IEventAggregator _eventAggregator;
        private Card _selectedCard;
        private Card _selectedAvailableCard;
        private Card _selectedChosenCard;

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

        public Card SelectedAvailableCard
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

        public Card SelectedChosenCard
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

        private void ListBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((ListBox)sender).SelectedItem = null;
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
