using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using navio_web.Models;


namespace navio_web.Banco
{
    public class FornecedorDAL
    {
        public IEnumerable<Fornecedor> Listar()
    {
        var lista = new List<Fornecedor>();

        using var connection = new Connection().obterConexao();
        connection.Open();

        string sql = "SELECT * FROM Fornecedor";
        MySqlCommand command = new MySqlCommand(sql, connection);
        using MySqlDataReader dataReader = command.ExecuteReader();

        while (dataReader.Read())
        {
            string idFornecedor = Convert.ToString(dataReader["ID_FORNECEDOR"]);
            string referenciaId = Convert.ToString(dataReader["REFERENCIA_ID"]);
            string nomeFornecedor = Convert.ToString(dataReader["NOME_FORNECEDOR"]);
            string nomeContato = Convert.ToString(dataReader["NOME_CONTATO"]);
            string foneZap = Convert.ToString(dataReader["FONE_ZAP"]);
            string email = Convert.ToString(dataReader["EMAIL"]);

            Fornecedor fornecedor = new(idFornecedor ,referenciaId, nomeFornecedor, nomeContato, foneZap, email);

            lista.Add(fornecedor);
        }

        return lista;
    }
        public void adicionar(Fornecedor fornecedor)
    {
        using var connection = new Connection().obterConexao();
        connection.Open();

        string sql = "INSERT INTO Fornecedor (ID_FORNECEDOR, REFERENCIA_ID, NOME_FORNECEDOR, NOME_CONTATO, FONE_ZAP, EMAIL) VALUES (@idFornecedor, @referenciaId, @nomeFornecedor, @nomeContato, @foneZap, @email)";
        MySqlCommand command = new MySqlCommand(sql, connection); 

        command.Parameters.AddWithValue("@idFornecedor", fornecedor.ID_FORNECEDOR);
        command.Parameters.AddWithValue("@referenciaId", fornecedor.REFERENCIA_ID);
        command.Parameters.AddWithValue("@nomeFornecedor", fornecedor.NOME_FORNECEDOR);
        command.Parameters.AddWithValue("@nomeContato", fornecedor.NOME_CONTATO);
        command.Parameters.AddWithValue("@foneZap", fornecedor.FONE_ZAP);
        command.Parameters.AddWithValue("@email", fornecedor.EMAIL);

        int retorno = command.ExecuteNonQuery();
        Console.WriteLine($"Linhas afetadas {retorno}");

    }

    public void atualizar(Fornecedor fornecedor)
    {
        using var connection = new Connection().obterConexao();
        connection.Open();

            string sql = "UPDATE Fornecedor SET " +
                 "REFERENCIA_ID = @referenciaId, " +
                 "NOME_FORNECEDOR = @nomeFornecedor, " +
                 "NOME_CONTATO = @nomeContato, " +
                 "FONE_ZAP = @foneZap, " +
                 "EMAIL = @email " +
                 "WHERE ID_FORNECEDOR = @idFornecedor";

        MySqlCommand command = new MySqlCommand(sql, connection);

        command.Parameters.AddWithValue("@idFornecedor", fornecedor.ID_FORNECEDOR);
        command.Parameters.AddWithValue("@referenciaId", fornecedor.REFERENCIA_ID);
        command.Parameters.AddWithValue("@nomeFornecedor", fornecedor.NOME_FORNECEDOR);
        command.Parameters.AddWithValue("@nomeContato", fornecedor.NOME_CONTATO);
        command.Parameters.AddWithValue("@foneZap", fornecedor.FONE_ZAP);
        command.Parameters.AddWithValue("@email", fornecedor.EMAIL);

        int retorno = command.ExecuteNonQuery();
        Console.WriteLine($"Linhas afetadas {retorno}");
    }

    public void deletar(Fornecedor fornecedor)
    {
        using var connection = new Connection().obterConexao();
        connection.Open();

        string sql = "DELETE FROM Fornecedor WHERE ID_FORNECEDOR = @idFornecedor";
        MySqlCommand command = new MySqlCommand(sql, connection);

        command.Parameters.AddWithValue("@idFornecedor", fornecedor.ID_FORNECEDOR);
        int retorno = command.ExecuteNonQuery();
        Console.WriteLine($"Linhas afetadas {retorno}");
    }
    
    }

   
}