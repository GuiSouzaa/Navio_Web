using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class FornecedorController : ControllerBase
{
    // GET: api/fornecedores
    [HttpGet]
    public ActionResult<List<Fornecedor>> GetFornecedores()
    {
        var fornecedores = Fornecedor.BuscarFornecedores(); // Chama o método para buscar fornecedores do banco
        if (fornecedores == null || fornecedores.Count == 0)
        {
            return NotFound("Nenhum fornecedor encontrado.");
        }
        return Ok(fornecedores);
    }

    // POST: api/fornecedores
    [HttpPost]
    public ActionResult CadastrarFornecedor([FromBody] Fornecedor fornecedor)
    {
        if (fornecedor == null)
        {
            return BadRequest("Dados inválidos.");
        }

        // Chama o método para cadastrar fornecedor
        Fornecedor.CadastrarFornecedor(fornecedor.ID_FORNECEDOR, fornecedor.REFERENCIA_ID, fornecedor.NOME_FORNECEDOR, fornecedor.NOME_CONTATO, fornecedor.FONE_ZAP, fornecedor.EMAIL);

        return CreatedAtAction(nameof(GetFornecedores), new { id = fornecedor.ID_FORNECEDOR }, fornecedor);
    }

    // PUT: api/fornecedores/{id}
[HttpPut("{id}")]
public IActionResult AtualizarFornecedor(string id, [FromBody] Fornecedor fornecedorAtualizado)
{
    if (fornecedorAtualizado == null)
    {
        return BadRequest("Dados inválidos.");
    }

    var fornecedor = Fornecedor.BuscarFornecedores().FirstOrDefault(f => f.ID_FORNECEDOR == id); // Aqui você vai usar uma busca no banco real

    if (fornecedor == null)
    {
        return NotFound($"Fornecedor com ID {id} não encontrado.");
    }

    // Atualizando os dados do fornecedor com os novos valores
    fornecedor.NOME_FORNECEDOR = fornecedorAtualizado.NOME_FORNECEDOR;
    fornecedor.NOME_CONTATO = fornecedorAtualizado.NOME_CONTATO;
    fornecedor.FONE_ZAP = fornecedorAtualizado.FONE_ZAP;
    fornecedor.EMAIL = fornecedorAtualizado.EMAIL;

    // Chamar o método que atualiza no banco de dados (simulado ou real)
    Fornecedor.AtualizarFornecedor(fornecedor);

    return NoContent(); // Retorna sem corpo
}

  // DELETE: api/fornecedores/{id}
    [HttpDelete("{id}")]
    public IActionResult ExcluirFornecedor(string id)
    {
        // Chama o método para excluir fornecedor
        Fornecedor.ExcluirFornecedor(id);
        return NoContent(); // Retorna resposta de sucesso sem corpo
    }

}
