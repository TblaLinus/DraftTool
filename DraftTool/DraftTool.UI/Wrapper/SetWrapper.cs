using DraftTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.UI.Wrapper
{
    public class SetWrapper : WrapperBase<Set>
    {
        private bool _isUsed;
        public SetWrapper(Set model) : base(model)
        {
        }

        public bool IsUsed
        {
            get { return _isUsed; }
            set
            {
                _isUsed = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
    }
}
