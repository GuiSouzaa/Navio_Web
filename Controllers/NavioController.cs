using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using navio_web.Models;

namespace navio_web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NavioController : ControllerBase
    {
        //API CONSULTAR
        [HttpGet]
        public ActionResult<List<Navio>> GetNavio()
        {
            var navio = Navio.buscarNavios();
            if(navio == null || navio.Count == 0)
            {
                return NotFound("Nenhum navio encontrado");
            } 
            return Ok(navio);        
        }


    //API CADASTRAR
    [HttpPost]
    public ActionResult cadastrarnavio([FromBody] Navio navio)
    {
        if(navio == null)
        {
            return BadRequest("ERRO!");
        }

        //Chama o metodo para cadastro
        Navio.cadastrarNavio(navio.ID_NAVIO, navio.NOME_NAVIO, navio.PORTO, navio.MODAL);
        return CreatedAtAction(nameof(GetNavio), new { id = navio.ID_NAVIO}, navio);
        
    }

    //API ATUALIZAR
    [HttpPut("{id}")]
    public IActionResult atualizarNavio(int id, [FromBody] Navio navioAtualizado)
    {
        if (navioAtualizado == null)
        {
            return BadRequest("Dados inválidos.");
        }

        // Busca o navio pelo ID diretamente no banco de dados
        var navio = Navio.buscarNavios().FirstOrDefault(f => f.ID_NAVIO == id);
        if (navio == null)
        {
            return NotFound($"Navio com ID {id} não encontrado.");
        }

        // Atualiza os campos do navio
        navio.NOME_NAVIO = navioAtualizado.NOME_NAVIO;
        navio.PORTO = navioAtualizado.PORTO;
        navio.MODAL = navioAtualizado.MODAL;

        // Chama o método de atualização
        Navio.atualizarNavio(navio);
        return NoContent();
    }

    //API DELETAR
    [HttpDelete("{id}")]
    public IActionResult deletarNavio(int id)
    {
        Navio.deletarNavio(id);
        return NoContent();
    }

    }
}