using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductRepository : IProductRepository
    {
        _214346710DbContext productContext = new _214346710DbContext();
        public ProductRepository(_214346710DbContext productContext)
        {
            this.productContext = productContext;

        }



        public async Task<List<Product>> Get(string? descreption, int? min, int? max, string? name, int?[] categoryIds, int position, int skip)
        {
            var query = productContext.Products.Include(p => p.Category).Where(product =>
            (name == null ? (true) : (product.Name.Contains(name))) &&
            (descreption == null ? (true) : (product.Description.Contains(descreption)))
            && ((min == null) ? (true) : (product.Price >= min))
            && ((max == null) ? (true) : (product.Price <= max))
            && ((categoryIds.Length == 0) ? (true) : (categoryIds.Contains((int)product.CategoryId))))
            .OrderBy(product => product.Price);
            Console.WriteLine(query.ToQueryString());
            List<Product> products = await query.ToListAsync();
            return products;
        }

        public async Task<List<Product>> Get()
        {
            return await productContext.Products.ToListAsync();
        }
    }
}
