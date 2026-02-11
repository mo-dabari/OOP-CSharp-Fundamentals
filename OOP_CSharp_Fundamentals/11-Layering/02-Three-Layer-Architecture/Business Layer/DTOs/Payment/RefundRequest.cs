using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeLayerArchitecture.BusinessLayer.Services
{
    public class RefundRequest
    {
        public Guid PaymentTransactionId { get; init; }
        public decimal? Amount { get; init; }

        public RefundRequest(Guid paymentTransactionId, decimal? amount)
        {
            if (paymentTransactionId == Guid.Empty)
                throw new ArgumentException("payment Transaction Id cannot be empty");
            if (amount.HasValue)
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount.Value, nameof(amount));

            PaymentTransactionId = paymentTransactionId;
            Amount = amount;
        }
    }
}
