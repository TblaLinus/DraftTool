using DraftTool.UI.Event;
using DraftTool.UI.ViewModel.Interfaces;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DraftTool.UI.ViewModel
{
    public class PlayerReadyVM : ViewModelBase, IPlayerReadyVM
    {
        private int _player { get; set; }
        private string _readyText;
        private IEventAggregator _eventAggregator;

        public bool DraftFinished { get; set; }
        public ICommand ReadyCommand { get; }

        public PlayerReadyVM(IEventAggregator eventAggregator)
        {
            Player = 1;
            DraftFinished = false;

            _eventAggregator = eventAggregator;

            ReadyCommand = new DelegateCommand(OnReady);
        }

        public int Player
        {
            get { return _player; }
            set
            {
                _player = value;
                OnPropertyChanged();
                if (DraftFinished)
                {
                    ReadyText = $"Player{_player}'s cards";
                }
                else
                {
                    ReadyText = $"Player{_player}'s turn";
                }
            }
        }

        public string ReadyText
        {
            get { return _readyText; }
            set
            {
                _readyText = value;
                OnPropertyChanged();
            }
        }

        private void OnReady()
        {
            _eventAggregator.GetEvent<PlayerReadyEvent>().Publish(new PlayerReadyEventArgs { Player = Player, Results = DraftFinished});
        }
    }
}
