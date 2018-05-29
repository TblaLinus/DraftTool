using DraftTool.DataAccess;
using DraftTool.Models;
using DraftTool.UI.Event;
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
    public class DBServiceBase
    {
        protected IEventAggregator _eventAggregator;

        public DBServiceBase(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }
    }
}
