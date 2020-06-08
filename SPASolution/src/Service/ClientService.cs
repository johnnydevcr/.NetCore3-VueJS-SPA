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
    public interface IClientService {
        Task<ClientDTO>Create(ClientCreateDTO model);
        Task Update(int id, ClientUpdateDTO model);
        Task Remove(int id);
        Task<ClientDTO>Get(int id);
        Task<DataCollection<ClientDTO>> GetAll(int page, int take);
    }

    public class ClientService:IClientService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ClientService(ApplicationDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClientDTO> Create(ClientCreateDTO model) {
            var entry = new Client
            {
                Name = model.Name
            };
            await _context.Clients.AddAsync(entry);
            await _context.SaveChangesAsync();

            return _mapper.Map<ClientDTO>(entry);
        }

        public async Task<ClientDTO> Get(int id)
        {
            return  _mapper.Map<ClientDTO>(await _context.Clients.SingleAsync(x => x.ClientId == id));
        }

        public async Task<DataCollection<ClientDTO>> GetAll(int page, int take)
        {

            return _mapper.Map<DataCollection<ClientDTO>>(
                await _context.Clients.OrderByDescending(x => x.ClientId)
                              .AsQueryable()
                              .PagedAsync(page, take)
            );
        }

        public async Task Update(int id, ClientUpdateDTO model)
        {
            var entry = await _context.Clients.SingleAsync(x => x.ClientId == id);
            entry.Name = model.Name;
            await _context.SaveChangesAsync();
        }
        public async Task Remove(int id)
        {
            _context.Clients.Remove(new Client { ClientId = id});

            await _context.SaveChangesAsync();
        }
    }
}
