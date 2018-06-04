using DraftTool.Models;

namespace DraftTool.UI.Wrapper
{
    public class CardWrapper : WrapperBase<Card>
    {
        private int _numberOfUses;
        public CardWrapper(Card model) : base(model)
        {
        }

        //Anger hur många gånger ett kort ska användas
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

        public string Side
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        
        public string Set
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
