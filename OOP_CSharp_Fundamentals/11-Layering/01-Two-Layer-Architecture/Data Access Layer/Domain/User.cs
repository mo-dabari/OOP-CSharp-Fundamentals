using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoLayerArchitecture.DataAccessLayer.Domain
{
    public class User
    {
        public string Name { get; set; }

        public User(string name)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
            Name = name;
        }

    }
}
