using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppNomesBr.Domain.Interfaces.Repositories;
using AppNomesBr.Domain.Interfaces.Services;
using AppNomesBr.Domain.Models; // Certifique-se de que a classe Nomes está nesse namespace

namespace AppNomesBr.Domain.Services
{
    public class NomesBrService : INomesBrService
    {
        private readonly INomesBrRepository _repository; // Renomeado para manter a consistência

        public NomesBrService(INomesBrRepository repository)
        {
            _repository = repository; // Inicializa o repositório
        }

        public async Task<List<Nomes>> ListaTop20Nacional()
        {
            // Implementação para obter os 20 nomes mais populares
            return await _repository.GetTop20Nacional(); // Supondo que você tenha esse método no repositório
        }

        public async Task<List<Nomes>> ListaMeuRanking()
        {
            // Implementação para obter o ranking de nomes
            return await _repository.GetAll(); // Ajuste conforme sua implementação
        }

        public async Task<List<Nomes>> ListaMeuRankingPorSexo(string sexo)
        {
            // Implementação para filtrar os nomes por sexo
            var todosNomes = await ListaMeuRanking();
            return todosNomes.FindAll(n => n.Sexo.Equals(sexo, StringComparison.OrdinalIgnoreCase));
        }

        public async Task InserirNovoRegistroNoRanking(string nome, string sexo)
        {
            // Implementação para inserir um novo registro no ranking
            await _repository.Insert(new Nomes { Nome = nome, Sexo = sexo }); // Ajuste conforme sua estrutura
        }
    }
}
