using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Data;

namespace API.Controllers;

[ApiController]
[Route("api/item")]
public class ItemController : ControllerBase
{
    private readonly AppDataContext _ctx;
    public ItemController(AppDataContext ctx)
    {
        _ctx = ctx;
    }

    [HttpPost]
    [Route("cadastrar")]
    public IActionResult Cadastrar([FromBody] Item item)
    {
      try
      {
        {
          Categoria? categoria = _ctx.Categorias.Find(item.CategoriaId);
          if (categoria == null)
          {
            return NotFound("Categoria não encontrada.");
          }
            item.Efeito = string.IsNullOrWhiteSpace(item.Efeito) ? "Esse item não possui efeitos" : item.Efeito;

            item.Durabilidade = "CHEIO";

            item.Categoria = categoria;
                
            _ctx.Items.Add(item);
            _ctx.SaveChanges();

            return Created("", item);
        }
      }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }
        
    

    [HttpGet]
    [Route("listar")]
    public IActionResult Listar()
    {
        List<Item> items = _ctx.Items.Include(x => x.Categoria).ToList();
        return Ok(items);
    }

    [HttpGet]
    [Route("listar-efeito")]
    public IActionResult ListarEfeito()
    {
        List<Item> items = _ctx.Items.Where(x => !string.IsNullOrWhiteSpace(x.Efeito)
            && x.Efeito != "Esse item não possui efeitos").ToList();

        return Ok(items);
    }

    [HttpPut]
    [Route("atualizar/{id}")]
    public IActionResult Atualizar([FromRoute] int id, 
        [FromBody] Item item)
    {
        Item? itemCadastrado = _ctx.Items.FirstOrDefault(x => x.ItemId == id);
        if(itemCadastrado != null)
        {
            itemCadastrado.Efeito = item.Efeito;
            itemCadastrado.Nome = item.Nome;
            itemCadastrado.Quantidade = item.Quantidade;
            _ctx.Items.Update(itemCadastrado);
            _ctx.SaveChanges();
            return Ok(itemCadastrado);

        }
        else
        {
        return NotFound();
        }
    }

    [HttpPut]
    [Route("atualizar-durabilidade/{id}")]
    public IActionResult AtualizarDurabilidade([FromRoute] int id)
    {
        var item = _ctx.Items.FirstOrDefault(x => x.ItemId == id);

    if (item != null)
    {
        switch (item.Durabilidade)
        {
            case "CHEIO":
                item.Durabilidade = "MÉDIO";
                break;
            case "MÉDIO":
                item.Durabilidade = "VAZIO";
                break;
        }

        _ctx.SaveChanges();

        return Ok(item);
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
        Item? itemCadastrado = _ctx.Items.Find(id);
        if(itemCadastrado != null)
        {
            _ctx.Items.Remove(itemCadastrado);
            _ctx.SaveChanges();
            return Ok();
        }
        else
        {
            return NotFound();
        }
    } 

    [HttpGet]
    [Route("buscar/{id}")]
    public IActionResult Bucar([FromRoute] int id)
    {
        Item? itemCadastrado = _ctx.Items.FirstOrDefault(x => x.ItemId == id);
        if(itemCadastrado != null)
        {
            return Ok(itemCadastrado);
        }
        else
        {
            return NotFound();
        }
    }
        
      
}
