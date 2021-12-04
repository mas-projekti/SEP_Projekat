using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Models.Configuration;
using WebShop.Models.DomainModels;
using WebShop.Models.Enums;
using WebShop.Repository.Contract.Interfaces;
using WebShop.Service.Contract.Dto;
using WebShop.Service.Contract.Services;

namespace WebShop.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto> InsertProduct(ProductDto newProduct)
        {
            ValidateProduct(newProduct);

            newProduct.Id = 0;

            Product product = await _productRepository.Add(_mapper.Map<Product>(newProduct));

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            Product product = await _productRepository.Get(productId);

            if (product == null)
                throw new Exception($"Product with {productId} does not exist!");

            var condition = await _productRepository.Delete(productId);

            return condition != null ? true : false;

        }

        public async Task<IEnumerable<ProductDto>> GetAllProducts() => _mapper.Map<IEnumerable<ProductDto>>(await _productRepository.GetAll());
      
        public async Task<ProductDto> GetProductById(int productId) => _mapper.Map<ProductDto>(await _productRepository.Get(productId));
       
        public async Task<ProductDto> UpdateProduct(ProductDto product)
        {
            ValidateProduct(product);

            Product updatedProduct = _mapper.Map<Product>(product);
            Product oldProduct = await _productRepository.Get(product.Id); 

            if (oldProduct == null)
                throw new Exception($"Product with Id = {updatedProduct.Id} does not exists!");

            return _mapper.Map<ProductDto>(await _productRepository.Update(updatedProduct.Id, updatedProduct));
        }

        public async Task<ProductListDto> GetProductsPaged(ProductField sortBy, SortingDirection direction, int page, int perPage)
        {
            IEnumerable<Product> productsPaged = await _productRepository.GetAll();
            productsPaged = SortProducts(productsPaged, sortBy, direction);
 

            int resourceCount = productsPaged.Count();
            productsPaged = productsPaged.Skip(page * perPage)
                                         .Take(perPage);

            ProductListDto returnValue = new ProductListDto()
            {
                Products = _mapper.Map<List<ProductDto>>(productsPaged),
                TotalCount = resourceCount
            };

            return returnValue;
        }

        private IEnumerable<Product> SortProducts(IEnumerable<Product> products, ProductField sortBy, SortingDirection direction)
        {

            if (direction == SortingDirection.Asc)
            {
                switch (sortBy)
                {
                    case ProductField.Id:
                        return products.OrderBy(x => x.Id);
                    case ProductField.Manufacturer:
                        return products.OrderBy(x => x.Manufacturer);
                    case ProductField.Model:
                        return products.OrderBy(x => x.Model);
                    case ProductField.Price:
                        return products.OrderBy(x => x.Price);
                    case ProductField.Warrany:
                        return products.OrderBy(x => x.Warranty);

                }

            }
            else
            {
                switch (sortBy)
                {
                    case ProductField.Id:
                        return products.OrderByDescending(x => x.Id);
                    case ProductField.Manufacturer:
                        return products.OrderByDescending(x => x.Manufacturer);
                    case ProductField.Model:
                        return products.OrderByDescending(x => x.Model);
                    case ProductField.Price:
                        return products.OrderByDescending(x => x.Price);
                    case ProductField.Warrany:
                        return products.OrderByDescending(x => x.Warranty);

                }

            }

            return products;
        }

        #region Validations

        private void ValidateProduct(ProductDto product)
        {
            if (product.Description.Length > Constants.DescriptionLength)
                throw new Exception($"Max description length is {Constants.DescriptionLength}.");

            if (String.IsNullOrEmpty(product.ImageURL))
                throw new Exception($"Product without image attached!");

            if (String.IsNullOrEmpty(product.Manufacturer))
                throw new Exception($"Product manufacturer can not be empty!");

            if (String.IsNullOrEmpty(product.Model))
                throw new Exception($"Product model can not be empty!");

            if (product.Price <= 0)
                throw new Exception("$Product price should be greather than 0!");

            if(product.Warranty <= 0)
                throw new Exception("$Product warranty should be greather than 0!");

            if (!Enum.IsDefined(typeof(CategoryType), product.CategoryType))
                throw new Exception("Undefined device type!");


        }

       
        #endregion

    }
}
