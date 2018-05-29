using DraftTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.DataAccess
{
    public interface ISetRepo
    {
        List<Set> GetSets();
        void AddSet(Set set);
        void DeleteSet(string name);
    }
}
