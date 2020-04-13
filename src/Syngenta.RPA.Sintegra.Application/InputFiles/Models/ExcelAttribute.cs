using System;
using System.Collections.Generic;
using System.Text;

namespace Syngenta.RPA.Sintegra.Application.InputFiles.Models
{
    [AttributeUsage(AttributeTargets.All)]
    public class ExcelColumnAttribute : Attribute
    {

        private readonly string _name;

        public ExcelColumnAttribute(string name)
        {
            this._name = name;
        }
        public string Name
        {
            get { return _name; }
        }
    }
}
