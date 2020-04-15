namespace Syngenta.Sintegra.Domain
{
    public interface ISintegraFacade
    {
        Customer GetDataByCnpj(string cnpj);
        Customer GetDataByCpf(string cpf);
    }
}
