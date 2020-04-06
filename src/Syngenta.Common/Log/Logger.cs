namespace Syngenta.Common.Log
{
    public class Logger : Base
    {
        public Logger(string applicationName) : base(applicationName) { }
        public Logger(System.Type classe, string applicationName) : base(applicationName)
        {
            Base.Logar.Information(classe.FullName);
        }
    }
}
