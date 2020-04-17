using Syngenta.Sintegra.AntiCorruption.DTO;
using System.Threading.Tasks;

namespace Syngenta.Sintegra.AntiCorruption
{
    public interface IPartnerDataSourceGateway
    {
        Task<Output> GetDataByCnpj(string cnpj, string uf);

        Task<Output> GetDataByCpf(string cpf, string uf);
    }
}