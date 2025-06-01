using System;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Domain;
using Desafio.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Persistence
{
    public class DesafioPersistence : IDesafioPersistence
    {

        private readonly DesafioContext _context;
        public DesafioPersistence(DesafioContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
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

        public async Task<Telemetry[]> GetAllTelemetriesAsync()
        {
            IQueryable<Telemetry> query = _context.Telemetries;
            query = query.OrderBy(e => e.Id);
            return await query.ToArrayAsync();
        }

        public async Task<Telemetry[]> GetAllTelemetriesByStatusAsync(MachineStatus status)
        {
            IQueryable<Telemetry> query = _context.Telemetries;
            query = query.OrderBy(e => e.Id).
            Where(e => e.Status == status);
            return await query.ToArrayAsync();
        }

        public async Task<Telemetry> GetAllTelemetryByIdAsync(int id)
        {
            IQueryable<Telemetry> query = _context.Telemetries;
            query = query.OrderBy(e => e.Id).
            Where(e => e.Id == id);
            return await query.FirstOrDefaultAsync();
        }
    }
}
