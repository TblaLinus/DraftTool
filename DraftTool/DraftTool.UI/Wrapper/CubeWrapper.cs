using DraftTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.UI.Wrapper
{
    public class CubeWrapper : WrapperBase<Cube>
    {
        public CubeWrapper(Cube model) : base(model)
        {
        }

        public string Name
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public List<string> CardNames
        {
            get { return GetValue<List<string>>(); }
            set { SetValue(value); }
        }
    }
}
