namespace navio_web.Models
{
    public class Fornecedor
    {
        public string ID_FORNECEDOR { get; set; }
        public string REFERENCIA_ID { get; set; }
        public string NOME_FORNECEDOR { get; set; }
        public string NOME_CONTATO { get; set; }
        public string FONE_ZAP { get; set; }
        public string EMAIL { get; set; }

        // Construtor
        public Fornecedor(string idFornecedor, string referenciaId, string nomeFornecedor, string nomeContato, string foneZap, string email)
        {
            ID_FORNECEDOR = idFornecedor;
            REFERENCIA_ID = referenciaId;
            NOME_FORNECEDOR = nomeFornecedor;
            NOME_CONTATO = nomeContato;
            FONE_ZAP = foneZap;
            EMAIL = email;
        }
    }
}
