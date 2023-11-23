using APICatalogo.Models;
using System.Linq.Expressions;

namespace APICatalogo.Repository
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> GetAsync();
        Task<Categoria> GetByIdAsync(int id);
        Task<int> AddAsync(Categoria entity);
        Task DeleteAsync(int id);
    }
}
