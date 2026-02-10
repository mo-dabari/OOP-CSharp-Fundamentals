using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeLayerArchitecture.BusinessLayer.Enums;

namespace ThreeLayerArchitecture.BusinessLayer.DTOs
{
    public record CustomerProfileDto(string FullName, string Email);
}
