using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.ProductService.Dtos
{
    internal class ProductImageUrlResolver : IValueResolver<Product, ProductDetailsDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductImageUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductDetailsDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{_configuration["BaseUrl"]}{source.PictureUrl}";

            return null!;
        }
    }
}
