using Entities;

namespace Repository
{
    public interface IProductRepositary
    {
        Task<List<Product>> Get(string? descreption, int? min, int? max, string? name, int?[] categoryIds, int position = 20, int skip = 1);
    }
}