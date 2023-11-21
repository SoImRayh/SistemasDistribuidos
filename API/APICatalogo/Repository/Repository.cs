using APICatalogo.Context;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repository;

//where T : class especifica que o tipo generico T só pode receber classes como implementacão
public class Repository<T> : IRepository<T> where T : class
{
    protected ApiCatalogoDbContextOld _context;

    public Repository(ApiCatalogoDbContextOld context)
    {
        _context = context;
    }

    //IQueryable é um filtro que irá ser passado para o banco de dados filtrar e retornar os resultados, IEnumerable irá aplicar o filtro na memoria depois de buscar todos os registros
    public IQueryable<T> Get()
    {
        //Set<T> de _context retorna uma instancia DbSet<T> para acessar entidades do tipo generico recebido
        /* Ao adicionar o metodo AsNoTracking(), o EntityFramework para de 
        * Monitorar o estado dos objetos, ou seja, a consulta fica mais leve.
        * Porém, o _context perde informações de atualização nestes objetos,
        * portanto esta otimização só pode ser feita em metodos que não irão
        * atualizar estes dados no banco de dados (perfeito para os verbos GET)*/
        return _context.Set<T>().AsNoTracking();
    }

    public T GetById(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
    {
        //Set<T> de _context retorna uma instancia DbSet<T> para acessar entidades do tipo generico recebido
        return _context.Set<T>().AsNoTracking().SingleOrDefault(predicate);
    }

    public void Add(T entity)
    {
        //Set<T> de _context retorna uma instancia DbSet<T> para acessar entidades do tipo generico recebido
        _context.Set<T>().Add(entity);
    }

    public void Delete(T entity)
    {
        //Set<T> de _context retorna uma instancia DbSet<T> para acessar entidades do tipo generico recebido
        _context.Set<T>().Remove(entity);
    }

    public void Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        //Set<T> de _context retorna uma instancia DbSet<T> para acessar entidades do tipo generico recebido
        _context.Set<T>().Update(entity);
    }
}
