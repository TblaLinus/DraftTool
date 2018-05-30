using DraftTool.UI.Wrapper;
using System.Collections.Generic;

namespace DraftTool.UI.Service
{
    public interface ICubeService
    {
        void AddCube(CubeWrapper cube);
        void DeleteCube(CubeWrapper cube);
        List<CubeWrapper> GetCubes();
    }
}
