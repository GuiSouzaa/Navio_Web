using MySql.Data.MySqlClient;


public class Fornecedor
{
    public string ID_FORNECEDOR { get; set; } = string.Empty;
    public string REFERENCIA_ID { get; set; } = string.Empty;
    public string NOME_FORNECEDOR { get; set; } = string.Empty;
    public string NOME_CONTATO { get; set; } = string.Empty;
    public string FONE_ZAP { get; set; } = string.Empty;
    public string EMAIL { get; set; } = string.Empty;//Esse sinal de "?" deixa ser null e não precisa definir um construtor já que eu puxo os dados diretamente do banco de dados


    // Método para buscar fornecedores no banco de dados
    public static List<Fornecedor> BuscarFornecedores()
    {
        var fornecedores = new List<Fornecedor>();

        // Definir a consulta SQL
        string query = "SELECT * FROM Fornecedor";

        try
        {
            // Criar a conexão com o banco de dados usando a classe Conexao
            using (var conexao = new Conexao().GetConnection())
            {
                conexao.Open(); // Abre a conexão com o banco de dados

                using (var cmd = new MySqlCommand(query, conexao))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Preencher a lista de fornecedores com os dados retornados
                        var fornecedor = new Fornecedor
                        {
                            ID_FORNECEDOR = reader.GetString("ID_FORNECEDOR"),
                            REFERENCIA_ID = reader.GetString("REFERENCIA_ID"),
                            NOME_FORNECEDOR = reader.GetString("NOME_FORNECEDOR"),
                            NOME_CONTATO = reader.GetString("NOME_CONTATO"),
                            FONE_ZAP = reader.GetString("FONE_ZAP"),
                            EMAIL = reader.GetString("EMAIL")
                        };
                        fornecedores.Add(fornecedor);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao buscar fornecedores: " + ex.Message);
        }

        return fornecedores;
    }

    // Método para cadastrar fornecedor no banco
    public static void CadastrarFornecedor(string idFornecedor, string referenciaId, string nomeFornecedor, string nomeContato, string foneZap, string email)
    {
        string query = "INSERT INTO Fornecedor (ID_FORNECEDOR, REFERENCIA_ID, NOME_FORNECEDOR, NOME_CONTATO, FONE_ZAP, EMAIL) " +
                       "VALUES (@ID_FORNECEDOR, @REFERENCIA_ID, @NOME_FORNECEDOR, @NOME_CONTATO, @FONE_ZAP, @EMAIL)";

        try
        {
            using (var conexao = new Conexao().GetConnection())
            {
                conexao.Open(); // Abre a conexão com o banco de dados

                using (var cmd = new MySqlCommand(query, conexao))
                {
                    // Adiciona os parâmetros para prevenir injeção de SQL
                    cmd.Parameters.AddWithValue("@ID_FORNECEDOR", idFornecedor);
                    cmd.Parameters.AddWithValue("@REFERENCIA_ID", referenciaId);
                    cmd.Parameters.AddWithValue("@NOME_FORNECEDOR", nomeFornecedor);
                    cmd.Parameters.AddWithValue("@NOME_CONTATO", nomeContato);
                    cmd.Parameters.AddWithValue("@FONE_ZAP", foneZap);
                    cmd.Parameters.AddWithValue("@EMAIL", email);

                    cmd.ExecuteNonQuery(); // Executa a consulta
                }
            }

            Console.WriteLine($"Fornecedor {nomeFornecedor} cadastrado com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao cadastrar fornecedor: " + ex.Message);
        }
    }

    // Método para excluir fornecedor do banco
    public static void ExcluirFornecedor(string idFornecedor)
    {
        string query = "DELETE FROM Fornecedor WHERE ID_FORNECEDOR = @ID_FORNECEDOR";

        try
        {
            using (var conexao = new Conexao().GetConnection())
            {
                conexao.Open(); // Abre a conexão com o banco de dados

                using (var cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@ID_FORNECEDOR", idFornecedor);
                    cmd.ExecuteNonQuery(); // Executa a consulta
                }
            }

            Console.WriteLine($"Fornecedor com ID {idFornecedor} excluído.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao excluir fornecedor: " + ex.Message);
        }
    }

    public static void AtualizarFornecedor(Fornecedor fornecedor)
{
    // Lógica para atualizar o fornecedor no banco de dados
    try
    {
        using (var conn = new Conexao().GetConnection())
        {
            conn.Open();

            // Verifica se o fornecedor existe
            string checkQuery = @"SELECT COUNT(1) FROM Fornecedor WHERE ID_FORNECEDOR = @IdFornecedor";
            using (var checkCmd = new MySqlCommand(checkQuery, conn))
            {
                checkCmd.Parameters.AddWithValue("@IdFornecedor", fornecedor.ID_FORNECEDOR);
                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                // Se o fornecedor não existir, lança uma exceção
                if (count == 0)
                {
                    throw new Exception("Fornecedor não encontrado.");
                }
            }

            // Atualiza o fornecedor no banco de dados
            string query = @"UPDATE Fornecedor SET 
                                NOME_FORNECEDOR = @NomeFornecedor,
                                NOME_CONTATO = @NomeContato,
                                FONE_ZAP = @FoneZap,
                                EMAIL = @Email
                            WHERE ID_FORNECEDOR = @IdFornecedor";
            
            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.Add("@NomeFornecedor", MySqlDbType.VarChar).Value = fornecedor.NOME_FORNECEDOR;
                cmd.Parameters.Add("@NomeContato", MySqlDbType.VarChar).Value = fornecedor.NOME_CONTATO;
                cmd.Parameters.Add("@FoneZap", MySqlDbType.VarChar).Value = fornecedor.FONE_ZAP;
                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = fornecedor.EMAIL;
                cmd.Parameters.Add("@IdFornecedor", MySqlDbType.VarChar).Value = fornecedor.ID_FORNECEDOR;

                cmd.ExecuteNonQuery();
            }
        }
    }
    catch (Exception ex)
    {
        // Em caso de erro, pode-se registrar o erro ou retornar uma mensagem específica
        Console.WriteLine($"Erro ao atualizar fornecedor: {ex.Message}");
        throw; // Pode ser lançado ou tratado de acordo com a necessidade
    }
}


}
