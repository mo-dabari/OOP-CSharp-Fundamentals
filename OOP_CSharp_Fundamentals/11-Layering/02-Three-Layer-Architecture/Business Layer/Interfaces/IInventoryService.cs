using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeLayerArchitecture.BusinessLayer.Interfaces
{
    public interface IInventoryService
    {
        bool ReserveStock(Guid productId, int quantity);
        void ReleaseStock(Guid productId, int quantity);
        bool IsInStock(Guid productId, int quantity);
    }
}
