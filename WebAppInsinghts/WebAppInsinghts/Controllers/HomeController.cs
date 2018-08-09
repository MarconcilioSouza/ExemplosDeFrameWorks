using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppInsinghts.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var tc = new TelimetriaCliente();

            try
            {
                //tc.LogAplicationInsightMsg("Chamada Index", SeverityLevel.Information);
                //Dictionary<string, string> a = new Dictionary<string, string>();

                //a.Add("Chamada Index", "Filtro X");

                //tc.LogAplicationInsightMsg("", SeverityLevel.Information, a);
                int n1 = 0;
                int n2 = 1;
                int n3 = n2 / n1;

                return View();
            }
            catch (Exception ex)
            {
                Dictionary<string, string> a = new Dictionary<string, string>();
                a.Add("Erro Interno", "Filtro Erros");
                tc.LogAplicationInsightException(ex, a);
                return View();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}