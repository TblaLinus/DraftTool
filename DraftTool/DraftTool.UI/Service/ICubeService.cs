using DraftTool.UI.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.UI.Service
{
    public interface ICubeService
    {
        List<CubeWrapper> GetCubes();
    }
}
