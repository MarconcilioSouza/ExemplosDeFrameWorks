using Microsoft.Web.Services3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace WS.App_Code
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [Microsoft.Web.Services3.Policy("ServerPolicy")]
    public class Service : System.Web.Services.WebService
    {
        public Service()
        {
            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        [WebMethod]
        public string HelloMyFriend()
        {
            //Say hello to the guy that you know
            return "Hello " + RequestSoapContext.Current.IdentityToken.Identity.Name;
        }

        public SegurancaClientes Credencial = new SegurancaClientes();


        [WebMethod]
        public string HelloWorld()
        {
            return "Hello " + RequestSoapContext.Current.IdentityToken.Identity.Name;
        }

        private Boolean Autenticou()
        {
            Boolean Autenticou = false;

            string Usuario = "admin";
            string Senha = "123456";

            if (Credencial.Usuario == Usuario && Credencial.Senha == Senha)
            {
                Autenticou = true;
            }

            return Autenticou;
        }




        [SoapHeader("Credencial")]
        [WebMethod(Description = "Retorna todos os endereços")]
        public List<vw_BuscarEndereco> GetBuscarEndereco()
        {
            if (!Autenticou())
            {
                throw new Exception("Erro: Usuário não autenticado.");
            }

            List<vw_BuscarEndereco> enderecos = null;

            using (var ctx = new dneEntity())
            {
                enderecos = ctx.vw_BuscarEndereco.OrderBy(x => x.nome).Skip(1).Take(10).ToList();
            }

            return enderecos;
        }
    }
}
