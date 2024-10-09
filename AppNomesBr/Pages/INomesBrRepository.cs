using System.Collections.Generic;
using System.Threading.Tasks;
using AppNomesBr.Domain.Interfaces.Repositories;
using AppNomesBr.Domain.Models; // Verifique se isso está no topo do arquivo



namespace AppNomesBr.Domain.Interfaces.Repositories
{
    public interface INomesBrRepository
    {
        Task<List<Nomes>> GetTop20Nacional(); // Para obter os top 20 nomes
        Task<List<Nomes>> GetAll(); // Para obter todos os registros
        Task Delete(int id); // Para deletar um registro
        Task Insert(Nomes nome); // Para inserir um novo registro
    }
}
