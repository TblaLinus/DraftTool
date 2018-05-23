using DraftTool.Models;
using DraftTool.UI.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.UI.ViewModel
{
    public class ResultVM : ViewModelBase, IResultVM
    {
        private int _player;
        private List<Card>[] _resultDecks;

        public int Player
        {
            get { return _player; }
            set
            {
                _player = value;
                OnPropertyChanged();
            }
        }

        public List<Card>[] ResultDecks
        {
            get { return _resultDecks; }
            set
            {
                _resultDecks = value;
                OnPropertyChanged();
            }
        }
    }
}
