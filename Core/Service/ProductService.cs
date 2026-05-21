using AutoMapper;
using Domain.Contracts;
using Domain.Entities.ProductModule;
using Domain.Exceptions;
using Service.Specifications;
using Services.Abstractions;
using Shared;
using Shared.DataTransferObject.ProductModules;

namespace Service
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper, IFileService _fileService) : IProductService
    {
        public async Task<PaginationResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var specification = new ProductWithTypeAndBrandSpecification(queryParams);
            // Suger syntax:
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(specification);
            var productDtos = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
            var productCount = productDtos.Count();
            var totalCount = await _unitOfWork.GetRepository<Product, int>().CountAsync(new ProductCountSpecification(queryParams));
            return new PaginationResult<ProductDto>(queryParams.pageIndex, productCount, totalCount , productDtos);
           
           
        }
        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var specification = new ProductWithTypeAndBrandSpecification(id);
            // Suger syntax:
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(specification);
            if (product is null)
                throw new ProductNotFoundException(id);
            return _mapper.Map<Product, ProductDto>(product);
        }
        public async Task<ProductDto> CreateProductAsync(CreateOrUpdateProductDto productDto)
        {
            // 1. Map the incoming DTO to the Product Entity
            var product = _mapper.Map<CreateOrUpdateProductDto, Product>(productDto);

            // 2. Handle image upload if a file is provided
            if (productDto.Image is not null)
            {
                // Use await to handle the asynchronous file upload
               var PictureUrl = await _fileService.UploadFileAsync(productDto.Image, "products") ?? string.Empty;
                product.PictureUrl = PictureUrl;
            }

            // 3. Add the product to the tracking context
            await _unitOfWork.GetRepository<Product, int>().AddAsync(product);

            // 4. Persist changes to the database
            await _unitOfWork.SaveChangesAsync();
           
            return await GetProductByIdAsync(product.Id);
        }
        public async Task<ProductDto> UpdateProductAsync(int id, CreateOrUpdateProductDto productDto)
        {
            // 1. Retrieve the existing product using a specification
            var spec = new ProductWithTypeAndBrandSpecification(id);
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(spec);

            // Throw an exception if the product is not found
            if (product is null)
                throw new ProductNotFoundException(id);

            // 2. Handle image update logic
            if (productDto.Image is not null)
            {
                // Delete the old image file from the server if it exists
                if (!string.IsNullOrEmpty(product.PictureUrl))
                {
                    _fileService.DeleteFile(product.PictureUrl);
                }

                // Upload the new image and update the PictureUrl property
                product.PictureUrl = await _fileService.UploadFileAsync(productDto.Image, "products") ?? string.Empty;
            }

            // 3. Map the updated DTO values onto the existing product entity
            _mapper.Map(productDto, product);

            // 4. Save the modifications to the database
            await _unitOfWork.SaveChangesAsync();

            // 5. Return the updated product details by Id
            return await GetProductByIdAsync(id);
        }
        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(id);

            if (product is null) return false;

            // 1. Delete Image
            if (!string.IsNullOrEmpty(product.PictureUrl))
            {
                _fileService.DeleteFile(product.PictureUrl); 
            }
            // 2. Delete Product
            _unitOfWork.GetRepository<Product, int>().Delete(product);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
