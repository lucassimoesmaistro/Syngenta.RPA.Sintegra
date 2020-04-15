using AutoMapper;
using Microsoft.Extensions.Configuration;
using Syngenta.Common.Office;
using Syngenta.Sintegra.Application.InputFiles.Models;
using Syngenta.Sintegra.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Syngenta.Sintegra.Domain.Request;

namespace Syngenta.Sintegra.Application.InputFiles
{
    public class InputFilesApplication : IInputFilesApplication
    {
        private readonly string _filesPath;
        private readonly IMapper _mapper;
        private readonly IRequestRepository _repository;

        public InputFilesApplication(IConfiguration configuration,
                                 IMapper mapper,
                                 IRequestRepository repository)
        {
            _filesPath = configuration["FilesPath:NewFiles"];
            _mapper = mapper;
            _repository = repository;
        }

        public IConfiguration Configuration { get; }

        public async Task<List<string>> GetAllFilesInInputFolder()
        {
            return await Task.Run(()=>
            {
                string[] filePaths = Directory.GetFiles(_filesPath);
                return filePaths.OfType<string>().ToList();
            });
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Request>> ImportCurstomersFromExcelFiles()
        {
            return await Task.Run(() =>
            {
                var filesList = GetAllFilesInInputFolder().Result;
                List<Request> requests = new List<Request>();
                Parallel.ForEach(filesList, file =>
                {
                    var excel = ExcelExtensions.Read<CustomersColletionModel>(file);
                    var request = RequestFactory.NewDraftOfRequest(file);
                    excel.ForEach(customer =>
                    {
                        var item = _mapper.Map<RequestItem>(customer);
                        request.AddItem(item);
                    });
                    request.SetAsRegisteredItems();
                    _repository.Add(request);
                    var retorno = _repository.UnitOfWork?.Commit().Result;
                    requests.Add(request);
                });
                return requests;
            });
        }
    }
}
