

using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        _214346710DbContext categoryContext;
        public CategoryRepository(_214346710DbContext categoryContext)
        {
            this.categoryContext = categoryContext;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            var categories = await categoryContext.Categories.ToListAsync();
            if (categories != null)
            {
                return categories;
            }
            return null;
        }
}}


