using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeLayerArchitecture.PresentationLayer.Common.Interface;
using System.Text.Json;

namespace ThreeLayerArchitecture.PresentationLayer.Common.Concret
{
    public class ActionOKResult<T> : IActionResult
    {
        private readonly T? _data;
        public int StatusCode => 200;

        public ActionOKResult(T? data)
        {
            _data = data;
        }

        public void Execute()
        {
            Console.WriteLine($"Status: {StatusCode} | Data: {JsonSerializer.Serialize(_data)}");
        }

    }
}
