using Entities;
using System.Collections.Specialized;

namespace Service
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategories();
    }
}
