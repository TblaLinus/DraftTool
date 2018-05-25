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
using System.Windows.Input;

namespace DraftTool.UI.ViewModel
{
    public class ResultVM : ViewModelBase, IResultVM
    {
        private int _player;
        private ObservableCollection<CardWrapper> _resultDeck;
        private string _resultText;
        private IEventAggregator _eventAggregator;

        public ICommand DoneCommand { get; }

        public ResultVM(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            DoneCommand = new DelegateCommand(OnDone);
        }

        public int Player
        {
            get { return _player; }
            set
            {
                _player = value;
                ResultText = $"Player {Player}'s Cards";
            }
        }

        public ObservableCollection<CardWrapper> ResultDeck
        {
            get { return _resultDeck; }
            set
            {
                _resultDeck = value;
                OnPropertyChanged();
            }
        }

        public string ResultText
        {
            get { return _resultText; }
            set
            {
                _resultText = value;
                OnPropertyChanged();
            }
        }

        private void OnDone()
        {
            _eventAggregator.GetEvent<PlayerDoneEvent>().Publish(null);
        }
    }
}
