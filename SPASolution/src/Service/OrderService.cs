using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.DTOs;
using Persistence.Database;
using Service.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IOrderService {
        Task<OrderDTO> Create(OrderCreateDTO model);
        Task<OrderDTO>Get(int id);
        Task<DataCollection<OrderDTO>> GetAll(int page, int take);
        Task Remove(int id);
    }

    public class OrderService:IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public OrderService(ApplicationDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderDTO> Create(OrderCreateDTO model)
        {
            var entry = _mapper.Map<Order>(model);

            //complete details
            foreach (var item in entry.items)
            {
                item.Total = item.UnitPrice * item.Quantity;
                item.Iva = item.Total * 0.13m;
                item.SubTotal = item.Total - item.Iva;
            }

            //complete order
            entry.Total = entry.items.Sum(x => x.Total);
            entry.Subtotal = entry.items.Sum(x => x.SubTotal);
            entry.Iva = entry.items.Sum(x => x.Iva);


            await _context.Orders.AddAsync(entry);
            await _context.SaveChangesAsync();

            return _mapper.Map<OrderDTO>(await _context.Orders
                .Include(x => x.Client)
                .Include(x => x.items)
                    .ThenInclude(x => x.Product)
                .SingleAsync(x => x.OrderId == entry.OrderId));
        }

        public async Task<OrderDTO> Get(int id)
        {
            return  _mapper.Map<OrderDTO>(await _context.Orders
                .Include(x=>x.Client)
                .Include(x=>x.items)
                    .ThenInclude(x=>x.Product)
                .SingleAsync(x => x.OrderId == id));
        }

        public async Task<DataCollection<OrderDTO>> GetAll(int page, int take)
        {

            return _mapper.Map<DataCollection<OrderDTO>>(
                await _context.Orders
                .Include(x=>x.Client)
                .Include(x=>x.items)
                .ThenInclude(x=>x.Product)
                .OrderByDescending(x => x.ClientId)
                .AsQueryable()
                .PagedAsync(page, take)
            );
        }

        public async Task Remove(int id)
        {
            _context.Orders.Remove(new Order { OrderId = id });

            await _context.SaveChangesAsync();
        }
    }
}
