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

         [HttpPost]

    public ActionResult cadastrarProduto([FromBody] Produtos produtos)
    {
        if(produtos == null)
        {
            return BadRequest("ERRO!");
        }

        Produtos.cadastrarProduto(produtos.ID_PRODUTO, produtos.ID_FORNECEDOR, produtos.id_tabela, produtos.NOME_TABELA, produtos.REFERENCIA, produtos.FANTASIA, produtos.DESC_PRODUTO, produtos.CAR_PRODUTO, produtos.PESO_CAIXA, produtos.PALLET_EURO, produtos.PALLET_PBR, produtos.PRECO);
        return CreatedAtAction(nameof(GetProdutos), new { id = produtos.ID_PRODUTO }, produtos);
        //Seguinte, no final da linha estava dando erro no preco porque nao aceitava o "?" que esta no decimal da classe Produtos.
        //Ao efetuar o cadastro ele da algum tipo de erro verificar qual erro é esse, minha ideia: 
        //Parece que o cadastro é efetuado mas fica em espaco vazio, deve estar falando algum dado ou relacionamento(deve ter que ter um id de referencia.
    }


    [HttpPut("{id}")]
    public IActionResult atualizarProduto(int id, [FromBody] Produtos produtosAtualizado)
    {
        if(produtosAtualizado == null)
        {
            return BadRequest("Dados inválidos.");
        }

        var produto = Produtos.buscarProdutos().FirstOrDefault(f => f.ID_PRODUTO == id);//Produto ou Produtos? - Esse "S" importa?
        if(produto == null)
        {
            return NotFound($"Navio com ID {id} não encontrado.");
        }

        produto.ID_PRODUTO = produtosAtualizado.ID_PRODUTO;
        produto.id_tabela = produtosAtualizado.id_tabela;
        produto.NOME_TABELA = produtosAtualizado.NOME_TABELA;
        produto.REFERENCIA = produtosAtualizado.REFERENCIA;
        produto.FANTASIA = produtosAtualizado.FANTASIA;
        produto.DESC_PRODUTO = produtosAtualizado.DESC_PRODUTO;
        produto.CAR_PRODUTO = produtosAtualizado.CAR_PRODUTO;
        produto.PESO_CAIXA = produtosAtualizado.PESO_CAIXA;
        produto.PALLET_EURO = produtosAtualizado.PALLET_EURO;
        produto.PALLET_PBR = produtosAtualizado.PALLET_PBR;
        produto.PRECO = produtosAtualizado.PRECO;

        Produtos.atualizarProduto(produto);
        return NoContent();
    }

    }

   
}