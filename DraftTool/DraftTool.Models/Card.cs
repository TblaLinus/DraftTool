using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.Models
{
    public class Card : INotifyPropertyChanged
    {
        private int _numberOfUses;

        public string Name { get; set; }
        public string ImageURL { get; set; }
        public string CardSide { get; set; }
        public string CardSet { get; set; }
        public int MaxNumberOfUses { get; set; }

        public int NumberOfUses
        {
            get { return _numberOfUses; }
            set
            {
                _numberOfUses = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
