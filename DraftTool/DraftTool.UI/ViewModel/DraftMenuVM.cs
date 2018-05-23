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
    public class DraftMenuVM : ViewModelBase, IDraftMenuVM
    {
        private const int _numberOfRounds = 4;
        private const int _numberOfPlayers = 4;
        private const int _numberOfCards = 3;
        private IEventAggregator _eventAggregator;

        public ICommand StartDraftCommand { get; }

        public DraftMenuVM(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            StartDraftCommand = new DelegateCommand(OnStartDraft);
        }

        private void OnStartDraft()
        {
            _eventAggregator.GetEvent<StartDraftEvent>().Publish(new StartDraftEventArgs { NumberOfRounds = _numberOfRounds, NumberOfPlayers = _numberOfPlayers, NumberOfCards = _numberOfCards });
        }
    }
}
