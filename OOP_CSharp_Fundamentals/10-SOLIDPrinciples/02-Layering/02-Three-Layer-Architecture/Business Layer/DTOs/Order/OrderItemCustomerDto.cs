using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeLayerArchitecture.BusinessLayer.DTOs
{
    public record OrderItemCustomerDto
    (
        string ProductName,
        decimal UnitPrice,
        int Quantity,
        decimal Total
    );
}
