using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using Syngenta.Common.DomainObjects.DTO;
using Syngenta.Common.Log;
using System.Threading.Tasks;

namespace Syngenta.Sintegra.AntiCorruption
{
    public class NewDBaseGateway : IPartnerDataSourceGateway
    {
        private readonly IConfigurationManager _configManager;

        public NewDBaseGateway(IConfigurationManager configManager)
        {
            _configManager = configManager;
        }

        public async Task<SintegraNacionalResponseDTO> GetDataByCnpj(string cnpj, string uf)
        {
            return await Task.Run(() =>
            {
                var body = new
                {
                    nr_CNPJ = cnpj,
                    sg_UF = uf
                };

                return Request(body);
            });

        }

        private async Task<SintegraNacionalResponseDTO> Request(dynamic body)
        {
            return await Task.Run(() =>
            {

                Logger.Logar.Information($"SintegraNacionalResponseDTO: {JsonConvert.SerializeObject(body)}");
                
                var endpoint = _configManager.GetValue("SintegraWebService:Endpoint");
                var user = _configManager.GetValue("SintegraWebService:User");
                var password = _configManager.GetValue("SintegraWebService:Password");

                var client = new RestClient($"https://{user}:{password}@{endpoint}/api/153da7cb");
                //client.Timeout = -1;
                client.Authenticator = new NtlmAuthenticator(user, password);
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Cookie", "X-UAId=140; xpi-id=kbcZaYzHe5tOpjluZCQC; xpi-pid=BZH4Sz4Qd9TXNLisCzQZ");

                request.AddParameter("application/json", JsonConvert.SerializeObject(body), ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);
                SintegraNacionalResponseDTO responseDTO = new SintegraNacionalResponseDTO { Response = new Response { Status = new Status { Code = 400 } } };
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Logger.Logar.Information($"SintegraNacionalResponseDTO: {response.Content}");
                    responseDTO = JsonConvert.DeserializeObject<SintegraNacionalResponseDTO>(response.Content);
                }
                return responseDTO;
            });

        }

        public async Task<SintegraNacionalResponseDTO> GetDataByCpf(string cpf, string uf)
        {
            return await Task.Run(() =>
            {
                var body = new
                {
                    nr_CPF = cpf,
                    sg_UF = uf
                };

                return Request(body);
            });
        }
    }
}
