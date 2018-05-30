using DraftTool.UI.Wrapper;
using Prism.Events;

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
