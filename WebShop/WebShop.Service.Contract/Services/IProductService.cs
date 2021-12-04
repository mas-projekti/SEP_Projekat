using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebShop.Models.Enums;
using WebShop.Service.Contract.Dto;

namespace WebShop.Service.Contract.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProducts();
        Task<ProductDto> GetProductById(int productId);
        Task<ProductDto> InsertProduct(ProductDto newProduct);
        Task<ProductDto> UpdateProduct(ProductDto product);
        Task<bool> DeleteProduct(int productId);
        Task<ProductListDto> GetProductsPaged(ProductField sortBy, SortingDirection direction, int page, int perPage);
    }
}
