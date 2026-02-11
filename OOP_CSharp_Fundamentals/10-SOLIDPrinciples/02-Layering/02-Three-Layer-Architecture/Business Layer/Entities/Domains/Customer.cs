using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OOP_CSharp_Fundamentals;
using ThreeLayerArchitecture.BusinessLayer.Enums;

namespace ThreeLayerArchitecture.BusinessLayer.Domains
{
    public class Customer
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public string Email { get; set; }
        public enCustomerType CustomerType { get; set; }
        public DateTime MemberSince { get; set; }

        public Customer(string name, string email, enCustomerType type)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
            ArgumentException.ThrowIfNullOrWhiteSpace(email, nameof(email));

            if (!IsValidEmail(email))
                throw new ArgumentException("Invalid email format.", nameof(email));

            if (!Enum.IsDefined(type))
                throw new ArgumentOutOfRangeException(nameof(type));

            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            CustomerType = type;
            MemberSince = DateTime.UtcNow;
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
