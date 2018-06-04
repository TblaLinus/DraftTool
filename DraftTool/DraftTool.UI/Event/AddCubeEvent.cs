using DraftTool.UI.Wrapper;
using Prism.Events;

namespace DraftTool.UI.Event
{
    //Event som körs då man sparar en cube på CardList sidan
    public class AddCubeEvent :PubSubEvent<AddCubeEventArgs>
    {
    }

    public class AddCubeEventArgs
    {
        public CubeWrapper Cube { get; set; }
    }
}
