using Prism.Commands;
using Prism.Events;
using System.Windows;
using System.Windows.Input;

namespace DraftTool.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand ExitApplicationCommand { get; }

        private IEventAggregator _eventAggregator;

        public MainViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            ExitApplicationCommand = new DelegateCommand(OnExitApplication);
        }

        private void OnExitApplication()
        {
            Application.Current.Shutdown();
        }
    }
}
