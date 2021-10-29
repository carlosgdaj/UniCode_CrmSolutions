using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System.Net;

namespace RoboCargadeDados
{
    internal class Conexao
    {
        private static CrmServiceClient crmServiceClientLegado;
        public CrmServiceClient ConexaoRobo()
        {
            string connectionString =
             "AuthType=OAuth;" +
             "Username=Unicodelegado@Unicodelegado.onmicrosoft.com;" +
             "Password=Senhagrupo2;" +
             "Url=https://org4bb1a5b3.crm2.dynamics.com;" +
             "AppId=08b4fc34-8705-4a4f-a8ef-fc0a667420ce;" +
             "RedirectUri=app://58145B91-0C36-4500-8554-080854F2AC97;";

            //CrmServiceClient crmServiceClient = new CrmServiceClient(connectionString);
            if (crmServiceClientLegado == null)
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                crmServiceClientLegado = new CrmServiceClient(connectionString);
            }

            return crmServiceClientLegado;


        }
    }
}