using DraftTool.UI.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.UI.ViewModel.Interfaces
{
    public interface ICardVM
    {
        CardWrapper Card { get; set; }
        void OnAddCard();
        void OnRemoveCard();
    }
}
