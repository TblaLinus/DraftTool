using DraftTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.UI.ViewModel.Interfaces
{
    public interface IResultVM
    {
        int Player { get; set; }
        List<Card>[] ResultDecks { get; set; }
    }
}
