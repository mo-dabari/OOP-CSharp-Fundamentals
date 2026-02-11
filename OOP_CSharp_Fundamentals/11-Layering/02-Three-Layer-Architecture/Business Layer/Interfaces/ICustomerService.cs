using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeLayerArchitecture.BusinessLayer.Domains;
using ThreeLayerArchitecture.BusinessLayer.DTOs;
using ThreeLayerArchitecture.BusinessLayer.Result;

namespace ThreeLayerArchitecture.BusinessLayer.Interfaces
{
    public interface ICustomerService
    {
        ResultService<CustomerProfileDto> CreateCustomer(CustomerProfileDto customer);

        ResultService<CustomerProfileDto> GetCustomerById(Guid customerId);

        ResultService<IReadOnlyList<CustomerProfileDto>> GetAllCustomers();

        ResultService UpdateCustomer(Guid customerId);
        ResultService DeleteCustomer(Guid customerId);
    }
}
