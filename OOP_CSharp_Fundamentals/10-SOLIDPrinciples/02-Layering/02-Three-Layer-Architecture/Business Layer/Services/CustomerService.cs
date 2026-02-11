using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeLayerArchitecture.BusinessLayer.DTOs;
using ThreeLayerArchitecture.BusinessLayer.Interfaces;
using ThreeLayerArchitecture.BusinessLayer.Result;
using ThreeLayerArchitecture.DataAccessLayer.Interfaces;

namespace ThreeLayerArchitecture.BusinessLayer.Services
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository _repo;
        CustomerService(ICustomerRepository repo)
        {
            _repo = repo;
        }
        public ResultService<CustomerProfileDto> CreateCustomer(CustomerProfileDto customer)
        {
            throw new NotImplementedException();
        }

        public ResultService DeleteCustomer(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public ResultService<IReadOnlyList<CustomerProfileDto>> GetAllCustomers()
        {
            throw new NotImplementedException();
        }

        public ResultService<CustomerProfileDto> GetCustomerById(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public ResultService UpdateCustomer(Guid customerId)
        {
            throw new NotImplementedException();
        }
    }
}
