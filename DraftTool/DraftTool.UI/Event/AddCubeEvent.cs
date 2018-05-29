using DraftTool.Models;
using DraftTool.UI.Wrapper;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.UI.Event
{
    public class AddCubeEvent :PubSubEvent<AddCubeEventArgs>
    {
    }

    public class AddCubeEventArgs
    {
        public CubeWrapper Cube { get; set; }
    }
}
