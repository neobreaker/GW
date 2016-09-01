using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace GW
{
    public class Constant
    {
        public static string CONNSTR = ConfigurationManager.AppSettings["SqlConnectionString"];

        public static int MAXFORMULATYPE = 5;               //made follow formula
        public static int FORMULAAH = MAXFORMULATYPE;       //buy from AH
        public static int FORMULANPC = 6;                   //buy from NPC
        public static int FORMULAUNKNOWN = 7;               //unknown
    }
}