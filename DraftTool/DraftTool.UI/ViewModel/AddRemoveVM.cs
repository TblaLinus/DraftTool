using DraftTool.Models;
using DraftTool.UI.Event;
using DraftTool.UI.Service;
using DraftTool.UI.ViewModel.Interfaces;
using DraftTool.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace DraftTool.UI.ViewModel
{
    //Sida för att lägga till och ta bort objekt i databasen
    public class AddRemoveVM : ViewModelBase, IAddRemoveVM
    {
        private IEventAggregator _eventAggregator;
        private ICardService _cardService;
        private ISetService _setService;
        private ICubeService _cubeService;
        private SetWrapper _selectedSet;
        private CardWrapper _selecetedCard;
        private CubeWrapper _selectedCube;
        private string _setName;
        private string _cardName;
        private string _cardImage;
        private string _cardSide;
        private SetWrapper _cardSet;
        private int? _cardNumberOfUses;

        public ObservableCollection<SetWrapper> Sets { get; set; } 
        public ObservableCollection<CardWrapper> Cards { get; set; }
        public ObservableCollection<CubeWrapper> Cubes { get; set; }
        public ObservableCollection<string> SideOptions { get; }
        public ObservableCollection<int> NumberOptions { get; }

        public ICommand AddSetCommand { get; }
        public ICommand AddCardCommand { get; }
        public ICommand DeleteSetCommand { get; }
        public ICommand DeleteCardCommand { get; }
        public ICommand DeleteCubeCommand { get; }

        public AddRemoveVM(IEventAggregator eventAggregator, ICardService cardService, ISetService setService, ICubeService cubeService)
        {
            _eventAggregator = eventAggregator;
            _cardService = cardService;
            _setService = setService;
            _cubeService = cubeService;

            _eventAggregator.GetEvent<AddCubeEvent>().Subscribe(OnAddCube);

            Sets = new ObservableCollection<SetWrapper>(_setService.Sets);
            Cards = new ObservableCollection<CardWrapper>(_cardService.Cards);
            Cubes = new ObservableCollection<CubeWrapper>(_cubeService.GetCubes());
            SideOptions = new ObservableCollection<string> { "Corp", "Runner" };
            NumberOptions = new ObservableCollection<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            AddSetCommand = new DelegateCommand(OnAddSet, OnAddSetCanExecute);
            AddCardCommand = new DelegateCommand(OnAddCard, OnAddCardCanExecute);
            DeleteSetCommand = new DelegateCommand(OnDeleteSet, OnDeleteSetCanExecute);
            DeleteCardCommand = new DelegateCommand(OnDeleteCard, OnDeleteCardCanExecute);
            DeleteCubeCommand = new DelegateCommand(OnDeleteCube, OnDeleteCubeCanExecute);
        }

        public SetWrapper SelectedSet
        {
            get { return _selectedSet; }
            set
            {
                _selectedSet = value;
                OnPropertyChanged();
                ((DelegateCommand)DeleteSetCommand).RaiseCanExecuteChanged();
            }
        }

        public CardWrapper SelectedCard
        {
            get { return _selecetedCard; }
            set
            {
                _selecetedCard = value;
                OnPropertyChanged();
                ((DelegateCommand)DeleteCardCommand).RaiseCanExecuteChanged();
            }
        }

        public CubeWrapper SelectedCube
        {
            get { return _selectedCube; }
            set
            {
                _selectedCube = value;
                OnPropertyChanged();
                ((DelegateCommand)DeleteCubeCommand).RaiseCanExecuteChanged();
            }
        }

        public string SetName
        {
            get { return _setName; }
            set
            {
                _setName = value;
                OnPropertyChanged();
                ((DelegateCommand)AddSetCommand).RaiseCanExecuteChanged();
            }
        }

        public string CardName
        {
            get { return _cardName; }
            set
            {
                _cardName = value;
                OnPropertyChanged();
                ((DelegateCommand)AddCardCommand).RaiseCanExecuteChanged();
            }
        }

        public string CardImage
        {
            get { return _cardImage; }
            set
            {
                _cardImage = value;
                OnPropertyChanged();
                ((DelegateCommand)AddCardCommand).RaiseCanExecuteChanged();
            }
        }

        public string CardSide
        {
            get { return _cardSide; }
            set
            {
                _cardSide = value;
                OnPropertyChanged();
                ((DelegateCommand)AddCardCommand).RaiseCanExecuteChanged();
            }
        }

        public SetWrapper CardSet
        {
            get { return _cardSet; }
            set
            {
                _cardSet = value;
                OnPropertyChanged();
                ((DelegateCommand)AddCardCommand).RaiseCanExecuteChanged();
            }
        }

        public int? CardNumberOfUses
        {
            get { return _cardNumberOfUses; }
            set
            {
                _cardNumberOfUses = value;
                OnPropertyChanged();
                ((DelegateCommand)AddCardCommand).RaiseCanExecuteChanged();
            }
        }

        private void OnAddSet()
        {
            SetWrapper set = new SetWrapper(new Set());
            set.Name = SetName;
            _setService.AddSetToDB(set);
            SetName = "";
            Sets.Add(set);
            ((DelegateCommand)AddSetCommand).RaiseCanExecuteChanged();
        }

        public bool OnAddSetCanExecute()
        {
            return !String.IsNullOrEmpty(SetName) && !Sets.Select(s => s.Name).Contains(SetName);
        }

        private void OnAddCard()
        {
            CardWrapper card = new CardWrapper(new Card());
            card.Name = CardName;
            card.ImageURL = CardImage;
            card.Side = CardSide;
            card.Set = CardSet.Name;
            card.MaxNumberOfUses = (int)CardNumberOfUses;
            _cardService.AddCardToDB(card);
            CardName = "";
            CardImage = "";
            CardSide = null;
            CardSet = null;
            CardNumberOfUses = null;
            Cards.Add(card);
            ((DelegateCommand)AddCardCommand).RaiseCanExecuteChanged();
        }

        public bool OnAddCardCanExecute()
        {
            bool nameOk = !String.IsNullOrEmpty(CardName) && !Cards.Select(c => c.Name).Contains(CardName);
            bool imageOk = !String.IsNullOrEmpty(CardImage) && !Cards.Select(c => c.ImageURL).Contains(CardImage);
            bool sideOk = !String.IsNullOrEmpty(CardSide);
            bool setOk = CardSet != null;
            bool numberOk = CardNumberOfUses != null && CardNumberOfUses > 0;
            return nameOk && imageOk && sideOk && setOk && numberOk;
        }

        private void OnDeleteSet()
        {
            _setService.DeleteSetFromDB(SelectedSet);
            Sets.Remove(SelectedSet);
            SelectedSet = null;
            ((DelegateCommand)DeleteSetCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)AddSetCommand).RaiseCanExecuteChanged();
        }

        public bool OnDeleteSetCanExecute()
        {
            return SelectedSet != null;
        }

        private void OnDeleteCard()
        {
            _cardService.DeleteCardFromDB(SelectedCard);
            Cards.Remove(SelectedCard);
            SelectedCard = null;
            ((DelegateCommand)DeleteCardCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)AddCardCommand).RaiseCanExecuteChanged();
        }

        public bool OnDeleteCardCanExecute()
        {
            return SelectedCard != null;
        }
        private void OnDeleteCube()
        {
            _cubeService.DeleteCube(SelectedCube);
            Cubes.Remove(SelectedCube);
            SelectedCube = null;
            ((DelegateCommand)DeleteCubeCommand).RaiseCanExecuteChanged();
        }

        public bool OnDeleteCubeCanExecute()
        {
            return SelectedCube != null;
        }

        private void OnAddCube(AddCubeEventArgs args)
        {
            Cubes.Add(args.Cube);
        }
    }
}
