using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InabilitytoChangeImplementation.GoodExample_Strategy;
using ThreeLayerArchitecture.BusinessLayer.Services;

namespace ThreeLayerArchitecture.BusinessLayer.Interfaces
{
    public interface IPaymentService
    {
        bool ProcessPayment(PaymentRequest request);
        void RefundPayment(RefundRequest request);
    }
}
