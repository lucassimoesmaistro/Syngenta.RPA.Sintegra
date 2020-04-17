using Syngenta.Common.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Syngenta.Sintegra.Domain
{
    public class Request : Entity, IAggregateRoot
    {
        public DateTime Timestamp { get; private set; }
        public string FileName { get; private set; }
        public RequestStatus RequestStatus { get; private set; }

        private readonly List<RequestItem> _requestItems;
        public IReadOnlyCollection<RequestItem> RequestItems => _requestItems;

        public Request(string fileName)
        {
            Timestamp = DateTime.Now;
            FileName = fileName;
            _requestItems = new List<RequestItem>();
        }

        protected Request()
        {
            _requestItems = new List<RequestItem>();
        }
        public static class RequestFactory
        {
            public static Request NewDraftOfRequest(string fileName)
            {
                var request = new Request
                {
                    FileName = fileName,
                };

                request.MakeDraft();
                return request;
            }
        }

        private void MakeDraft()
        {
            RequestStatus = RequestStatus.Draft;
        }

        public void AddItem(RequestItem item)
        {
            item.ConnectToRequest(Id);
            item.SetStatusRegistred();

            _requestItems.Add(item);
        }

        public void SetAsRegisteredItems()
        {
            RequestStatus = RequestStatus.RegisteredItems;
        }

        public void SetAsProcessed()
        {
            RequestStatus = RequestStatus.Processed;
        }
    }

}
