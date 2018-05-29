using DraftTool.DataAccess;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.UI.Service
{
    class CubeService : DBServiceBase, ICubeService
    {
        private ICubeRepo _repo;
        public CubeService(IEventAggregator eventAggregator, ICubeRepo repo) : base(eventAggregator)
        {
            _repo = repo;
        }
    }
}
