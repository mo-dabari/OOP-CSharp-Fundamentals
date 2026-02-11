using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeLayerArchitecture.BusinessLayer.Interfaces;

namespace ThreeLayerArchitecture.BusinessLayer.Services
{
    public class InventoryService : IInventoryService
    {
        public bool IsInStock(Guid productId, int quantity)
        {
            throw new NotImplementedException();
        }

        public void ReleaseStock(Guid productId, int quantity)
        {
            throw new NotImplementedException();
        }

        public bool ReserveStock(Guid productId, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}
