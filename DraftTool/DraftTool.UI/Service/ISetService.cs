using DraftTool.UI.Wrapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.UI.Service
{
    public interface ISetService
    {
        ObservableCollection<SetWrapper> Sets { get; }
    }
}
