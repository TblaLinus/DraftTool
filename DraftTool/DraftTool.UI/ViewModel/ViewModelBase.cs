using DraftTool.UI.ViewModel.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DraftTool.UI.ViewModel
{
    public class ViewModelBase : IViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
