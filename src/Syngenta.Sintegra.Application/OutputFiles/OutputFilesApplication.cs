using Syngenta.Sintegra.Domain;
using System;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Syngenta.Common.Log;
using System.Linq;
using Syngenta.Common.Office;
using System.Collections.Generic;
using Syngenta.Sintegra.Application.OutputFiles.Models;
using AutoMapper;

namespace Syngenta.Sintegra.Application.OutputFiles
{
    public class OutputFilesApplication : IOutputFilesApplication
    {
        private readonly IRequestRepository _repository;
        private readonly IMapper _mapper;
        public IConfiguration Configuration { get; }

        private readonly string _filesPath;

        public OutputFilesApplication(IConfiguration configuration,
                                      IMapper mapper,
                                      IRequestRepository repository)
        {
            _filesPath = configuration["FilesPath:ProcessedFiles"];
            _mapper = mapper;
            _repository = repository;
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }

        public async Task<bool> GetAllProcessed()
        {
            return await Task.Run(() =>
            {
                Logger.Logar.Information($"GetAllProcessed");

                var requests = _repository.GetAllRequestsWithAllItemsProcessed().Result.ToList();
                bool result = false;
                Logger.Logar.Information($"AllRequestsWithAllItemsProcessed: {requests.Count()}");

                Parallel.ForEach(requests, request =>
                {
                    Logger.Logar.Information($"request: {request.Id.ToString()}");
                    bool allRequestItemsOk = true;

                    List<OutputFileColumns> list = _mapper.Map<List<OutputFileColumns>>(request.RequestItems);

                    //TODO: Create Excel File
                    var file = ExcelExtensions.Create<OutputFileColumns>(_filesPath, $"NewFile{DateTime.Now.ToString("yyyyMMddHHmmss")}", "teste", list);

                    if (allRequestItemsOk)
                        request.SetAsOutputFileGenerated();


                    _repository.Update(request);

                    result = _repository.UnitOfWork.Commit().Result && allRequestItemsOk;
                });

                return result;
            });
        }
    }
}
