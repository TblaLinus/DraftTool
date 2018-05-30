using DraftTool.UI.Wrapper;
using System.Collections.Generic;

namespace DraftTool.UI.Service
{
    public interface ISetService
    {
        List<SetWrapper> Sets { get; }
        void AddSetToDB(SetWrapper set);
        void DeleteSetFromDB(SetWrapper set);
    }
}
