using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace navio_web.Models
{
    public class Navio
    {
        public int ID_NAVIO { get; set; } 
        public string NOME_NAVIO { get; set; } = string.Empty;
        public string PORTO { get; set; } = string.Empty;
        public string MODAL { get; set; } = string.Empty;

        // Buscar Navios do BD
        public static List<Navio> buscarNavios() 
        {
            var navios = new List<Navio>(); 
            string query = "SELECT * FROM Navio";
            try
            {
                using (var conexao = new Conexao().GetConnection())
                {
                    conexao.Open();

                    using (var cmd = new MySqlCommand(query, conexao))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var navio = new Navio 
                            {
                                ID_NAVIO = reader.GetInt32("ID_NAVIO"), // ID_NAVIO deve ser lido como int
                                NOME_NAVIO = reader.GetString("NOME_NAVIO"),
                                PORTO = reader.GetString("PORTO"),
                                MODAL = reader.GetString("MODAL")
                            };
                            navios.Add(navio); // Adiciona à lista de navios
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO! " + ex);
            }

            return navios; // Retorna a lista de navios
        }

        public static void cadastrarNavio(int idNavio, string nomeNavio, string porto, string modal)
        {
            string query = "INSERT INTO Navio (ID_NAVIO, NOME_NAVIO, PORTO, MODAL) " +
                           "VALUES (@ID_NAVIO, @NOME_NAVIO, @PORTO, @MODAL)";
            
            try
            {
                using (var conexao = new Conexao().GetConnection())
                {
                    conexao.Open();
                    using (var cmd = new MySqlCommand(query, conexao))
                    {
                        cmd.Parameters.AddWithValue("@ID_NAVIO", idNavio);
                        cmd.Parameters.AddWithValue("@NOME_NAVIO", nomeNavio); // Corrigido para incluir o símbolo '@'
                        cmd.Parameters.AddWithValue("@PORTO", porto);
                        cmd.Parameters.AddWithValue("@MODAL", modal);

                        cmd.ExecuteNonQuery(); // Executa a consulta.
                    }
                }
            }
            catch (Exception ex)
            {   
                Console.WriteLine("ERRO! " + ex.Message);
            }
        }

        public static void atualizarNavio(Navio navio)
        {
            try
            {
                using (var conexao = new Conexao().GetConnection())
                {
                    conexao.Open();

                    string checkQuery = @"SELECT COUNT(1) FROM Navio WHERE ID_NAVIO = @ID_NAVIO";
                    using (var cmd = new MySqlCommand(checkQuery, conexao))
                    {
                        cmd.Parameters.AddWithValue("@ID_NAVIO", navio.ID_NAVIO);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count == 0)
                        {
                            throw new Exception("Navio não encontrado."); // Corrigido para "Navio"
                        }
                    }

                    // Comando para atualizar no DB
                    string query = @"UPDATE Navio SET 
                                     NOME_NAVIO = @nomeNavio, 
                                     PORTO = @porto,
                                     MODAL = @modal
                                     WHERE ID_NAVIO = @idnavio";

                    using (var cmd = new MySqlCommand(query, conexao))
                    {
                        cmd.Parameters.AddWithValue("@nomeNavio", navio.NOME_NAVIO);
                        cmd.Parameters.AddWithValue("@porto", navio.PORTO);
                        cmd.Parameters.AddWithValue("@modal", navio.MODAL);
                        cmd.Parameters.AddWithValue("@idnavio", navio.ID_NAVIO); // Adicionando o parâmetro ID_NAVIO

                        // Executa o comando de atualização
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO! " + ex.Message);
            }
        }

          public static void deletarNavio(int idNavio)
        {
            string query = "DELETE FROM Navio WHERE ID_NAVIO = @ID_NAVIO";

            try
            {
                using (var conexao = new Conexao().GetConnection())
                {
                    conexao.Open();
                    
                    using( var cmd = new MySqlCommand(query, conexao))
                    {
                        cmd.Parameters.AddWithValue("@ID_NAVIO", idNavio);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("ERRO!" + ex.Message);
            }
        }
        
    }
    
}


