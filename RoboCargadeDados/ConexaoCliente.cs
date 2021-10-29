using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System.Net;

namespace RoboCargadeDados
{
    internal class ConexaoCliente
    {
        private static CrmServiceClient crmServiceClientDestino;
        public CrmServiceClient ConexaoDestino()
        {
            string connectionString =
                 "AuthType=OAuth;" +
                 "Username=tccambiente2@ambiente2.onmicrosoft.com;" +
                 "Password=Senhagrupo2;" +
                 "Url=https://orgb9bebf23.crm2.dynamics.com/;" +
                 "AppId=b314a061-7cc9-4ee7-9621-ced0cfea6087;" +
                 "RedirectUri=app://58145B91-0C36-4500-8554-080854F2AC97;";

            //CrmServiceClient crmServiceClient = new CrmServiceClient(connectionString);
            if (crmServiceClientDestino == null)
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                crmServiceClientDestino = new CrmServiceClient(connectionString);
            }
            return crmServiceClientDestino;
        }
    }
}