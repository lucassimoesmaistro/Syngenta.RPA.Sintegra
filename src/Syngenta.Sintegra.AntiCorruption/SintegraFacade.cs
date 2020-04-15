using Syngenta.Sintegra.Domain;
using System;
namespace Syngenta.Sintegra.AntiCorruption
{
    public class SintegraFacade : ISintegraFacade
    {
        private readonly IPartnerDataSourceGateway _partnerDataSourceGateway;

        public SintegraFacade(IPartnerDataSourceGateway partnerDataSourceGateway)
        {
            _partnerDataSourceGateway = partnerDataSourceGateway;
        }

        public Customer GetDataByCnpj(string cnpj)
        {
            throw new NotImplementedException();
        }

        public Customer GetDataByCpf(string cpf)
        {

            throw new NotImplementedException();
        }
    }
}
