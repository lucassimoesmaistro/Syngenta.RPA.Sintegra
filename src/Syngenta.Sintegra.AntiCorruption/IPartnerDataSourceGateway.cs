using Syngenta.Common.DomainObjects.DTO;
using System.Threading.Tasks;

namespace Syngenta.Sintegra.AntiCorruption
{
    public interface IPartnerDataSourceGateway
    {
        Task<SintegraNacionalResponseDTO> GetDataByCnpj(string cnpj, string uf);

        Task<SintegraNacionalResponseDTO> GetDataByCpf(string cpf, string uf);
    }
}