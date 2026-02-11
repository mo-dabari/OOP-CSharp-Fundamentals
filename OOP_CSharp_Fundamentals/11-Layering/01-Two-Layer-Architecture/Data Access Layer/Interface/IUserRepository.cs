using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using TwoLayerArchitecture.DataAccessLayer.Domain;

namespace TwoLayerArchitecture.DataAccessLayer.Interface
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetById(string name);
        bool Add(User user);
        bool Update(User user);
        bool Delete(string name);
    }
}
