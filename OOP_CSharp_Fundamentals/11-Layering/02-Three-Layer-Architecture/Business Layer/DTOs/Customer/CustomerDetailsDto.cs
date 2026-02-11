using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeLayerArchitecture.BusinessLayer.Enums;

namespace ThreeLayerArchitecture.BusinessLayer.DTOs
{
    public record CustomerDetailsDto(Guid Id, string FullName, string Email, string CustomerType, DateTime MemberSince);
}
