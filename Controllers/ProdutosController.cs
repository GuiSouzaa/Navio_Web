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
    public class ProdutosController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Produtos>> GetProdutos()
        {
            var produtos = Produtos.buscarProdutos();
            if(produtos == null || produtos.Count == 0)
            {
                return NotFound("Nenhum produto foi encontrado");
            }
            return Ok (produtos);
        }
    }
}