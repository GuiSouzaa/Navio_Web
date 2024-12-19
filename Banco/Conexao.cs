using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

public class Conexao
{
    private string? _connectionString;

    public Conexao()
    {
        // Carregar as configurações do arquivo appsettings.json
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // Define o diretório base como o local onde o programa está sendo executado
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        var configuration = builder.Build();
        _connectionString = configuration.GetConnectionString("DefaultConnection");
        
    }

    public MySqlConnection GetConnection()
    {
        try
        {
            // Retorna uma nova conexão MySQL usando a connection string carregada
            return new MySqlConnection(_connectionString);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao conectar ao banco de dados: " + ex.Message);
            throw;
        }
    }
}
