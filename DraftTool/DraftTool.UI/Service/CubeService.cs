using DraftTool.DataAccess;
using DraftTool.Models;
using DraftTool.UI.Event;
using DraftTool.UI.Wrapper;
using Prism.Events;
using System.Collections.Generic;

namespace DraftTool.UI.Service
{
    class CubeService : DBServiceBase, ICubeService
    {
        private ICubeRepo _repo;
        public CubeService(IEventAggregator eventAggregator, ICubeRepo repo) : base(eventAggregator)
        {
            _repo = repo;

            _eventAggregator.GetEvent<AddCubeEvent>().Subscribe(OnAddCube);
            _eventAggregator.GetEvent<DeleteCubeEvent>().Subscribe(OnDeleteCube);
        }

        private void OnAddCube(AddCubeEventArgs args)
        {
            _repo.AddCube(args.Cube.Model);
            foreach(string card in args.Cube.CardNames)
            {
                _repo.AddCard(card, args.Cube.Name);
            }
        }

        private void OnDeleteCube(DeleteCubeEventArgs args)
        {
            _repo.DeleteCube(args.Cube.Name);
            foreach (string card in args.Cube.CardNames)
            {
                _repo.DeleteCard(card, args.Cube.Name);
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
