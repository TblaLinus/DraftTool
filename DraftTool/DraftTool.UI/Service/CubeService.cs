using DraftTool.DataAccess;
using DraftTool.Models;
using DraftTool.UI.Wrapper;
using Prism.Events;
using System.Collections.Generic;

namespace DraftTool.UI.Service
{
    class CubeService : DBServiceBase, ICubeService
    {
        //Sköter kommunikation mellan CubeRepo och resten av programmet
        private ICubeRepo _repo;
        public CubeService(IEventAggregator eventAggregator, ICubeRepo repo) : base(eventAggregator)
        {
            _repo = repo;
        }

        public void AddCube(CubeWrapper cube)
        {
            _repo.AddCube(cube.Model);
            foreach(string card in cube.CardNames)
            {
                _repo.AddCard(card, cube.Name);
            }
        }

        public void DeleteCube(CubeWrapper cube)
        {
            _repo.DeleteCube(cube.Name);
            foreach (string card in cube.CardNames)
            {
                _repo.DeleteCard(card, cube.Name);
            }
        }

        public List<CubeWrapper> GetCubes()
        {
            List<CubeWrapper> cubes = new List<CubeWrapper>();
            foreach (Cube cube in _repo.GetCubes())
            {
                cube.CardNames = new List<string>();
                foreach (string cardName in _repo.GetCards(cube.Name))
                {
                    cube.CardNames.Add(cardName);
                }
                cubes.Add(new CubeWrapper(cube));
            }
            return cubes;
        }
    }
}
