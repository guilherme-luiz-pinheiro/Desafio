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

        // Injeção do contexto do banco via construtor
        public TelemetryPersistence(DesafioContext context)
        {
            _context = context;
        }

        // Retorna todas as telemetrias, ordenadas pelo Id
        public async Task<Telemetry[]> GetAllTelemetriesAsync()
        {
            IQueryable<Telemetry> query = _context.Telemetries;
            query = query.OrderBy(e => e.Id);
            return await query.ToArrayAsync();
        }

        // Retorna todas as telemetrias filtradas pelo status da máquina,
        // ordenadas pelo Id
        public async Task<Telemetry[]> GetAllTelemetriesByStatusAsync(MachineStatus status)
        {
            IQueryable<Telemetry> query = _context.Telemetries;
            query = query.OrderBy(e => e.Id)
                         .Where(e => e.Status == status); // Filtro por status
            return await query.ToArrayAsync();
        }

        // Retorna uma telemetria pelo seu Id
        public async Task<Telemetry> GetAllTelemetryByIdAsync(int id)
        {
            IQueryable<Telemetry> query = _context.Telemetries;
            query = query.OrderBy(e => e.Id)
                         .Where(e => e.Id == id);
            return await query.FirstOrDefaultAsync(); // Retorna a telemetria encontrada ou null
        }
    }
}
