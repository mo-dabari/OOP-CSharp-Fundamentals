using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeLayerArchitecture.PresentationLayer.Common.Interface;
using System.Text.Json;

namespace ThreeLayerArchitecture.PresentationLayer.Common.Concret
{
    public class ActionNotFoundResult : IActionResult
    {
        private readonly string _message;
        public int StatusCode => 404;

        public ActionNotFoundResult(string message)
        {
            _message = message;
        }

        public void Execute()
        {
            Console.WriteLine($"Status: {StatusCode} | Error: {_message}");
        }

    }
}
