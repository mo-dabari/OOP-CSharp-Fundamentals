using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeLayerArchitecture.BusinessLayer.DTOs;
using ThreeLayerArchitecture.BusinessLayer.Interfaces;
using ThreeLayerArchitecture.BusinessLayer.Result;
using ThreeLayerArchitecture.BusinessLayer.Result.Enum;
using ThreeLayerArchitecture.PresentationLayer.Common.Concret;
using ThreeLayerArchitecture.PresentationLayer.Common.Interface;

namespace ThreeLayerArchitecture.PresentationLayer
{
    public class CustomerController
    {
        private readonly ICustomerService _logic;

        public CustomerController(ICustomerService logic)
        {
            _logic = logic;
        }

        public IActionResult GetCustomerById(Guid customerId)
        {
            ResultService<CustomerProfileDto> result = _logic.GetCustomerById(customerId);

            return HandleRequest(result);
        }

        public IActionResult GetAllCustomers()
        {
            ResultService<IReadOnlyList<CustomerProfileDto>> result = _logic.GetAllCustomers();

            return HandleRequest(result);
        }

        public IActionResult UpdateCustomer(Guid customerId)
        {
            ResultService result = _logic.UpdateCustomer(customerId);

            return HandleRequest(result);
        }

        public IActionResult DeleteCustomer(Guid customerId)
        {
            ResultService result = _logic.DeleteCustomer(customerId);

            return HandleRequest(result);
        }

        private IActionResult HandleRequest<T>(ResultService<T> result)
        {
            return result.Status switch
            {
                enResultStatus.Success => new ActionOKResult<T>(result.Data),
                enResultStatus.NotFound => new ActionNotFoundResult("Not Found"),
                enResultStatus.Failure => new ActionBadRequestResult(result.ErrorMessage),
                _ => throw new Exception("Unknown result")
            };

        }
        private IActionResult HandleRequest(ResultService result)
        {
            return result.Status switch
            {
                enResultStatus.Success => new ActionOKResult<object>(null),
                enResultStatus.NotFound => new ActionNotFoundResult("Not Found"),
                enResultStatus.Failure => new ActionBadRequestResult(result.ErrorMessage),
                _ => throw new Exception("Unknown result")
            };

        }
    }
}
