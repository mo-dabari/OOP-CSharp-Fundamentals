using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeLayerArchitecture.BusinessLayer.Interfaces;

namespace ThreeLayerArchitecture.BusinessLayer.Services
{
    public class PayPalService : IPaymentService
    {

        public bool ProcessPayment(PaymentRequest request)
        {
            throw new NotImplementedException();
        }

        public void RefundPayment(RefundRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
