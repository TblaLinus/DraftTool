using DraftTool.UI.Wrapper;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.UI.Event
{
    class DeleteCubeEvent : PubSubEvent<DeleteCubeEventArgs>
    {
    }

    public class DeleteCubeEventArgs
    {
        public CubeWrapper Cube { get; set; }
    }
}
