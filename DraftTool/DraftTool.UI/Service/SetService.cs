using DraftTool.DataAccess;
using DraftTool.Models;
using DraftTool.UI.Wrapper;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.UI.Service
{
    class SetService : DBServiceBase, ISetService
    {
        private ISetRepo _repo;

        public ObservableCollection<SetWrapper> Sets { get; }

        public SetService(IEventAggregator eventAggregator, ISetRepo repo) : base(eventAggregator)
        {
            _repo = repo;

            Sets = new ObservableCollection<SetWrapper>();
            foreach (Set DBset in _repo.GetSets())
            {
                SetWrapper set = new SetWrapper(DBset);
                set.IsUsed = false;
                Sets.Add(set);
            }
        }
    }
}
