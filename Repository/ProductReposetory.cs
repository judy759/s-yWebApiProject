

using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ProductRepository : IProductRepository
    {
        _214346710DbContext productContext;
        public ProductRepository(_214346710DbContext productContext)
        {
            this.productContext = productContext;
        }


        public async Task<List<Product>> GetAllProducts()
        {
            return await productContext.Products.ToListAsync();
        }

        public async Task<List<Product>> GetProductsByCondition(string? descreption, int? min, int? max, string? name, int?[] categoryIds, int position, int skip)
        {
            var query = productContext.Products.Include(p => p.Category).Where(product =>
          (descreption == null ? (true) : (product.Description.Contains(descreption)))
          && ((min == null) ? (true) : (product.Price >= min))
          && ((max == null) ? (true) : (product.Price <= max))
          && ((categoryIds.Length == 0) ? (true) : (categoryIds.Contains((int)product.CategoryId))))
          .OrderBy(product => product.Price);
            Console.WriteLine(query.ToQueryString());
            List<Product> products = await query.ToListAsync();
            return products;
        }
    }
}
