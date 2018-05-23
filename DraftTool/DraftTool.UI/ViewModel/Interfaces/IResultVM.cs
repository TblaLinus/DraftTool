using DraftTool.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.UI.ViewModel.Interfaces
{
    public interface IResultVM
    {
        int Player { get; set; }
        ObservableCollection<Card> ResultDeck { get; set; }
    }
}
