using APICatalogo.Models;
using APICatalogo.Repository;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

[ApiController]//Fornece vários recursos para um controller, como a validação do ModelState de forma automática, e tmb a ligação do ModelBind adicionando de forma automatica o [FromBody] para metodos Post
[Route("[controller]")]//Especifica uma rota para acessar esta classe, [controller]/{action} criará uma rota para cada metodo action dessa classe com o nome do metodo
public class ProdutosController: ControllerBase
{
    private readonly IUnityOfWork _uof;

    public ProdutosController(IUnityOfWork uof)
    {
        _uof = uof;
    }

    [HttpGet("menorpreco")]
    public ActionResult<IEnumerable<Produto>> GetProdutosMenorPrecos()
    {
        return _uof.ProdutoRepository.GetProdutosPorPreco().ToList();
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        var produtos = _uof.ProdutoRepository.Get().Take(10).ToList();/*O método Take irá impor um limite de produtos a serem buscados*/
        if (produtos is null)
        {
            return NotFound();
        }
        return produtos;
    }

    [HttpGet("{id:int}",Name ="ObterProduto")]//Especifica que esta rota irá receber um atributo id do tipo inteiro e adiciona um nome interno para esta rota, neste exemplo este nome de rota está sendo utilizado no método CreatedAtRouteResult
    public ActionResult<Produto> Get(int id)
    {
        var produto = _uof.ProdutoRepository.GetById(p => p.ProdutoId == id);
        if (produto is null)
        {
            return NotFound("Produto não encontrado");
        }
        return produto;
    }

    //Este metodo irá retornar apenas ActionResult, ou seja, respostas com códigos de status e sem objetos no Body
    [HttpPost]
    public IActionResult Post(Produto produto)//Graças ao DataAnnotation ApiController, irá pegar este atributo do Body da resposta automaticamente, validar e retornar BadRequest caso haja problemas na requisição
    {
        _uof.ProdutoRepository.Add(produto);
        _uof.Commit();
        
        return CreatedAtRoute("ObterProduto", new { id = produto.ProdutoId }, produto);/*Este método retorna o código 201 (created result)
                                                      E também adiciona um campo location no cabeçalho da resposta, com a URI
                                                      utilizada para consultar o objeto criado (uma das boas práticas rest).
                                                      O parametro ObterProduto corresponde ao nome da rota que será chamada
                                                      para consultar o objeto criado*/
    }

    [HttpPut("{id:int}")]
    public ActionResult<Produto> Put(int id, Produto produto)
    {
        if (produto.ProdutoId != id)//esta implementação está vulneravel a erros, pois está apenas verificando se o id do produto passado é igual ao id especificado para atualização
        {
            return BadRequest();
        }

        _uof.ProdutoRepository.Update(produto);
        _uof.Commit();
        return Ok(produto);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<Produto> Delete(int id)
    {
        var produto = _uof.ProdutoRepository?.GetById(p => p.ProdutoId == id);
        if (produto is null)
        {
            return NotFound("Produto não localizado");
        }
        _uof.ProdutoRepository?.Delete(produto);
        _uof.Commit();
        return Ok(produto);
    }
}
