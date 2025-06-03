using Desafio.Domain;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Persistence.Context
{
    // Classe que representa o contexto do Entity Framework para o banco de dados Desafio
    // Herda de DbContext para fornecer funcionalidades de acesso e manipulação dos dados
    public class DesafioContext : DbContext
    {
        // Representa a tabela Machines no banco de dados
        public DbSet<Machine> Machines { get; set; }

        // Representa a tabela Telemetries no banco de dados
        public DbSet<Telemetry> Telemetries { get; set; }

        // Construtor que recebe as opções de configuração do contexto (como string de conexão)
        public DesafioContext(DbContextOptions<DesafioContext> options) : base(options)
        {
            // Construtor da classe base DbContext é chamado para configurar o contexto
        }

        // Caso queira customizar o modelo de dados, como configurar relacionamentos,
        // poderá sobrescrever o método OnModelCreating aqui
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     base.OnModelCreating(modelBuilder);
        //     // Configurações adicionais aqui
        // }
    }
}
