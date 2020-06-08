using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.DTOs;
using Persistence.Database;
using Service.Commons;
using System.Linq;
using System.Threading.Tasks;

namespace Service
{
    public interface IProductService{
        Task<ProductDTO> Create(ProductCreateDTO model);
        Task Update(int id, ProductUpdateDTO model);
        Task Remove(int id);
        Task<ProductDTO> Get(int id);
        Task<DataCollection<ProductDTO>> GetAll(int page,int take);
    }
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(ApplicationDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ProductDTO> Create(ProductCreateDTO model)
        {
            var entry = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price
            };
            await _context.Products.AddAsync(entry);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductDTO>(entry);

        }
        public async Task Update(int id, ProductUpdateDTO model)
        {
            var entry = await _context.Products.SingleAsync(x => x.ProductId == id);
            entry.Description = model.Description;
            entry.Name = model.Name;
            entry.Price = model.Price;

            await _context.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            _context.Products.Remove(new Product { ProductId = id });

            await _context.SaveChangesAsync();
        }
        public async Task<ProductDTO> Get(int id)
        {
            return _mapper.Map<ProductDTO>(await _context.Products.SingleAsync(x => x.ProductId == id));
        }

        public async Task<DataCollection<ProductDTO>>GetAll(int page, int take){
            return _mapper.Map<DataCollection<ProductDTO>>(
                await _context.Products.OrderByDescending(x=>x.ProductId)
                .AsQueryable().PagedAsync(page,take)
                );
        }

        
    }
}
