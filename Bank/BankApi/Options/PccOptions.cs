using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Options
{
    public class PccOptions
    {
        public static string PCC = "PCC";
        public string BaseURL { get; set; }
        public string Route { get; set; }

        public PccOptions()
        {
        }
    }
}
