using Model;
using Model.DTOs;
using Persistence.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IClientService {
        Task Create(ClientCreateDTO model);
    }

    public class ClientService:IClientService
    {
        private readonly ApplicationDbContext _context;
        public ClientService(ApplicationDbContext context) {
            _context = context;
        }

        public async Task Create(ClientCreateDTO model) {
            var entry = new Client
            {
                Name = model.Name
            };
            await _context.Clients.AddAsync(entry);
            await _context.SaveChangesAsync();
        }
    }
}
