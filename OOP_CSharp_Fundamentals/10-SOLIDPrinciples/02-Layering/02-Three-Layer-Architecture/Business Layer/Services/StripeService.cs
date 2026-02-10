using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeLayerArchitecture.BusinessLayer.Interfaces;

namespace ThreeLayerArchitecture.BusinessLayer.Services
{
    public class StripeService : IPaymentService
    {
        public bool ProcessPayment(PaymentRequest request)
        {
            Console.WriteLine($"[Stripe] Charging card:\nOrderID: {request.OrderId}\n Amount: {request.Amount}");
            // Stripe API Logic
            return true;
        }
        public void RefundPayment(RefundRequest request)
        {
            Console.WriteLine($"[Stripe] Refunding:\nTransaction ID: {request.PaymentTransactionId}\n{request.Amount}");
        }
    }
}
