using Model;
using Model.DTOs;
using Persistence.Database;
using System.Threading.Tasks;

namespace Service
{
    public interface IProductService{
        Task Create(ProductCreateDTO model); 
    }
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context) {
            _context = context;
        }
        public async Task Create(ProductCreateDTO model)
        {
            var entry = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price
            };
            await _context.Products.AddAsync(entry);
            await _context.SaveChangesAsync();
        }
    }
}
