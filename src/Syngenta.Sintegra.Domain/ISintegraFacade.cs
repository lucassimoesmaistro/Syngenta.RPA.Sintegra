using Syngenta.Common.DomainObjects.DTO;
using System.Threading.Tasks;

namespace Syngenta.Sintegra.Domain
{
    public interface ISintegraFacade
    {
        Task<SintegraNacionalResponseDTO> GetDataByCnpj(string cnpj, string uf);
        Task<SintegraNacionalResponseDTO> GetDataByCpf(string cpf, string uf);
    }
}
