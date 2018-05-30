using DraftTool.UI.Event;
using DraftTool.UI.HelperClasses;
using DraftTool.UI.Service;
using DraftTool.UI.ViewModel.Interfaces;
using DraftTool.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DraftTool.UI.ViewModel
{
    public class CardMenuVM : ViewModelBase, ICardMenuVM
    {
        private string _side;
        private IEventAggregator _eventAggregator;
        private ICardService _cardService;
        private ISetService _setService;
        private List<CardWrapper> _cards;

        public ObservableCollection<string> SideOptions { get; }
        public ItemsChangeObservableCollection<SetWrapper> Sets { get; set; }
        public ICommand StartCardListCommand { get; }

        public CardMenuVM(IEventAggregator eventAggregator, ICardService cardService, ISetService setService)
        {
            _eventAggregator = eventAggregator;
            _cardService = cardService;
            _setService = setService;

            SideOptions = new ObservableCollection<string> { "Corp", "Runner" };
            Sets = new ItemsChangeObservableCollection<SetWrapper>(_setService.Sets);

            Sets.CollectionChanged += Sets_CollectionChanged;
            _side = "Corp";

            StartCardListCommand = new DelegateCommand(OnStartCardList, OnStartCardListCanExecute);
        }

        private void Sets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ((DelegateCommand)StartCardListCommand).RaiseCanExecuteChanged();
        }

        public string Side
        {
            get { return _side; }
            set
            {
                _side = value;
                OnPropertyChanged();
                ((DelegateCommand)StartCardListCommand).RaiseCanExecuteChanged();
            }
        }

        private void OnStartCardList()
        {
            _eventAggregator.GetEvent<StartCardListEvent>().Publish(
                new StartCardListEventArgs { Side = Side, Sets = Sets.Where(s => s.IsUsed).ToList() });
            Sets.Clear();
        }

        private bool OnStartCardListCanExecute()
        {
            _cards = _cardService.GetUsedCards(Side, Sets.Where(s => s.IsUsed).Select(s => s.Name));
            return _cards.Count() > 0;
        }
    }
}
