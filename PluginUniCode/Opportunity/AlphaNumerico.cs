using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginUniCode.Opportunity
{
    public class AlphaNumerico : PluginImplementation
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            Entity opportunity = (Entity)this.Context.InputParameters["Target"];

            
            opportunity["leg_oportunidadeid"] = generateCode();
            this.Service.Update(opportunity);
            
        }
        public string generateCode()
        {

            string[] arrayID = new string[7];

            arrayID[0] = "OPP-";

            Random rand = new Random();
            arrayID[1] = rand.Next(00000, 99999).ToString();
            
            arrayID[2] = "-";

            Random rand1 = new Random();
            arrayID[3] = Convert.ToChar(rand1.Next(65, 91)).ToString();

            Random rand2 = new Random();
            arrayID[4] = Convert.ToChar(rand2.Next(48, 58)).ToString();

            Random rand3 = new Random();
            arrayID[5] = Convert.ToChar((rand3.Next(65, 91) + 1) > 91 ? rand3.Next(65, 91) - 1 : rand3.Next(65, 91) + 1).ToString();

            Random rand4 = new Random();
            arrayID[6] = Convert.ToChar((rand4.Next(48, 58) + 1) > 58 ? rand4.Next(48, 58) - 1 : rand4.Next(48, 58) + 1).ToString();
           
            string id = String.Join("", arrayID);
            return id;

        }
        
    }
}
