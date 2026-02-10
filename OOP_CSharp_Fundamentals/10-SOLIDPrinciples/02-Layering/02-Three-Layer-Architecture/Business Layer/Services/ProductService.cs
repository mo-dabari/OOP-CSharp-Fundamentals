using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeLayerArchitecture.BusinessLayer.DTOs;
using ThreeLayerArchitecture.BusinessLayer.Interfaces;

namespace ThreeLayerArchitecture.BusinessLayer.Services
{
    public class ProductService : IProductService
    {
        public bool CreateProduct(ProductDetailsDto product)
        {
            throw new NotImplementedException();
        }

        public bool DeleteProduct(Guid ProductId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductDetailsDto> GetAllDetailsProducts()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductDto> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public bool UpdateProduct(Guid ProductId, ProductDetailsDto product)
        {
            throw new NotImplementedException();
        }

        public ProductDetailsDto GetProductById(Guid productId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductDto> GetProductsByCategory(ProductDto category)
        {
            throw new NotImplementedException();
        }

    }
}
