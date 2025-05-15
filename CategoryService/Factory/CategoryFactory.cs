using CategoryService.Models;

namespace CategoryService.Factory
{
    public class CategoryFactory : ICategoryFactory
    {
        public Category CreateCategory(string name, int orders)
        {
            return new Category
            {
                Id = Guid.NewGuid(),
                CategoryName = name,
                Orders = orders
            };
        }
    }
}
