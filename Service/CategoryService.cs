using Entities;
using Repository;
using System.Collections.Specialized;

namespace Service
{
    public class CategoryService: ICategoryService
    {
        public readonly ICategoryRepository categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public async Task<List<Category>> GetAllCategories()
        {
            List<Category> res = await categoryRepository.GetAllCategories();
            if (res != null)
            {
                return res;
            }
            return null;
        }

    }
}
