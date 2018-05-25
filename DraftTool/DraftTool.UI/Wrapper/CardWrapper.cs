using DraftTool.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.UI.Wrapper
{
    public class CardWrapper : WrapperBase<Card>
    {
        private int _numberOfUses;
        public CardWrapper(Card model) : base(model)
        {
        }

        public int NumberOfUses
        {
            get { return _numberOfUses; }
            set
            {
                _numberOfUses = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string ImageURL
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string CardSide
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        
        public string CardSet
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public int MaxNumberOfUses
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }
    }
}
