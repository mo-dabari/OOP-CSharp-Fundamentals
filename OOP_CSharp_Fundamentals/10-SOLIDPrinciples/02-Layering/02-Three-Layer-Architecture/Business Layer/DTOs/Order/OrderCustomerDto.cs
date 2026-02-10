using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeLayerArchitecture.BusinessLayer.DTOs;

namespace ThreeLayerArchitecture.BusinessLayer.DTOs
{
    public record OrderCustomerDto
    (
        Guid OrderId,
        DateTime OrderDate,
        string Status,
        IReadOnlyList<OrderItemCustomerDto> Items
    );
}
