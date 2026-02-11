using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoLayerArchitecture.DataAccessLayer.Domain;
using TwoLayerArchitecture.PresentationLayer.Interface;

namespace TwoLayerArchitecture.PresentationLayer.Concret
{
    public class UserConsole : IUserView
    {
        public void DisplayError(string message)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(message, nameof(message));
            Console.WriteLine(message);
        }

        public void DisplaySuccess(string message)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(message, nameof(message));
            Console.WriteLine(message);
        }

        public void DisplayUsers(IEnumerable<User> users)
        {
            foreach (User user in users)
            {
                Console.WriteLine(user.Name);
            }
        }
    }
}
