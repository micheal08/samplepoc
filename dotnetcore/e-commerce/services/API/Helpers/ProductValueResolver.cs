using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class ProductValueResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _config;

        public ProductValueResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Product source, 
            ProductToReturnDto destination, 
            string destMember, 
            ResolutionContext context)
        {
            if(!string.IsNullOrWhiteSpace(source.PictureUrl))
            {
                return _config["ApiUrl"] + source.PictureUrl;
            }

            return null;
        }
    }
}
