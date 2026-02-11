using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeLayerArchitecture.BusinessLayer.Result;

namespace ThreeLayerArchitecture.PresentationLayer.Common.Interface
{
    public interface IActionResult
    {
        int StatusCode { get; }
        void Execute();
    }
}
