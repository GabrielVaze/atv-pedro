namespace AppNomesBr.Models
{
    public class NomeRanking
    {
        public string Nome { get; set; }         // Nome da pessoa
        public int Frequencia { get; set; }      // Quantidade de ocorrências
        public int Ranking { get; set; }          // Posição no ranking
        public string Sexo { get; set; }          // Sexo (Masculino ou Feminino)
    }
}
