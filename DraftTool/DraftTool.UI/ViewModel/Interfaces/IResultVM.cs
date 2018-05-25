using DraftTool.Models;
using DraftTool.UI.Wrapper;
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
        ObservableCollection<CardWrapper> ResultDeck { get; set; }
    }
}
