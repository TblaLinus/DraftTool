using DraftTool.Models;
using System.Collections.Generic;

namespace DraftTool.DataAccess
{
    public interface ISetRepo
    {
        List<Set> GetSets();
        void AddSet(Set set);
        void DeleteSet(string name);
    }
}
