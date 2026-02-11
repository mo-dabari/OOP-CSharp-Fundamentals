using System;
using ThreeLayerArchitecture.BusinessLayer.Domains;
using ThreeLayerArchitecture.BusinessLayer.Entities.Enums;
using ThreeLayerArchitecture.BusinessLayer.Enums;

namespace ThreeLayerArchitecture.BusinessLayer.Interfaces
{
    public interface IPricingService
    {
        decimal CalculateDiscount(decimal Subtotal, Customer Customer);
        decimal CalculateTax(decimal Subtotal, enCountries countries);
        decimal CalculateShipping(decimal Subtotal, enDestination destination);
    }
}
