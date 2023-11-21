using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repository;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriasController : Controller
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriasController(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var categorias = await _categoriaRepository.GetAsync();

        return Ok(categorias);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(Categoria categoria)
    {
        await _categoriaRepository.AddAsync(categoria);

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var categoria = await _categoriaRepository.GetByIdAsync(id);
        if(categoria is null)
            return NotFound();

        return Ok(categoria);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _categoriaRepository.DeleteAsync(id);

        return Ok();
    }
}
