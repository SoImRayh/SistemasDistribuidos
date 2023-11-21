using APICatalogo.Context;
using APICatalogo.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ApiCatalogoDbSession _dbSession;

        public CategoriaRepository(ApiCatalogoDbSession dbSession)
        {
            _dbSession = dbSession;
        }

        public async Task<int> AddAsync(Categoria entity)
        {
            using (var conn = _dbSession.Connection)
            {
                string insertQuery = @"INSERT INTO categoria VALUES (@CategoriaId, @Nome, @ImagemUrl)";

                var result = await conn.ExecuteAsync(sql: insertQuery, param: entity);
                return result;
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var conn = _dbSession.Connection)
            {
                string deleteSql = $"DELETE FROM categoria WHERE categoriaid = { id }";

                await conn.ExecuteAsync(sql: deleteSql);
            }
        }

        public async Task<IEnumerable<Categoria>> GetAsync()
        {
            using (var conn = _dbSession.Connection)
            {
                string query = "SELECT * FROM categoria;";

                var categorias = (await conn.QueryAsync<Categoria>(sql: query)).ToList();
                return categorias;
            }

        }

        public async Task<Categoria> GetByIdAsync(int id)
        {
            using (var conn = _dbSession.Connection)
            {
                string query = $"SELECT * FROM categoria WHERE categoriaid = { id }";

                return (await conn.QueryAsync<Categoria>(sql: query)).First();
            }
        }
    }
}
