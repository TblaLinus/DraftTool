using DraftTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.UI.Wrapper
{
    class CubeWrapper : WrapperBase<Cube>
    {
        public CubeWrapper(Cube model) : base(model)
        {
        }

        public string CardNames
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
    }
}
