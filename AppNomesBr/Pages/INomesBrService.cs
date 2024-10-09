using System.Collections.Generic;
using System.Threading.Tasks;
using AppNomesBr.Domain.Models; // Ajuste conforme o namespace da classe Nomes


namespace AppNomesBr.Domain.Interfaces.Services
{
    public interface INomesBrService
    {
        Task<List<Nomes>> ListaTop20Nacional();
        Task<List<Nomes>> ListaMeuRanking();
        Task<List<Nomes>> ListaMeuRankingPorSexo(string sexo); // Método adicionado
        Task InserirNovoRegistroNoRanking(string nome, string sexo);
    }
}
