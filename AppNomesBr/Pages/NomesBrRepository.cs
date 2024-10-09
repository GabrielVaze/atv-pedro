using AppNomesBr.Domain.Interfaces.Repositories;
using AppNomesBr.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppNomesBr.Infrastructure.Repositories
{
    public class NomesBrRepository : INomesBrRepository
    {
        private readonly List<Nomes> _nomes; // Aqui você pode substituir por seu contexto de banco de dados

        public NomesBrRepository()
        {
            // Inicializar com alguns dados para teste (pode ser substituído por acesso a um banco de dados)
            _nomes = new List<Nomes>
            {
                new Nomes { Id = 1, Nome = "João", Frequencia = 1000, Ranking = 1, Sexo = "M" },
                new Nomes { Id = 2, Nome = "Maria", Frequencia = 900, Ranking = 2, Sexo = "F" },
                // Adicione mais nomes conforme necessário
            };
        }

        public Task<List<Nomes>> GetAll()
        {
            return Task.FromResult(_nomes);
        }

        public Task<List<Nomes>> GetTop20Nacional()
        {
            // Retorna os 20 nomes mais frequentes (ou como for definido)
            var top20 = _nomes.OrderByDescending(n => n.Frequencia).Take(20).ToList();
            return Task.FromResult(top20);
        }

        public Task Delete(int id)
        {
            var nome = _nomes.FirstOrDefault(n => n.Id == id);
            if (nome != null)
            {
                _nomes.Remove(nome);
            }
            return Task.CompletedTask;
        }

        public Task Insert(Nomes nome) // Implementação do método Insert
        {
            // Para simular um ID auto-incremental
            nome.Id = _nomes.Max(n => n.Id) + 1;
            _nomes.Add(nome);
            return Task.CompletedTask;
        }
    }
}
