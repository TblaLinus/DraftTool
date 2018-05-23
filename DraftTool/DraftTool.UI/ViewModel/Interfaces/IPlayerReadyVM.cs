using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.UI.ViewModel.Interfaces
{
    public interface IPlayerReadyVM
    {
        int Player { get; set; }
        bool Results { get; set; }
    }
}
