using AutoMapper;
using Microsoft.Extensions.Configuration;
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

        public DataValidatorApplication(IMapper mapper, IRequestRepository repository, ISintegraFacade sintegraFacade)
        {
            _mapper = mapper;
            _repository = repository;
            _sintegraFacade = sintegraFacade;
        }

        public async Task<IEnumerable<Request>> GetValidationRequestWithRegisteredItems()
        {
            return await _repository.GetAllRequestsWithRegisteredItems();
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
            });
            
        }

        private string PreparaData(string dataToCompare)
        {
            return dataToCompare.Replace(".", "").Replace("-", "").Replace("/", "").Replace(".", "").ToUpper();
        }

        public async Task<bool> GetAllNewRequestsAndVerify()
        {
            var requestsToVerify = await _repository.GetAllRequestsWithRegisteredItems();
            bool result = false;
            int maxParallelRequests = 3;
            requestsToVerify.ToList().ForEach(request =>
            {
                List<RequestItem> list = request.RequestItems.ToList();
                for (int i = 0; i < list.Count; i = i + maxParallelRequests)
                {
                    var items = list.Skip(i).Take(maxParallelRequests).ToList();
                    Parallel.ForEach(items, item =>
                    {
                        Customer customer;
                        if (!string.IsNullOrEmpty(item.CustomerCNPJ))
                            customer = _sintegraFacade.GetDataByCnpj(item.CustomerCNPJ, item.CustomerRegion).Result;
                        else
                            customer = _sintegraFacade.GetDataByCpf(item.CustomerCPF, item.CustomerRegion).Result;

                        VerifyDifferenceBetweenRequestItemAndSintegra(item, customer).Wait();

                    });
                }                

                result = _repository.UnitOfWork.Commit().Result;
            });


            return result;
        }

    }
}
