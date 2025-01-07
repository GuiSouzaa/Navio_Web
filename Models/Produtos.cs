using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;

namespace navio_web.Models
{
    public class Produtos
    {
    public int ID_PRODUTO { get; set;}
    public string ID_FORNECEDOR { get; set;} = string.Empty;
    public int id_tabela { get; set;}
    public string NOME_TABELA { get; set;} = string.Empty;
    public string REFERENCIA { get; set;} = string.Empty;
    public string FANTASIA { get; set;} = string.Empty;
    public string DESC_PRODUTO { get; set;} = string.Empty;
    public string CAR_PRODUTO { get; set;} = string.Empty;
    public int PESO_CAIXA { get; set;}
    public int PALLET_EURO { get; set;}
    public int PALLET_PBR { get; set;}
    public decimal? PRECO { get; set;}//Coloquei esse "?" porque antes se não tivesse preco ele não aparecia nada da tabela.
                    //Novo bug, aora mesmo com o "?" não aparece as coisas que estao faltando preco....

    public static List<Produtos> buscarProdutos()
    {
        var produtos = new List<Produtos>();
        string query = "SELECT * FROM Produtos";
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
                        var produto = new Produtos
                        {
                            ID_PRODUTO = reader.GetInt32("ID_PRODUTO"),
                            ID_FORNECEDOR = reader.GetString("ID_FORNECEDOR"),
                            id_tabela = reader.GetInt32("id_tabela"),
                            NOME_TABELA = reader.GetString("NOME_TABELA"),
                            REFERENCIA = reader.GetString("REFERENCIA"),
                            FANTASIA = reader.GetString("FANTASIA"),
                            DESC_PRODUTO = reader.GetString("DESC_PRODUTO"),
                            CAR_PRODUTO = reader.GetString("CAR_PRODUTO"),
                            PESO_CAIXA = reader.GetInt32("PESO_CAIXA"),
                            PALLET_EURO = reader.GetInt32("PALLET_EURO"),
                            PALLET_PBR = reader.GetInt32("PALLET_PBR"),
                            PRECO = reader.GetDecimal("PRECO")
                        };
                        produtos.Add(produto);
                    }
                }
            }
        }catch (Exception ex)
        {
            Console.WriteLine("ERRO!" + ex.Message);
        }
        return produtos;
         
    }//chave listar produtos

    public static void cadastrarProduto(int id_produto, string id_fornecedor, int id_tabela, string nome_tabela, string referencia, string fantasia, string desc_produto, string car_produto, int peso_caixa, int pallet_euro, int pallet_pbr, decimal preco)
    {
        string query = @"INSERT INTO produtos 
                 (ID_PRODUTO, ID_FORNECEDOR, id_tabela, NOME_TABELA, REFERENCIA, FANTASIA, DESC_PRODUTO, CAR_PRODUTO, PESO_CAIXA, PALLET_EURO, PALLET_PBR, PRECO) + 
                 VALUES (@ID_PRODUTO, @ID_FORNECEDOR, @id_tabela, @NOME_TABELA, @REFERENCIA, @FANTASIA, @DESC_PRODUTO, @CAR_PRODUTO, @PESO_CAIXA, @PALLET_EURO, @PALLET_PBR, @PRECO)";

        try
        {
            using (var conexao = new Conexao().GetConnection())
            {
                conexao.Open();

                using (var cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@ID_PRODUTO", id_produto);
                    //adicionar os outros parametros

                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch(Exception ex )
        {
            Console.WriteLine("Erro", ex.Message);
        }
    }

    }//chave da classe

}//Chave do namespace