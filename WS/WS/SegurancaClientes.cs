using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;

namespace WS
{
    public class SegurancaClientes : SoapHeader
    {
        public string Usuario;
        public string Senha;
    }
}