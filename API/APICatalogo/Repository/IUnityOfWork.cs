namespace APICatalogo.Repository
{
    public interface IUnityOfWork
    {
        IProdutoRepository ProdutoRepository { get; }
        void Commit();
    }
}
