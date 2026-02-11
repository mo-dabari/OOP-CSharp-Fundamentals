using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoLayerArchitecture.DataAccessLayer.Domain;

namespace TwoLayerArchitecture.PresentationLayer.Interface
{
    public interface IUserView
    {
        void DisplaySuccess(string message);
        void DisplayError(string message);
        void DisplayUsers(IEnumerable<User> users);
    }
}
