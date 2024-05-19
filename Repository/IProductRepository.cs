using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IProductRepository
    {

        Task<List<Product>> GetAllProducts();

        Task<List<Product>> GetProductsByCondition(string? descreption, int? min, int? max, string? name, int?[] categoryIds, int position=20, int skip=1);
    }
}
