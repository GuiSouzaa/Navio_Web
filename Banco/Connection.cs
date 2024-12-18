using MySql.Data.MySqlClient;
using navio_web.Models;

internal class Connection
{
    private string connectionString = "Server=localhost;Database=Navio;Uid=root;Pwd=Gui181201@;";

    public MySqlConnection obterConexao()
    {
        return new MySqlConnection(connectionString);
    }

    
}