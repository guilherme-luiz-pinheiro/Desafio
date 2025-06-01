using Desafio.Domain;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Persistence
{

    public class DesafioContext : DbContext
    {
        public DbSet<Machine> Machines { get; set; }
        public DbSet<Telemetry> Telemetries { get; set; }

        public DesafioContext(DbContextOptions<DesafioContext> options) : base(options)
        {

        }
    }
}