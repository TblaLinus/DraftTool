using DraftTool.UI.Wrapper;

namespace DraftTool.UI.ViewModel.Interfaces
{
    public interface ICardVM
    {
        CardWrapper Card { get; set; }
        void OnAddCard();
        void OnRemoveCard();
    }
}
