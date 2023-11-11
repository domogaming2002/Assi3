using AutoMapper;
using BusinessObject.Models;
using DataAccess;
using DTO.DTO.Request.Category;
using DTO.DTO.Response.Category;
using Repository.IRepository;

namespace Repository.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        CategoryDAO _categoryDAO;
        IMapper _mapper;

        public CategoryRepository(MyDBContext context, IMapper mapper)
        {
            _categoryDAO = new CategoryDAO(context);
            _mapper = mapper;
        }

        public bool AddCategory(CategoryAddDTO c)
        {
            return _categoryDAO.Create(_mapper.Map<Category>(c));
        }

        public bool DeleteCategory(CategoryUpdateDTO c)
        {
            return _categoryDAO.Delete(_mapper.Map<Category>(c));
        }

        public List<CategoryResponseDTO> GetCategories()
        {
            return _mapper.Map<List<CategoryResponseDTO>>(_categoryDAO.GetCategories());
        }

        public CategoryResponseDTO? GetCategoryById(int id)
        {
            return _mapper.Map<CategoryResponseDTO>(_categoryDAO.GetById(id));
        }

        public bool UpdateCategory(CategoryUpdateDTO c)
        {
            return _categoryDAO.Update(_mapper.Map<Category>(c));
        }
    }
}
