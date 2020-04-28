using AutoMapper;
using Microsoft.Extensions.Configuration;
using Syngenta.Common.DomainObjects.DTO;
using Syngenta.Common.Log;
using Syngenta.Sintegra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syngenta.Sintegra.Application.SintegraComunication
{
    public class DataValidatorApplication : IDataValidatorApplication
    {
        private readonly IMapper _mapper;
        private readonly IRequestRepository _repository;
        private readonly ISintegraFacade _sintegraFacade;

        private readonly int _maxParalelRequest;

        public DataValidatorApplication(IConfiguration configuration,
                                        IMapper mapper,
                                        IRequestRepository repository,
                                        ISintegraFacade sintegraFacade)
        {
            _maxParalelRequest = int.Parse(configuration.GetSection("SintegraWebService:MaxParalelRequest").Value);
            _mapper = mapper;
            _repository = repository;
            _sintegraFacade = sintegraFacade;
        }

        public async Task<IEnumerable<Request>> GetValidationRequestWithRegisteredItems()
        {
            return await _repository.GetAllRequestsWithRegisteredItemsAndCommunicationFailure();
        }
        
        public void Dispose()
        {
            _repository?.Dispose();
        }

        public async Task VerifyDifferenceBetweenRequestItemAndSintegra(RequestItem item, Customer customer)
        {
            await Task.Run(() =>
            {
                string itemData;
                string customereData;

                itemData = PreparaData(item.CustomerName);
                customereData = PreparaData(customer.Name);
                if (itemData != customereData)
                {
                    var changeLog = new ChangeLog("Name", customereData);
                    changeLog.ConnectToRequestItem(item.Id);
                    _repository.AddChangeLog(changeLog);
                }

                itemData = PreparaData(item.CustomerStreet);
                customereData = PreparaData(customer.Street);
                if (itemData != customereData)
                {
                    var changeLog = new ChangeLog("Street", customereData);
                    changeLog.ConnectToRequestItem(item.Id);
                    _repository.AddChangeLog(changeLog);
                }

                itemData = PreparaData(item.CustomerHouseNumber);
                customereData = PreparaData(customer.HouseNumber);
                if (itemData != customereData)
                {
                    var changeLog = new ChangeLog("HouseNumber", customereData);
                    changeLog.ConnectToRequestItem(item.Id);
                    _repository.AddChangeLog(changeLog);
                }

                itemData = PreparaData(item.CustomerDistrict);
                customereData = PreparaData(customer.District);
                if (itemData != customereData)
                {
                    var changeLog = new ChangeLog("District", customereData);
                    changeLog.ConnectToRequestItem(item.Id);
                    _repository.AddChangeLog(changeLog);
                }

                itemData = PreparaData(item.CustomerPostalCode);
                customereData = PreparaData(customer.PostalCode);
                if (itemData != customereData)
                {
                    var changeLog = new ChangeLog("PostalCode", customereData);
                    changeLog.ConnectToRequestItem(item.Id);
                    _repository.AddChangeLog(changeLog);
                }

                itemData = PreparaData(item.CustomerCity);
                customereData = PreparaData(customer.City);
                if (itemData != customereData)
                {
                    var changeLog = new ChangeLog("City", customereData);
                    changeLog.ConnectToRequestItem(item.Id);
                    _repository.AddChangeLog(changeLog);
                }

                itemData = PreparaData(item.CustomerCountry);
                customereData = PreparaData(customer.Country);
                if (itemData != customereData)
                {
                    var changeLog = new ChangeLog("Country", customereData);
                    changeLog.ConnectToRequestItem(item.Id);
                    _repository.AddChangeLog(changeLog);
                }

                itemData = PreparaData(item.CustomerRegion);
                customereData = PreparaData(customer.Region);
                if (itemData != customereData)
                {
                    var changeLog = new ChangeLog("Region", customereData);
                    changeLog.ConnectToRequestItem(item.Id);
                    _repository.AddChangeLog(changeLog);
                }

                itemData = PreparaData(item.CustomerCNPJ);
                customereData = PreparaData(customer.CNPJ);
                if (itemData != customereData)
                {
                    var changeLog = new ChangeLog("CNPJ", customereData);
                    changeLog.ConnectToRequestItem(item.Id);
                    _repository.AddChangeLog(changeLog);
                }

                itemData = PreparaData(item.CustomerCPF);
                customereData = PreparaData(customer.CPF);
                if (itemData != customereData)
                {
                    var changeLog = new ChangeLog("CPF", customereData);
                    changeLog.ConnectToRequestItem(item.Id);
                    _repository.AddChangeLog(changeLog);
                }

                itemData = PreparaData(item.CustomerInscricaoEstadual);
                customereData = PreparaData(customer.InscricaoEstadual);
                if (itemData != customereData)
                {
                    var changeLog = new ChangeLog("InscricaoEstadual", customereData);
                    changeLog.ConnectToRequestItem(item.Id);
                    _repository.AddChangeLog(changeLog);
                }
                var changeLogConstant = new ChangeLog("SituacaoDoContribuinte", customer.SituacaoDoContribuinte);
                changeLogConstant.ConnectToRequestItem(item.Id);
                _repository.AddChangeLog(changeLogConstant);

                changeLogConstant = new ChangeLog("TipoIE", customer.TipoIE);
                changeLogConstant.ConnectToRequestItem(item.Id);
                _repository.AddChangeLog(changeLogConstant);

                changeLogConstant = new ChangeLog("DataDaSituação", customer.DataDaSituação.ToString());
                changeLogConstant.ConnectToRequestItem(item.Id);
                _repository.AddChangeLog(changeLogConstant);
            });
            
        }

        private string PreparaData(string dataToCompare)
        {
            return dataToCompare.Replace(".", "").Replace("-", "").Replace("/", "").Replace(".", "").ToUpper();
        }

        public async Task<bool> GetAllNewRequestsAndVerify()
        {
            Logger.Logar.Information($"GetAllNewRequestsAndVerify - maxParallelRequests: {_maxParalelRequest}");

            var requestsToVerify = await _repository.GetAllRequestsWithRegisteredItemsAndCommunicationFailure();
            bool result = false;
            Logger.Logar.Information($"requestsToVerify: {requestsToVerify.Count()}");
            requestsToVerify.ToList().ForEach(request =>
            {
                Logger.Logar.Information($"request: {request.Id.ToString()}");
                bool allRequestItemsOk = true;

                //TODO: Fiz the Repository Query in GetAllRequestsWithRegisteredItemsAndCommunicationFailure to avoid next line wiht Linq to Objets
                List<RequestItem> list = request.RequestItems
                                .Where(w => w.RequestItemStatus.Equals(RequestItemStatus.Registered) ||
                                            w.RequestItemStatus.Equals(RequestItemStatus.CommunicationFailure))
                                .ToList();

                Logger.Logar.Information($"RequestItems: {list.Count()}");
                for (int i = 0; i < list.Count; i = i + _maxParalelRequest)
                {
                    var items = list.Skip(i).Take(_maxParalelRequest).ToList();

                    Parallel.ForEach(items, item =>
                    {
                        string logInfo = string.IsNullOrEmpty(item.CustomerCNPJ) ? item.CustomerCPF : item.CustomerCNPJ;
                        Logger.Logar.Information($"Item: {logInfo}");
                        
                        SintegraNacionalResponseDTO response = GetDataFromSintegra(item);

                        if (response.Response.Status.Code == 200)
                        {
                            Logger.Logar.Information($"Item: {logInfo} - SintegraResponseOK");

                            PrepareAndVerify(item, response);
                        }
                        else
                        {
                            Logger.Logar.Error($"Item: {logInfo} - SetStatusCommunicationFailure - {response.Response.Status.Message}");
                            allRequestItemsOk = false;
                            item.SetStatusCommunicationFailure();
                        }

                        _repository.AtualizarItem(item);
                    });
                }

                if (allRequestItemsOk)
                    request.SetAsProcessed();


                _repository.Update(request);

                result = _repository.UnitOfWork.Commit().Result && allRequestItemsOk;
            });


            return result;
        }

        private void PrepareAndVerify(RequestItem item, SintegraNacionalResponseDTO response)
        {
            Customer customer;

            var output = response.Response.Output.Where(w => w.nr_InscricaoEstadual.Replace(",", "").Replace(".", "").Replace("-", "").Replace("/", "").Equals(item.CustomerInscricaoEstadual)).FirstOrDefault();
            if (output == null)
            {
                customer = new Customer(item.CustomerName, item.CustomerStreet, item.CustomerHouseNumber, item.CustomerDistrict, item.CustomerPostalCode, item.CustomerDistrict, item.CustomerCountry, item.CustomerRegion, item.CustomerCNPJ, item.CustomerCPF, "NÃO ENCONTRADO", "NÃO ENCONTRADO", "NÃO ENCONTRADO");
                item.SetStatusNotFound();
            }
            else
            {
                customer = _mapper.Map<Customer>(output);
                item.SetStatusChecked();
            }

            VerifyDifferenceBetweenRequestItemAndSintegra(item, customer).Wait();
        }

        private SintegraNacionalResponseDTO GetDataFromSintegra(RequestItem item)
        {
            SintegraNacionalResponseDTO response;
            if (!string.IsNullOrEmpty(item.CustomerCNPJ))
                response = _sintegraFacade.GetDataByCnpj(item.CustomerCNPJ, item.CustomerRegion).Result;
            else
                response = _sintegraFacade.GetDataByCpf(item.CustomerCPF, item.CustomerRegion).Result;
            return response;
        }

        
    }
}
