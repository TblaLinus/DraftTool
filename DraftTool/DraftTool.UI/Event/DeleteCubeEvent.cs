using DraftTool.UI.Wrapper;
using Prism.Events;

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
