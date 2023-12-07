using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Data;

namespace API.Controllers
{
    [ApiController]
    [Route("api/categoria")]
    public class CategoriaController : ControllerBase
  {
    private readonly AppDataContext _ctx;

    public CategoriaController(AppDataContext ctx)
    {
        _ctx = ctx;
    }

    [HttpPost]
    [Route("cadastrar")]
    public IActionResult Cadastrar([FromBody] Categoria categoria)
    {
        _ctx.Categorias.Add(categoria);
        _ctx.SaveChanges();
        return Created("", categoria);
    }

    [HttpGet]
    [Route("listar")]
    public IActionResult Listar()
    {
        List<Categoria> categorias = _ctx.Categorias.ToList();
        return categorias.Count == 0 ? NotFound() : Ok(categorias);
    }

    [HttpPut]
    [Route("atualizar/{id}")]
    public IActionResult Atualizar([FromRoute] int id, 
        [FromBody] Categoria categoria)
    {
        Categoria? categoriaCadastrado = _ctx.Categorias.FirstOrDefault(x => x.CategoriaId == id);
        if(categoriaCadastrado != null)
        {
            categoriaCadastrado.Nome = categoria.Nome;
            categoriaCadastrado.Descricao = categoria.Descricao;
            _ctx.Categorias.Update(categoriaCadastrado);
            _ctx.SaveChanges();
            return Ok(categoriaCadastrado);

        }
        else
        {
        return NotFound();
        }
    }

    [HttpDelete]
    [Route("deletar/{id}")]
    public IActionResult Deletar([FromRoute] int id)
    {
        Categoria? categoriaCadastrada = _ctx.Categorias.Find(id);
        if (categoriaCadastrada != null)
        {
            _ctx.Categorias.Remove(categoriaCadastrada);
            _ctx.SaveChanges();
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
  }
}
