using DraftTool.UI.ViewModel;
using System.Windows;

namespace DraftTool.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;

        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;
            _viewModel = viewModel;
            DataContext = _viewModel;
        }
    }
}
