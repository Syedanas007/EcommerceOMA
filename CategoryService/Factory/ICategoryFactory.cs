using CategoryService.Models;

namespace CategoryService.Factory
{
    public interface ICategoryFactory
    {
        Category CreateCategory(string name, int orders);
    }
}
