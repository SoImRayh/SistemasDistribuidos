using APICatalogo.Context;

namespace APICatalogo.Repository
{
    public class UnityOfWork : IUnityOfWork
    {
        private ProdutoRepository _produtoRepository;
        private CategoriaRepository _categoriaRepository;
        private ApiCatalogoDbContextOld _context;

        public UnityOfWork(ApiCatalogoDbContextOld context)
        {
            _context = context;
        }

        public IProdutoRepository ProdutoRepository
        {
            get { return _produtoRepository ?? new ProdutoRepository(_context); }
        }
        

        public void Commit()
        {
            _context?.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
