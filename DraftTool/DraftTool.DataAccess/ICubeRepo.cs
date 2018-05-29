using DraftTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.DataAccess
{
    public interface ICubeRepo
    {
        List<Cube> GetCubes();
        void AddCube(Cube cube);
        void DeleteCube(string name);
        List<string> GetCards(string cube);
        void AddCard(string cardName, string cubeName);
        void DeleteCard(string cardName, string cubeName);
    }
}
