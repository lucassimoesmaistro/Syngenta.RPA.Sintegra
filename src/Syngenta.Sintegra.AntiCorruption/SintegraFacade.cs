using AutoMapper;
using Syngenta.Common.DomainObjects.DTO;
using Syngenta.Sintegra.Domain;
using System;
using System.Threading.Tasks;

namespace Syngenta.Sintegra.AntiCorruption
{
    public class SintegraFacade : ISintegraFacade
    {
        private readonly IPartnerDataSourceGateway _partnerDataSourceGateway;

        public SintegraFacade(IPartnerDataSourceGateway partnerDataSourceGateway)
        {
            _partnerDataSourceGateway = partnerDataSourceGateway;
        }

        public async Task<SintegraNacionalResponseDTO> GetDataByCnpj(string cnpj, string uf)
        {

            return await _partnerDataSourceGateway.GetDataByCnpj(cnpj, uf);
        }

        public async Task<SintegraNacionalResponseDTO> GetDataByCpf(string cpf, string uf)
        {
            return await _partnerDataSourceGateway.GetDataByCpf(cpf, uf);
        }
    }
}
