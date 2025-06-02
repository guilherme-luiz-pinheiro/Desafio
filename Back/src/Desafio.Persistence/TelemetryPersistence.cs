using System.Linq;
using System.Threading.Tasks;
using Desafio.Domain;
using Desafio.Domain.Enums;
using Desafio.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Desafio.Persistence.Context;

namespace Desafio.Persistence
{
    public class TelemetryPersistence : ITelemetryPersistence
    {

        private readonly DesafioContext _context;
        public TelemetryPersistence(DesafioContext context)
        {
            _context = context;
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
