using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeLayerArchitecture.BusinessLayer.Services
{
    public record PaymentRequest
    {
        public Guid OrderId { get; init; }
        public decimal Amount { get; set; }

        public PaymentRequest(Guid orderId, decimal amount)
        {
            if (orderId == Guid.Empty)
                throw new ArgumentException("OrderId cannot be empty");

            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount, nameof(amount));

            OrderId = orderId;
            Amount = amount;
        }
    }
}
