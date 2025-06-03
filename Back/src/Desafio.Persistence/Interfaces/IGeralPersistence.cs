using System.Threading.Tasks;

namespace Desafio.Persistence.Interfaces
{
    public interface IGeralPersistence
    {
        // Adiciona uma nova entidade ao contexto para inserção no banco
        void Add<T>(T entity) where T : class;

        // Marca uma entidade existente no contexto como modificada para atualização
        void Update<T>(T entity) where T : class;

        // Marca uma entidade para remoção do banco de dados
        void Delete<T>(T entity) where T : class;

        // Marca várias entidades para remoção do banco de dados
        void DeleteRange<T>(T[] entity) where T : class;

        // Persiste todas as mudanças feitas no contexto no banco de dados
        // Retorna true se ao menos uma alteração foi salva
        Task<bool> SaveChangesAsync();
    }
}
