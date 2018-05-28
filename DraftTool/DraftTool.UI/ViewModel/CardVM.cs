using DraftTool.UI.Event;
using DraftTool.UI.ViewModel.Interfaces;
using DraftTool.UI.Wrapper;
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
    public class CardVM : ViewModelBase, ICardVM
    {
        private IEventAggregator _eventAggregator;

        public CardWrapper Card { get; set; }
        public ICommand AddCardCommand { get; }
        public ICommand RemoveCardCommand { get; }

        public CardVM(IEventAggregator eventAggregator, CardWrapper card)
        {
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<AddAllEvent>().Subscribe(OnAddAll);
            _eventAggregator.GetEvent<RemoveAllEvent>().Subscribe(OnRemoveAll);

            Card = card;

            AddCardCommand = new DelegateCommand(OnAddCard, OnAddCanExecute);
            RemoveCardCommand = new DelegateCommand(OnRemoveCard, OnRemoveCanExecute);
        }

        private void OnAddCard()
        {
            Card.NumberOfUses++;
            _eventAggregator.GetEvent<AddCardEvent>().Publish(new AddCardEventArgs { Name = Card.Name });
            ((DelegateCommand)AddCardCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)RemoveCardCommand).RaiseCanExecuteChanged();
        }

        private void OnRemoveCard()
        {
            Card.NumberOfUses--;
            _eventAggregator.GetEvent<RemoveCardEvent>().Publish(new RemoveCardEventArgs { Name = Card.Name });
            ((DelegateCommand)AddCardCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)RemoveCardCommand).RaiseCanExecuteChanged();
        }

        private void OnAddAll()
        {
            Card.NumberOfUses = Card.MaxNumberOfUses;
            ((DelegateCommand)AddCardCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)RemoveCardCommand).RaiseCanExecuteChanged();
        }

        private void OnRemoveAll()
        {
            Card.NumberOfUses = 0;
            ((DelegateCommand)AddCardCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)RemoveCardCommand).RaiseCanExecuteChanged();
        }

        private bool OnAddCanExecute()
        {
            return Card.NumberOfUses < Card.MaxNumberOfUses;
        }

        private bool OnRemoveCanExecute()
        {
            return Card.NumberOfUses > 0;
        }
    }
}
