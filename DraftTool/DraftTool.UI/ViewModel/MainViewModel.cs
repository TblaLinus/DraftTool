using DraftTool.Models;
using Prism.Commands;
using Prism.Events;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace DraftTool.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;

        public ICommand ExitApplicationCommand { get; }
        public List<Card> CardList { get; set; }

        public MainViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            ExitApplicationCommand = new DelegateCommand(OnExitApplication);
            CardList = Startup.CreateCards.Create();
        }

        private void OnExitApplication()
        {
            Application.Current.Shutdown();
        }
    }
}
