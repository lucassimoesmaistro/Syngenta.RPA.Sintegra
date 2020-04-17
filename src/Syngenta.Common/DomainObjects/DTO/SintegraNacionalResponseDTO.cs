using System;
using System.Collections.Generic;
using System.Text;

namespace Syngenta.Common.DomainObjects.DTO
{
    public class SintegraNacionalResponseDTO
    {
        public Response Response { get; set; }
    }

    public class Status
    {
        public string Protocol { get; set; }
        public DateTime DateTime { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public string Mode { get; set; }
        public string Source { get; set; }
    }

    public class Input
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class Output
    {
        public string nm_Empresa { get; set; }
        public string sg_UF { get; set; }
        public string nr_CNPJ { get; set; }
        public string ds_SituacaoCNPJ { get; set; }
        public string nr_InscricaoEstadual { get; set; }
        public string ds_SituacaoContribuinte { get; set; }
        public string tp_IE { get; set; }
        public string dt_Situacao { get; set; }
        public string nm_Fantasia { get; set; }
        public string dt_InicioAtividade { get; set; }
        public string dt_FimAtividade { get; set; }
        public string ds_RegimeTributacao { get; set; }
        public string ds_IEDestinatario { get; set; }
        public string ds_PorteEmpresa { get; set; }
        public string ds_EnderecoEletronico { get; set; }
        public string ds_NaturezaJuridica { get; set; }
        public string ds_OcorrenciaFiscal { get; set; }
        public string dt_CredenciamentoEmissorNFe { get; set; }
        public string ds_IndicadorObrigatoriedadeNFe { get; set; }
        public string dt_InicioObrigatoriedadeNFe { get; set; }
        public string ds_CNAEPrincipal { get; set; }
        public string ds_CNAESecundario { get; set; }
        public string ds_CNAEstadual { get; set; }
        public string cd_MunicipioIBGE { get; set; }
        public string ds_Incentivo { get; set; }
        public string sg_UFLocalizacao { get; set; }
        public string ds_Logradouro { get; set; }
        public string nr_Logradouro { get; set; }
        public string ds_Complemento { get; set; }
        public string nm_Bairro { get; set; }
        public string nr_CEP { get; set; }
        public string nm_Municipio { get; set; }
        public string nr_Telefone { get; set; }
    }

    public class Response
    {
        public Status Status { get; set; }
        public List<Input> Input { get; set; }
        public List<Output> Output { get; set; }
    }
}
