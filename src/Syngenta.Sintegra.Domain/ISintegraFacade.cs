using System.Threading.Tasks;

namespace Syngenta.Sintegra.Domain
{
    public interface ISintegraFacade
    {
        Task<Customer> GetDataByCnpj(string cnpj, string uf);
        Task<Customer> GetDataByCpf(string cpf, string uf);
    }
}
