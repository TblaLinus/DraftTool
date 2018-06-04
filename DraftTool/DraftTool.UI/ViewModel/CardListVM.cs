using DraftTool.Models;
using DraftTool.UI.Event;
using DraftTool.UI.Service;
using DraftTool.UI.ViewModel.Interfaces;
using DraftTool.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace DraftTool.UI.ViewModel
{
    //Sida för att bestämma hur många av varje kort som ska användas
    public class CardListVM : ViewModelBase, ICardListVM
    {
        private IEventAggregator _eventAggregator;
        private ICardService _cardService;
        private ICubeService _cubeService;
        private Func<CardWrapper, ICardVM> _cardVMCreator;
        private string _side;
        private List<SetWrapper> _sets;
        private string _cubeName;

        public ObservableCollection<ICardVM> CardVMList { get; set; }
        public ICommand AddCardCommand { get; }
        public ICommand RemoveCardCommand { get; }
        public ICommand SaveCubeCommand { get; }
        public ICommand AddAllCommand { get; }
        public ICommand RemoveAllCommand { get; }
        public ICommand ExitCommand { get; }

        public CardListVM(IEventAggregator eventAggregator, ICardService cardService, ICubeService cubeService, Func<CardWrapper, ICardVM> cardVMCreator)
        {
            _eventAggregator = eventAggregator;
            _cardService = cardService;
            _cubeService = cubeService;
            _cardVMCreator = cardVMCreator;

            _eventAggregator.GetEvent<StartCardListEvent>().Subscribe(OnStartCardList);

            SaveCubeCommand = new DelegateCommand(OnSaveCube, OnSaveCubeCanExecute);
            AddAllCommand = new DelegateCommand(OnAddAll);
            RemoveAllCommand = new DelegateCommand(OnRemoveAll);
            ExitCommand = new DelegateCommand(OnExit);
        }

        public string CubeName
        {
            get { return _cubeName; }
            set
            {
                _cubeName = value;
                OnPropertyChanged();
                ((DelegateCommand)SaveCubeCommand).RaiseCanExecuteChanged();
            }
        }

        //Hämtar en lista på alla kort från aktuell sida och set
        private void OnStartCardList(StartCardListEventArgs args)
        {
            _side = args.Side;
            _sets = args.Sets;
            List<CardWrapper> cards = _cardService.GetUsedCards( _side, _sets.Select(s => s.Name));
            CardVMList = new ObservableCollection<ICardVM>();
            foreach (CardWrapper card in cards)
            {
                ICardVM cardVM = _cardVMCreator(card);
                CardVMList.Add(cardVM);
            }
        }

        private void OnSaveCube()
        {
            CubeWrapper cube = new CubeWrapper(new Cube());
            cube.Name = CubeName;
            cube.CardNames = _cardService.GetCardsWithNumbers(_side, _sets.Select(s => s.Name)).Select(c => c.Name).ToList();
            _cubeService.AddCube(cube);
            CubeName = "";
            _eventAggregator.GetEvent<AddCubeEvent>().Publish( new AddCubeEventArgs { Cube = cube });
        }

        private void OnAddAll()
        {
            foreach(ICardVM card in CardVMList)
            {
                while (card.Card.NumberOfUses < card.Card.MaxNumberOfUses)
                {
                    card.OnAddCard();
                }
            }
        }

        private void OnRemoveAll()
        {
            foreach (ICardVM card in CardVMList)
            {
                while (card.Card.NumberOfUses > 0)
                {
                    card.OnRemoveCard();
                }
            }
        }

        private void OnExit()
        {
            _eventAggregator.GetEvent<BackToMainEvent>().Publish();
        }

        public bool OnSaveCubeCanExecute()
        {
            List<string> cubeNames = _cubeService.GetCubes().Select(c => c.Name).ToList();
            return (!String.IsNullOrEmpty(CubeName) && !cubeNames.Contains(CubeName));
        }
    }
}
