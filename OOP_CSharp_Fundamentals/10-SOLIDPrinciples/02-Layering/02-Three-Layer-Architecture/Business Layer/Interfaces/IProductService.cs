using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeLayerArchitecture.BusinessLayer.DTOs;

namespace ThreeLayerArchitecture.BusinessLayer.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductDetailsDto> GetAllDetailsProducts();
        IEnumerable<ProductDto> GetAllProducts();

        IEnumerable<ProductDto> GetProductsByCategory(ProductDto category);
        ProductDetailsDto GetProductById(Guid productId);

        bool CreateProduct(ProductDetailsDto product);
        bool UpdateProduct(Guid ProductId, ProductDetailsDto product);
        bool DeleteProduct(Guid ProductId);
    }
}
