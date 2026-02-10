using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeLayerArchitecture.BusinessLayer.Domains;

namespace ThreeLayerArchitecture.DataAccessLayer.Interfaces
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAllCustomers();
        void Create(Customer customer);
        void Update(Customer customer);
        void Delete(Guid Id);
        Customer GetCustomerById(Guid Id);
    }
}
