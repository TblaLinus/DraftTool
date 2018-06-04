using DraftTool.DataAccess;
using DraftTool.Models;
using DraftTool.UI.Wrapper;
using Prism.Events;
using System.Collections.Generic;

namespace DraftTool.UI.Service
{
    //Sköter kommunikation mellan SetRepo och resten av programmet
    class SetService : DBServiceBase, ISetService
    {
        private ISetRepo _repo;

        //Alla set i databasen
        public List<SetWrapper> Sets { get; }

        public SetService(IEventAggregator eventAggregator, ISetRepo repo) : base(eventAggregator)
        {
            _repo = repo;

            Sets = new List<SetWrapper>();
            foreach (Set DBset in _repo.GetSets())
            {
                SetWrapper set = new SetWrapper(DBset);
                set.IsUsed = false;
                Sets.Add(set);
            }
        }

        public void AddSetToDB(SetWrapper set)
        {
            _repo.AddSet(set.Model);
        }

        public void DeleteSetFromDB(SetWrapper set)
        {
            _repo.DeleteSet(set.Name);
        }
    }
}
