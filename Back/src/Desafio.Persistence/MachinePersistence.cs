using System;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Domain;
using Desafio.Domain.Enums;
using Desafio.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Desafio.Persistence.Context;

namespace Desafio.Persistence
{
    public class MachinePersistence : IMachinePersistence
    {

        private readonly DesafioContext _context;
        public MachinePersistence(DesafioContext context)
        {
            _context = context;
        }

        public async Task<Machine[]> GetAllMachinesAsync()
        {
            IQueryable<Machine> query = _context.Machines.Include(e => e.Telemetries);
            query = query.OrderBy(e => e.Id);
            return await query.ToArrayAsync();
        }

        public async Task<Machine[]> GetAllMachinesByStatusAsync(MachineStatus status)
        {
            IQueryable<Machine> query = _context.Machines
                .Include(e => e.Telemetries)
                .Where(e => e.Status == status)
                .OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }


        public async Task<Machine> GetMachineByIdAsync(Guid id)
        {
            IQueryable<Machine> query = _context.Machines.Include(e => e.Telemetries);
            query = query.OrderBy(e => e.Id).
            Where(e => e.Id == id);
            return await query.FirstOrDefaultAsync();
        }       
    }
}
