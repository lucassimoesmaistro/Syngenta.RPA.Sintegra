using Syngenta.Sintegra.AntiCorruption.DTO;

namespace Syngenta.Sintegra.AntiCorruption
{
    public interface IPartnerDataSourceGateway
    {
        SintegraNacionalResponseDTO GetDataByCnpj(string cnpj, string uf);

        SintegraNacionalResponseDTO GetDataByCpf(string cpf, string uf);
    }
}