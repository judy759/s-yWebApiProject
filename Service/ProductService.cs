using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService : IProductService
    {
        IProductRepository productRepository;
        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task<List<Product>> Get(string? descreption, int? min, int? max, string? name, int?[] categoryIds, int position = 20, int skip = 1)
        {
            List<Product> resault = await productRepository.Get(descreption,min,max,name, categoryIds, position , skip );
            
            return resault;
        }

        public async Task<List<Product>> Get()
        {
           return await productRepository.Get();
        }
    }
}
