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

        // Injeção do contexto do banco via construtor
        public MachinePersistence(DesafioContext context)
        {
            _context = context;
        }

        // Retorna todas as máquinas, incluindo suas telemetrias associadas,
        // ordenadas pelo Id da máquina
        public async Task<Machine[]> GetAllMachinesAsync()
        {
            IQueryable<Machine> query = _context.Machines
                .Include(e => e.Telemetries); // Carrega também as telemetrias relacionadas
            query = query.OrderBy(e => e.Id);
            return await query.ToArrayAsync();
        }

        // Retorna todas as máquinas que estejam com um status específico,
        // incluindo suas telemetrias associadas, ordenadas pelo Id
        public async Task<Machine[]> GetAllMachinesByStatusAsync(MachineStatus status)
        {
            IQueryable<Machine> query = _context.Machines
                .Include(e => e.Telemetries)
                .Where(e => e.Status == status) // Filtra pelo status da máquina
                .OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        // Retorna uma máquina pelo seu Id, incluindo as telemetrias associadas
        public async Task<Machine> GetMachineByIdAsync(Guid id)
        {
            IQueryable<Machine> query = _context.Machines
                .Include(e => e.Telemetries)
                .Where(e => e.Id == id)
                .OrderBy(e => e.Id);

            return await query.FirstOrDefaultAsync(); // Retorna a primeira máquina que bate com o Id, ou null se não achar
        }       
    }
}
