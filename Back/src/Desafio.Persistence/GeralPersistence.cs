using Desafio.Persistence.Context;
using System.Threading.Tasks;
using Desafio.Persistence.Interfaces;

namespace Desafio.Persistence
{
    public class GeralPersistence : IGeralPersistence
    {
        private readonly DesafioContext _context;

        // Injeção do contexto do banco via construtor
        public GeralPersistence(DesafioContext context)
        {
            _context = context;
        }

        // Adiciona uma entidade ao contexto para futura inserção no banco
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        // Atualiza uma entidade existente no contexto para futura atualização no banco
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        // Remove uma entidade do contexto para futura exclusão no banco
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        // Remove um array de entidades do contexto para exclusão em lote no banco
        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }

        // Salva as alterações pendentes no contexto no banco de dados
        // Retorna true se alguma alteração foi salva, false caso contrário
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
