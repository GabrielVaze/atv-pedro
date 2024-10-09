using System;

namespace AppNomesBr.Domain.Models
{
    public class Nomes
    {
        public int Id { get; set; } // Identificador único do nome
        public string Nome { get; set; } // Nome
        public int Frequencia { get; set; } // Frequência do nome
        public int Ranking { get; set; } // Ranking do nome
        public string Sexo { get; set; } // Sexo associado ao nome
        public string Resultado { get; set; } // Adicione esta linha se necessário
    }
}
