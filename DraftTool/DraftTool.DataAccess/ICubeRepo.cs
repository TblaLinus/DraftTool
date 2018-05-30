using DraftTool.Models;
using System.Collections.Generic;

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
