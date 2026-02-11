using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeLayerArchitecture.PresentationLayer.Common.Interface;
using System.Text.Json;

namespace ThreeLayerArchitecture.PresentationLayer.Common.Concret
{
    public class ActionBadRequestResult : IActionResult
    {
        private readonly string? _message;
        public int StatusCode => 400;

        public ActionBadRequestResult(string? message)
        {
            _message = message;
        }

        public void Execute()
        {
            Console.WriteLine($"Status: {StatusCode} | Error: {_message}");
        }

    }
}
