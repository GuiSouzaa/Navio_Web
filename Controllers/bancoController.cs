using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly string _connectionString = "Server=localhost;Database=Navio;Uid=root;Pwd=Gui181201@;";

        // Endpoint para verificar a conexão com o banco de dados
        [HttpGet("check-connection")]
        public IActionResult CheckConnection()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    return Ok("Conexão com o banco de dados bem-sucedida!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao conectar ao banco de dados: {ex.Message}");
            }
        }
    }
}
