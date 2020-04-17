using AutoMapper;
using Microsoft.Extensions.Configuration;
using Syngenta.Sintegra.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Syngenta.Sintegra.Application.SintegraComunication
{
    public class DataValidatorApplication : IDataValidatorApplication
    {
        private readonly IMapper _mapper;
        private readonly IRequestRepository _repository;

        public DataValidatorApplication(IMapper mapper, IRequestRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<Request>> GetValidationRequestWithRegisteredItems()
        {
            return await _repository.GetAllRequestsWithRegisteredItems();
        }
        
        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}
