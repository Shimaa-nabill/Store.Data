using AutoMapper;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Repository.Specification.ProductSpecs;
using Store.Repository.Specs.ProductSpecs;
using Store.Service.Helper;
using Store.Service.Services.ProductService.Dtos;
using System.Collections.Generic;

namespace Store.Service.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync();

            var mappedBrands = _mapper.Map<IReadOnlyList<BrandTypeDetailsDto>>(brands);
            return mappedBrands;
        }

        public async Task<PaginatedResultDto<ProductDetailsDto>> GetAllProductsAsync(ProductSpecifications input)
        {
            var specs = new ProductWithSpecifications(input);

            var products = await _unitOfWork.Repository<Product, int>().GetWithSpectificationAllAsync(specs);

            var countSpecs = new ProductWithCountSpecification(input);

            var count = await _unitOfWork.Repository<Product, int>().GetCountSpecificationsAsync(countSpecs);

            var mappedProducts = _mapper.Map<IReadOnlyList<ProductDetailsDto>>(products);
            return new PaginatedResultDto<ProductDetailsDto>(input.PageIndex, count, input.PageSize,mappedProducts);
        }

        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsync();

            IReadOnlyList<BrandTypeDetailsDto> mappedTypes = _mapper.Map<IReadOnlyList<BrandTypeDetailsDto>>(types);
            return mappedTypes;
        }

        public async Task<ProductDetailsDto> GetProductByIdAsync(int? id)
        {
            if (id is null)
                throw new Exception("Id Is Null");

            var specs = new ProductWithSpecifications(id);

            var product = await _unitOfWork.Repository<Product, int>().GetWithSpectificationAllAsync(specs);
            
            if (product is null)
                throw new Exception("Product Not Found");

            var mappedProduct = _mapper.Map<ProductDetailsDto>(product);
            return mappedProduct;
        }
    }
}
