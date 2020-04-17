using AutoMapper;
using Syngenta.Sintegra.Domain;
using System;
using System.Threading.Tasks;

namespace Syngenta.Sintegra.AntiCorruption
{
    public class SintegraFacade : ISintegraFacade
    {
        private readonly IPartnerDataSourceGateway _partnerDataSourceGateway;
        private readonly IMapper _mapper;

        public SintegraFacade(IMapper mapper, IPartnerDataSourceGateway partnerDataSourceGateway)
        {
            _partnerDataSourceGateway = partnerDataSourceGateway;
            _mapper = mapper;
        }

        public async Task<Customer> GetDataByCnpj(string cnpj, string uf)
        {

            return _mapper.Map<Customer>(await _partnerDataSourceGateway.GetDataByCnpj(cnpj, uf));
        }

        public async Task<Customer> GetDataByCpf(string cpf, string uf)
        {
            return _mapper.Map<Customer>(await _partnerDataSourceGateway.GetDataByCpf(cpf, uf));
        }
    }
}
