using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using Syngenta.Sintegra.AntiCorruption.DTO;
using System;

namespace Syngenta.Sintegra.AntiCorruption
{
    public class NewDBaseGateway : IPartnerDataSourceGateway
    {
        private readonly IConfigurationManager _configManager;

        public NewDBaseGateway(IConfigurationManager configManager)
        {
            _configManager = configManager;
        }

        public SintegraNacionalResponseDTO GetDataByCnpj(string cnpj, string uf)
        {
            var body = new
            {
                nr_CNPJ = cnpj,
                sg_UF = uf
            };

            return Request(body);

        }

        private SintegraNacionalResponseDTO Request(dynamic body)
        {
            var endpoint = _configManager.GetValue("SintegraWebService:Endpoint");
            var user = _configManager.GetValue("SintegraWebService:User");
            var password = _configManager.GetValue("SintegraWebService:Password");

            var client = new RestClient($"https://{endpoint}/api/153da7cb");
            client.Timeout = -1;
            client.Authenticator = new NtlmAuthenticator(user, password);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "X-UAId=140; xpi-id=kbcZaYzHe5tOpjluZCQC; xpi-pid=BZH4Sz4Qd9TXNLisCzQZ");

            request.AddParameter("application/json", JsonConvert.SerializeObject(body), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            SintegraNacionalResponseDTO responseDTO = new SintegraNacionalResponseDTO { Response = new Response { Status = new Status { Code = 400 } } };
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                try
                {
                    responseDTO = JsonConvert.DeserializeObject<SintegraNacionalResponseDTO>(response.Content);
                }
                catch
                {
                    responseDTO = JsonConvert.DeserializeObject<SintegraNacionalResponseDTO>(response.Content);
                }

            }
            return responseDTO;
        }

        public SintegraNacionalResponseDTO GetDataByCpf(string cpf, string uf)
        {
            var body = new
            {
                nr_CPF = cpf,
                sg_UF = uf
            };

            return Request(body);
        }
    }
}
