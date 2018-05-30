using System.Windows.Controls;

namespace DraftTool.UI.View
{
    /// <summary>
    /// Interaction logic for DraftView.xaml
    /// </summary>
    public partial class DraftView : UserControl
    {
        public DraftView()
        {
            InitializeComponent();
        }

        private void AvailableList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(AvailableList.SelectedValue != null)
                ChosenList.UnselectAll();
        }

        private void ChosenList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ChosenList.SelectedValue != null)
                AvailableList.UnselectAll();
        }
    }
}
