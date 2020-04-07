using Syngenta.Common.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Syngenta.RPA.Sintegra.Domain
{
    public class ChangeLog : Entity
    {
        public Guid RequestItemId { get; private set; }

        public string FieldName { get; private set; }
        public string NewValue { get; private set; }

        public RequestItem RequestItem { get; set; }
        public ChangeLog(string fieldName, string newValue)
        {
            FieldName = fieldName;
            NewValue = newValue;
        }

        protected ChangeLog() { }

        internal void ConnectToRequestItem(Guid requestItemId)
        {
            RequestItemId = requestItemId;
        }
    }
}
