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
        private IEventAggregator _eventAggregator;

        public ICommand StartDraftCommand { get; }

        public DraftMenuVM(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            StartDraftCommand = new DelegateCommand(OnStartDraft);
        }

        private void OnStartDraft()
        {
            _eventAggregator.GetEvent<StartDraftEvent>().Publish();
        }
    }
}
