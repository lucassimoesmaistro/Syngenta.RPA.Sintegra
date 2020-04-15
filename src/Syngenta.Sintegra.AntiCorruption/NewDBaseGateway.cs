using Newtonsoft.Json;
using RestSharp;
using Syngenta.Sintegra.AntiCorruption.DTO;

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
            var endpoint = _configManager.GetValue("SintegraWebService:Endpoint");
            var user = _configManager.GetValue("SintegraWebService:User");
            var password = _configManager.GetValue("SintegraWebService:Password");

            var client = new RestClient($"https://{user}:{password}@{endpoint}/api/153da7cb");
            //var client = new RestClient("https://SYNGTESTE:SYNGTESTE@dataintelligence.newdbase.com.br:443/api/153da7cb");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "X-UAId=140; xpi-id=kbcZaYzHe5tOpjluZCQC; xpi-pid=BZH4Sz4Qd9TXNLisCzQZ");

            var body = new
            {
                nr_CNPJ = cnpj,
                sg_UF = uf
            };

            request.AddParameter("application/json", JsonConvert.SerializeObject(body), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            SintegraNacionalResponseDTO resposta = new SintegraNacionalResponseDTO { Response =  new Response { Status = new Status { Code = 400 } } };
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                try
                {
                    resposta = JsonConvert.DeserializeObject<SintegraNacionalResponseDTO>(response.Content);
                }
                catch
                {
                    resposta = JsonConvert.DeserializeObject<SintegraNacionalResponseDTO>(response.Content);
                }

            }
            return resposta;
        }

        public SintegraNacionalResponseDTO GetDataByCpf(string cpf, string uf)
        {
            var endpoint = _configManager.GetValue("SintegraWebService:Endpoint");
            var user = _configManager.GetValue("SintegraWebService:User");
            var password = _configManager.GetValue("SintegraWebService:Password");

            var client = new RestClient($"https://{user}:{password}@{endpoint}/api/153da7cb");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "X-UAId=140; xpi-id=kbcZaYzHe5tOpjluZCQC; xpi-pid=BZH4Sz4Qd9TXNLisCzQZ");
            
            var body = new
            {
                nr_CPF = cpf,
                sg_UF = uf
            };

            request.AddParameter("application/json", JsonConvert.SerializeObject(body), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            SintegraNacionalResponseDTO resposta = new SintegraNacionalResponseDTO { Response = new Response { Status = new Status { Code = 400 } } };
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                try
                {
                    resposta = JsonConvert.DeserializeObject<SintegraNacionalResponseDTO>(response.Content);
                }
                catch
                {
                    resposta = JsonConvert.DeserializeObject<SintegraNacionalResponseDTO>(response.Content);
                }

            }
            return resposta;
        }
    }
}
