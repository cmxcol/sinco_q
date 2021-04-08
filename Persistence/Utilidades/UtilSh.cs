using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace Persistence.Utilidades
{
    public class UtilSh
    {
        public static String strCnn = System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString();
        public static String strCnn2 = System.Configuration.ConfigurationManager.ConnectionStrings["cnnSAPDB"].ToString();

        public static String getCnnStr(String str)
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[str].ToString();
        }
    }
}