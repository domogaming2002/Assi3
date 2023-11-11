using DTO.DTO.Request.Category;
using DTO.DTO.Response.Category;

namespace Repository.IRepository
{
    public interface ICategoryRepository
    {
        bool AddCategory(CategoryAddDTO c);
        CategoryResponseDTO? GetCategoryById(int id);
        bool DeleteCategory(CategoryUpdateDTO c);
        bool UpdateCategory(CategoryUpdateDTO c);
        List<CategoryResponseDTO> GetCategories();
    }
}
