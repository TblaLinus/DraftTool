using DraftTool.Models;
using DraftTool.UI.ViewModel.Interfaces;
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
        private IDraftMenuVM _draftMenuVM;
        private IPageVM _activePage;

        public ICommand NewDraftCommand { get; }
        public ICommand ExitApplicationCommand { get; }
        public List<Card> CardList { get; set; }

        public MainViewModel(IEventAggregator eventAggregator, IDraftMenuVM draftMenuVM)
        {
            _eventAggregator = eventAggregator;
            _draftMenuVM = draftMenuVM;
            NewDraftCommand = new DelegateCommand(OnNewDraft);
            ExitApplicationCommand = new DelegateCommand(OnExitApplication);

            CardList = Startup.CreateCards.Create();
        }

        private void OnNewDraft()
        {
            ActivePage = (IPageVM)_draftMenuVM;
        }

        private void OnExitApplication()
        {
            Application.Current.Shutdown();
        }

        public IPageVM ActivePage
        {
            get
            {
                return _activePage;
            }

            set
            {
                _activePage = value;
                OnPropertyChanged();
            }
        }
    }
}
