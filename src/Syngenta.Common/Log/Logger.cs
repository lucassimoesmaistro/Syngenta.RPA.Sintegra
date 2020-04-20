namespace Syngenta.Common.Log
{
    public class Logger : Base
    {
        public Logger(string applicationName, string logFilesFolder) : base(applicationName, logFilesFolder) { }
        public Logger(System.Type classe, string applicationName, string logFilesFolder) : base(applicationName, logFilesFolder)
        {
            Base.Logar.Information(classe.FullName);
        }
    }
}
