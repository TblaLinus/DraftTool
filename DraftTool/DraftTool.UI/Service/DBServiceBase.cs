using Prism.Events;

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
