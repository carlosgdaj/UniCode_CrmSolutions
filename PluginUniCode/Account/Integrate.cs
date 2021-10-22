using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginUniCode.Account
{
    public class Integrate : PluginImplementation
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            IOrganizationService service = GetCrmService();

            if (service != null)
                throw new InvalidPluginExecutionException("Conexão realizada com sucesso");
            else
                throw new InvalidPluginExecutionException("Conexão não realiza");
            //gabriel
            //carlos
        }

        public static IOrganizationService GetCrmService()
        {
            string connectionString =
               "AuthType=OAuth;" +
               "Username=tccambiente2@ambiente2.onmicrosoft.com;" +
               "Password=Senhagrupo2;" +
               "Url=https://orgb9bebf23.crm2.dynamics.com/;" +
               "AppId=b314a061-7cc9-4ee7-9621-ced0cfea6087;" +
               "RedirectUri=app://58145B91-0C36-4500-8554-080854F2AC97;";

            CrmServiceClient crmServiceClient = new CrmServiceClient(connectionString);
            return crmServiceClient.OrganizationWebProxyClient;
        }
    }
}