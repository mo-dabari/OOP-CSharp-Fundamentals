using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeLayerArchitecture.BusinessLayer.Domains;
using ThreeLayerArchitecture.BusinessLayer.Entities.Enums;
using ThreeLayerArchitecture.BusinessLayer.Enums;
using ThreeLayerArchitecture.BusinessLayer.Interfaces;

namespace ThreeLayerArchitecture.BusinessLayer.Services
{
    public class PricingService : IPricingService
    {
        public decimal CalculateDiscount(decimal Subtotal, Customer customer)
        {
            decimal discount = 0;

            // Business Rule 1: Customer Type Discount
            switch (customer.CustomerType)
            {
                case enCustomerType.Regular:
                    discount += Subtotal * 0.05m; // 5%
                    break;
                case enCustomerType.Premium:
                    discount += Subtotal * 0.10m; // 10%
                    break;
                case enCustomerType.VIP:
                    discount += Subtotal * 0.15m; // 15%
                    break;
            }

            // Business Rule 2: Loyalty Discount
            var membershipYears = (DateTime.UtcNow - customer.MemberSince).Days / 365;

            if (membershipYears >= 5)
                discount += Subtotal * 0.03m; // 3% for 5+ years

            // Business Rule 3: Order Amount Discount
            if (Subtotal >= 1000)
                discount += Subtotal * 0.15m; // 15% for orders > $1000

            else if (Subtotal >= 500)
                discount += Subtotal * 0.10m; // 10% for orders > $500

            // Business Rule 4: Maximum Discount Cap
            var maxDiscount = Subtotal * 0.30m; // Max 30%
            return Math.Min(discount, maxDiscount);
        }

        public decimal CalculateShipping(decimal Subtotal, enDestination destination)
        {
            // Business Rule: Free shipping for large orders
            if (Subtotal >= 500)
                return 0;

            // Business Rule: Shipping by destination
            return destination switch
            {
                enDestination.Local => 5.00m,
                enDestination.Regional => 15.00m,
                enDestination.International => 50.00m,
                _ => 10.00m
            };
        }

        public decimal CalculateTax(decimal Subtotal, enCountries countries)
        {
            decimal taxRate = countries switch
            {
                enCountries.Egypt => 0.07m,    // 7%
                enCountries.Iraq => 0.20m,    // 20% VAT
                enCountries.Qatar => 0.14m,    // 14%
                enCountries.SaudiArabia => 0.19m,    // 19%
                _ => 0.00m        // Default: no tax
            };

            return Subtotal * taxRate;
        }
    }
}
