using Persistence.DAO.Sim;
using Services.Simulador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace WebApplication.Client.Web.CO.PG.Usr.Handler_Descuento
{
    /// <summary>
    /// Summary description for Descuentos
    /// </summary>
    public class Descuentos : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            var prueba = GetData(context.Request["Segcli"], context.Request["SegReg"], context.Request["planta"]);

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            string Response = javaScriptSerializer.Serialize(prueba);
            context.Response.ContentType = "application / json";
            context.Response.Write(Response);
        }

      
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private String GetData(String Segcli, String SegReg, string planta)
        {
            SimServ valSeg = new SimServ();

            Boolean prueba = false;
            if (planta != "0") 
            {
                prueba = true;
            }
            return valSeg.ValSegmentacion(Segcli, SegReg, prueba);

        }
    }
}