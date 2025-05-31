using Desafio.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Desafio.API.Data
{

    public class DataContext : DbContext
    {
        public DbSet<Machine> Machine { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

    }
}